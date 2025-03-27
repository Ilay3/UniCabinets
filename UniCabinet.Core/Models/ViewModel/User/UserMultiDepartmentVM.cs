using UniCabinet.Core.Models.ViewModel.Common;

namespace UniCabinet.Core.Models.ViewModel.User
{
    public class UserMultiDepartmentVM
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public List<int> SelectedDepartmentIds { get; set; }
        public List<SelectListItemVM> AvailableDepartments { get; set; }
        public int? PrimaryDepartmentId { get; set; }
    }
}