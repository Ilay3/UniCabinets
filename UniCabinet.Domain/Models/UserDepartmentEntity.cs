using UniCabinet.Domain.Entities;
using UniCabinet.Domain.Models;

namespace UniCabinet.Domain.Models
{
    public class UserDepartmentEntity
    {
        public string UserId { get; set; }
        public UserEntity User { get; set; }

        public int DepartmentId { get; set; }
        public DepartmentEntity Department { get; set; }

        public bool IsPrimary { get; set; } // Флаг для основной кафедры
    }
}