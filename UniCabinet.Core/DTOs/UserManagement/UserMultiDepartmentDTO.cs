using UniCabinet.Core.DTOs.Common;

namespace UniCabinet.Core.DTOs.UserManagement
{
    public class UserMultiDepartmentDTO
    {
        public UserMultiDepartmentDTO()
        {
            SelectedDepartmentIds = new List<int>();
            AvailableDepartments = new List<SelectListItemDTO>();
        }

        public string UserId { get; set; }
        public string FullName { get; set; }
        public List<int> SelectedDepartmentIds { get; set; }
        public List<SelectListItemDTO> AvailableDepartments { get; set; }
        public int? PrimaryDepartmentId { get; set; }
    }

}