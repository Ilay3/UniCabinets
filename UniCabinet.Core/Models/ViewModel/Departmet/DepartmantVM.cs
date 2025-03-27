using UniCabinet.Core.Models.ViewModel.Discipline;

namespace UniCabinet.Core.Models.ViewModel.Departmet
{
     public class DepartmantVM
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }

        public int? FacultyId { get; set; }
        public string FacultyName { get; set; }
        public List<DisciplineListVM> Discipline { get; set; }
    }
}
