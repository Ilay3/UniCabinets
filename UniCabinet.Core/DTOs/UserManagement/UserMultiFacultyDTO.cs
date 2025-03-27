using UniCabinet.Core.DTOs.Common;

namespace UniCabinet.Core.DTOs.UserManagement
{
    public class UserMultiFacultyDTO
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public List<int> SelectedFacultyIds { get; set; }
        public List<SelectListItemDTO> AvailableFaculties { get; set; }
        public int? PrimaryFacultyId { get; set; }
    }
}