using UniCabinet.Core.DTOs.FacultyManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IFacultyRepository
    {
        Task<List<FacultyListDTO>> GetAllFacultiesAsync();
        Task<FacultyDTO> GetFacultyByIdAsync(int id);
        Task<FacultyDTO> GetFacultyWithDepartmentsAsync(int id);
        Task AddFacultyAsync(FacultyCreateDTO faculty);
        Task UpdateFacultyAsync(FacultyEditDTO faculty);
        Task DeleteFacultyAsync(int id);
        Task AssignDepartmentsToFacultyAsync(int facultyId, List<int> departmentIds);
    }
}