using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.FacultyManagement;

namespace UniCabinet.Application.UseCases.FacultyUseCase
{
    public class AddFacultyUseCase
    {
        private readonly IFacultyRepository _facultyRepository;

        public AddFacultyUseCase(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        public async Task ExecuteAsync(FacultyCreateDTO facultyDTO)
        {
            await _facultyRepository.AddFacultyAsync(facultyDTO);
        }
    }
}