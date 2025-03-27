using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.DepartmentManagmnet;
using UniCabinet.Domain.Models;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository;

public class DepartmentRepositoryImpl : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DepartmentRepositoryImpl(ApplicationDbContext context, IMapper mapper)

    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DepartmentDTO>> GetAllDepartment()
    {
        var departmentEntity = await _context.Departments
            .Include(d => d.Faculty) 
            .ToListAsync();
        return _mapper.Map<List<DepartmentDTO>>(departmentEntity);
    }

    public async Task<DepartmentEntity> GetDepartmentWithDisciplinesAndTeachersAsync(string headUserId)
    {
        return await _context.Departments
            .Include(d => d.Discipline)
                .ThenInclude(disc => disc.Specialty)
                    .ThenInclude(spec => spec.Teachers)
            .FirstOrDefaultAsync(d => d.User.Any(u => u.Id == headUserId));
    }



    public async Task<List<DepartmentEntity>> GetDepartmentsWithUsersAsync()
    {
        var departments = await _context.Departments
            .AsNoTracking()
            .Include(d => d.Faculty)
            .Include(d => d.User)
            .Include(d => d.Discipline)
                .ThenInclude(di => di.Specialty)
            .ToListAsync();

        return departments;
    }

    public async Task AddDepartmentAsync(DepartmentEntity department)
    {
        if (department == null)
            throw new ArgumentNullException(nameof(department));

        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDepartmentAsync(DepartmentEntity department)
    {
        if (department == null)
            throw new ArgumentNullException(nameof(department));

        _context.Departments.Update(department);
        await _context.SaveChangesAsync();
    }

    public async Task<DepartmentEntity> GetDepartmentByIdAsync(int departmentId)
    {
        return await _context.Departments
        .Include(d => d.Faculty)
        .FirstOrDefaultAsync(d => d.Id == departmentId);

    }

    public async Task<List<DepartmentDTO>> GetDepartmentsByFacultyIdAsync(int facultyId)
    {
        var departments = await _context.Departments
            .Include(d => d.Faculty)
            .Where(d => d.FacultyId == facultyId)
            .ToListAsync();

        return _mapper.Map<List<DepartmentDTO>>(departments);
    }
    public async Task<List<DepartmentDTO>> GetDepartmentsWithoutFacultyAsync()
    {
        var departments = await _context.Departments
            .Where(d => d.FacultyId == null)
            .ToListAsync();

        return _mapper.Map<List<DepartmentDTO>>(departments);
    }
}
