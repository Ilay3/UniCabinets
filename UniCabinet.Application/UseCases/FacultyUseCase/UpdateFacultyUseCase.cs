using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.FacultyManagement;

namespace UniCabinet.Application.UseCases.FacultyUseCase
{
    public class UpdateFacultyUseCase
    {
        private readonly IFacultyRepository _facultyRepository;

        public UpdateFacultyUseCase(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        public async Task ExecuteAsync(FacultyEditDTO facultyDTO)
        {
            await _facultyRepository.UpdateFacultyAsync(facultyDTO);
        }
    }
}