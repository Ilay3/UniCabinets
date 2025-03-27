using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Core.Models.ViewModel.Departmet;

public class DepartmentsWithUsersVM
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }
    public int? FacultyId { get; set; }
    public string FacultyName { get; set; }
    public List<UserDTO> Users { get; set; }
    public List<DisciplineDTO> Discipline { get; set; }
}
