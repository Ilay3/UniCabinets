using UniCabinet.Core.DTOs.CourseManagement;

namespace UniCabinet.Core.DTOs.DepartmentManagmnet;

public class DepartmentDTO
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }

    public int? FacultyId { get; set; }
    public string FacultyName { get; set; }
    public List<DisciplineDTO> Discipline { get; set; }

}
