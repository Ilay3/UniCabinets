using UniCabinet.Core.DTOs.Common;

namespace UniCabinet.Core.DTOs.UserManagement
{
    public class UserMultiDepartmentDTO
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public List<int> SelectedDepartmentIds { get; set; }
        public List<SelectListItemDTO> AvailableDepartments { get; set; }
        public int? PrimaryDepartmentId { get; set; }
    }
}