using UniCabinet.Core.Models.ViewModel.Departmet;

namespace UniCabinet.Core.Models.ViewModel.Faculty
{
    public class FacultyBaseVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class FacultyListVM : FacultyBaseVM
    {
        public int Id { get; set; }
        public int DepartmentsCount { get; set; }
    }

    public class FacultyCreateVM : FacultyBaseVM
    {
    }

    public class FacultyEditVM : FacultyBaseVM
    {
        public int Id { get; set; }
    }

    public class FacultyDetailsVM : FacultyBaseVM
    {
        public int Id { get; set; }
        public List<DepartmantVM> Departments { get; set; } = new List<DepartmantVM>();
    }

    public class FacultyHierarchyVM
    {
        public List<FacultyWithDepartmentsVM> Faculties { get; set; } = new List<FacultyWithDepartmentsVM>();
    }

    public class FacultyWithDepartmentsVM : FacultyBaseVM
    {
        public int Id { get; set; }
        public List<DepartmantVM> Departments { get; set; } = new List<DepartmantVM>();
    }

    public class FacultyDepartmentAssignmentVM
    {
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public List<DepartmentAssignmentItemVM> AvailableDepartments { get; set; } = new List<DepartmentAssignmentItemVM>();
    }

    public class DepartmentAssignmentItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAssigned { get; set; }
    }
}