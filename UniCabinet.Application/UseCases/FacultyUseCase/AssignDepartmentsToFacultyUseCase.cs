using UniCabinet.Application.Interfaces.Repository;

namespace UniCabinet.Application.UseCases.FacultyUseCase
{
    public class AssignDepartmentsToFacultyUseCase
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public AssignDepartmentsToFacultyUseCase(
            IFacultyRepository facultyRepository,
            IDepartmentRepository departmentRepository)
        {
            _facultyRepository = facultyRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task ExecuteAsync(int facultyId, List<int> departmentIds)
        {
            await _facultyRepository.AssignDepartmentsToFacultyAsync(facultyId, departmentIds);
        }
    }
}