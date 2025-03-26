using UniCabinet.Core.DTOs.DepartmentManagmnet;

namespace UniCabinet.Core.DTOs.FacultyManagement
{
    public class FacultyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<DepartmentDTO> Departments { get; set; } = new List<DepartmentDTO>();
    }

    public class FacultyCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class FacultyEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class FacultyListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DepartmentsCount { get; set; }
    }

    public class FacultyDepartmentAssignmentDTO
    {
        public int FacultyId { get; set; }
        public List<int> DepartmentIds { get; set; } = new List<int>();
    }
}