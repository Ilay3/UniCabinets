using UniCabinet.Core.Models.ViewModel.Common;

namespace UniCabinet.Core.Models.ViewModel.User
{
    public class UserMultiFacultyVM
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public List<int> SelectedFacultyIds { get; set; }
        public List<SelectListItemVM> AvailableFaculties { get; set; }
        public int? PrimaryFacultyId { get; set; }
    }
}