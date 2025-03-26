using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.FacultyManagement;

namespace UniCabinet.Application.UseCases.FacultyUseCase
{
    public class GetAllFacultiesUseCase
    {
        private readonly IFacultyRepository _facultyRepository;

        public GetAllFacultiesUseCase(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        public async Task<List<FacultyListDTO>> ExecuteAsync()
        {
            return await _facultyRepository.GetAllFacultiesAsync();
        }
    }
}