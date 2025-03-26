using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.FacultyManagement;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository
{
    public class FacultyRepositoryImpl : IFacultyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FacultyRepositoryImpl(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FacultyListDTO>> GetAllFacultiesAsync()
        {
            var faculties = await _context.Faculties
                .Include(f => f.Departments)
                .ToListAsync();

            return faculties.Select(f => new FacultyListDTO
            {
                Id = f.Id,
                Name = f.Name,
                Description = f.Description,
                DepartmentsCount = f.Departments?.Count ?? 0
            }).ToList();
        }

        public async Task<FacultyDTO> GetFacultyByIdAsync(int id)
        {
            var faculty = await _context.Faculties
                .FirstOrDefaultAsync(f => f.Id == id);

            return _mapper.Map<FacultyDTO>(faculty);
        }

        public async Task<FacultyDTO> GetFacultyWithDepartmentsAsync(int id)
        {
            var faculty = await _context.Faculties
                .Include(f => f.Departments)
                .FirstOrDefaultAsync(f => f.Id == id);

            return _mapper.Map<FacultyDTO>(faculty);
        }

        public async Task AddFacultyAsync(FacultyCreateDTO faculty)
        {
            var entity = new FacultyEntity
            {
                Name = faculty.Name,
                Description = faculty.Description
            };

            await _context.Faculties.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFacultyAsync(FacultyEditDTO faculty)
        {
            var entity = await _context.Faculties.FindAsync(faculty.Id);
            if (entity == null) return;

            entity.Name = faculty.Name;
            entity.Description = faculty.Description;

            _context.Faculties.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFacultyAsync(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null) return;

            _context.Faculties.Remove(faculty);
            await _context.SaveChangesAsync();
        }

        public async Task AssignDepartmentsToFacultyAsync(int facultyId, List<int> departmentIds)
        {
            var departments = await _context.Departments
                .Where(d => departmentIds.Contains(d.Id))
                .ToListAsync();

            foreach (var department in departments)
            {
                department.FacultyId = facultyId;
            }

            var departmentsToDetach = await _context.Departments
                .Where(d => d.FacultyId == facultyId && !departmentIds.Contains(d.Id))
                .ToListAsync();

            // Открепим их от факультета
            foreach (var department in departmentsToDetach)
            {
                department.FacultyId = null;
            }

            await _context.SaveChangesAsync();
        }
    }
}