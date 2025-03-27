using UniCabinet.Domain.Models;

namespace UniCabinet.Domain.Entities
{
    public class FacultyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<DepartmentEntity> Departments { get; set; }
        public ICollection<UserFacultyEntity> UserFaculties { get; set; }
    }
}