using UniCabinet.Application.Interfaces.Repository;

namespace UniCabinet.Application.UseCases.FacultyUseCase
{
    public class DeleteFacultyUseCase
    {
        private readonly IFacultyRepository _facultyRepository;

        public DeleteFacultyUseCase(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        public async Task ExecuteAsync(int id)
        {
            await _facultyRepository.DeleteFacultyAsync(id);
        }
    }
}