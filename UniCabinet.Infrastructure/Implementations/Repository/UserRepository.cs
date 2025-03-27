// UniCabinet.Infrastructure/Repositories/UserRepository.cs
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Core.Models.ViewModel.DisciplineDetail;
using UniCabinet.Domain.Entities;
using UniCabinet.Domain.Models;
using UniCabinet.Infrastructure.Data;


namespace UniCabinet.Infrastructure.Implementations.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ApplicationDbContext _context;

        public UserRepository(UserManager<UserEntity> userManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _context = applicationDbContext;
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersWithRolesAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersWithRoles = new List<UserEntity>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Студент"))
                {
                    // Загрузить информацию о группе студента
                    var group = await _context.Groups.FirstOrDefaultAsync(g => g.Users.Any(s => s.Id == user.Id));
                    if (group != null)
                    {
                        user.Group = group;  // Добавляем группу к пользователю
                    }
                }

                usersWithRoles.Add(user);
            }

            return usersWithRoles;
        }

  
        public async Task UpdateUserGroupAsync(string userId, int groupId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                var group = await _context.Groups.FindAsync(groupId);
                if (group != null)
                {
                    user.GroupId = groupId;
                    _context.Users.Update(user);  // Обновляем пользователя в базе
                    await _context.SaveChangesAsync();  // Сохраняем изменения
                }
            }
        }

 
        public async Task<IEnumerable<UserEntity>> SearchUsersAsync(string query)
        {
            return await _context.Users
                .Where(u => u.FirstName.Contains(query) || u.LastName.Contains(query) || u.Patronymic.Contains(query) || u.Email.Contains(query))
                .ToListAsync();
        }

        public async Task<UserEntity> GetUserByIdAsync(string userId)
        {
            return await _userManager.Users
                .Include(u => u.Group)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

   
        public async Task<List<UserDTO>> GetStudentsByGroupIdAsync(int groupId)
        {
            var students = await _context.Users
             .Where(u => u.GroupId == groupId)
             .ToListAsync();

            return students.Select(u => new UserDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Patronymic = u.Patronymic,
                GroupId = u.GroupId
            }).ToList();
        }

        public async Task<List<UserDepartmentEntity>> GetUserDepartmentsAsync(string userId)
        {
            return await _context.UserDepartments
                .Where(ud => ud.UserId == userId)
                .Include(ud => ud.Department)
                .ToListAsync();
        }

        public async Task UpdateUserDepartmentsAsync(string userId, List<int> departmentIds, int? primaryDepartmentId)
        {
            // Удаляем существующие связи
            var existingUserDepts = await _context.UserDepartments
                .Where(ud => ud.UserId == userId)
                .ToListAsync();

            _context.UserDepartments.RemoveRange(existingUserDepts);

            // Добавляем новые связи
            foreach (var deptId in departmentIds)
            {
                await _context.UserDepartments.AddAsync(new UserDepartmentEntity
                {
                    UserId = userId,
                    DepartmentId = deptId,
                    IsPrimary = deptId == primaryDepartmentId
                });
            }

            // Обновляем основную кафедру в UserEntity
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.DepartmentId = primaryDepartmentId;
                _context.Users.Update(user);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<UserFacultyEntity>> GetUserFacultiesAsync(string userId)
        {
            return await _context.UserFaculties
                .Where(uf => uf.UserId == userId)
                .Include(uf => uf.Faculty)
                .ToListAsync();
        }

        public async Task UpdateUserFacultiesAsync(string userId, List<int> facultyIds, int? primaryFacultyId)
        {
            // Удаляем существующие связи
            var existingUserFacs = await _context.UserFaculties
                .Where(uf => uf.UserId == userId)
                .ToListAsync();

            _context.UserFaculties.RemoveRange(existingUserFacs);

            // Добавляем новые связи
            foreach (var facId in facultyIds)
            {
                await _context.UserFaculties.AddAsync(new UserFacultyEntity
                {
                    UserId = userId,
                    FacultyId = facId,
                    IsPrimary = facId == primaryFacultyId
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
