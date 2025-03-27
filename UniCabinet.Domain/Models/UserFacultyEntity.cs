using UniCabinet.Domain.Entities;

namespace UniCabinet.Domain.Models
{
    public class UserFacultyEntity
    {
        public string UserId { get; set; }
        public UserEntity User { get; set; }

        public int FacultyId { get; set; }
        public FacultyEntity Faculty { get; set; }

        public bool IsPrimary { get; set; } // Флаг для основного факультета
    }
}