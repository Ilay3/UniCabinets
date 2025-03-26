using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.FacultyManagement;

namespace UniCabinet.Application.UseCases.FacultyUseCase
{
    public class GetFacultyWithDepartmentsUseCase
    {
        private readonly IFacultyRepository _facultyRepository;

        public GetFacultyWithDepartmentsUseCase(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        public async Task<FacultyDTO> ExecuteAsync(int id)
        {
            return await _facultyRepository.GetFacultyWithDepartmentsAsync(id);
        }
    }
}