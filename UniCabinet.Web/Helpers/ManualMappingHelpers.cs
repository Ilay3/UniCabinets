using UniCabinet.Core.DTOs.Common;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Core.Models.ViewModel.Common;
using UniCabinet.Core.Models.ViewModel.User;
using System.Collections.Generic;
using System.Linq;

namespace UniCabinet.Web.Helpers
{
    public static class ManualMappingHelpers
    {
        // Manual mapping for UserMultiDepartmentDTO to UserMultiDepartmentVM
        public static UserMultiDepartmentVM ToViewModel(this UserMultiDepartmentDTO dto)
        {
            if (dto == null) return null;

            return new UserMultiDepartmentVM
            {
                UserId = dto.UserId,
                FullName = dto.FullName,
                SelectedDepartmentIds = dto.SelectedDepartmentIds?.ToList() ?? new List<int>(),
                PrimaryDepartmentId = dto.PrimaryDepartmentId,
                AvailableDepartments = dto.AvailableDepartments?.Select(item => new SelectListItemVM
                {
                    Value = item.Value,
                    Text = item.Text
                }).ToList() ?? new List<SelectListItemVM>()
            };
        }

        // Manual mapping for UserMultiFacultyDTO to UserMultiFacultyVM
        public static UserMultiFacultyVM ToViewModel(this UserMultiFacultyDTO dto)
        {
            if (dto == null) return null;

            return new UserMultiFacultyVM
            {
                UserId = dto.UserId,
                FullName = dto.FullName,
                SelectedFacultyIds = dto.SelectedFacultyIds?.ToList() ?? new List<int>(),
                PrimaryFacultyId = dto.PrimaryFacultyId,
                AvailableFaculties = dto.AvailableFaculties?.Select(item => new SelectListItemVM
                {
                    Value = item.Value,
                    Text = item.Text
                }).ToList() ?? new List<SelectListItemVM>()
            };
        }
    }
}