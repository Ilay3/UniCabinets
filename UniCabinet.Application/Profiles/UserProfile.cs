using AutoMapper;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Core.DTOs.DepartmentManagmnet;
using UniCabinet.Core.DTOs.FacultyManagement;
using UniCabinet.Core.Models.ViewModel.User;
using UniCabinet.Domain.Entities;
using UniCabinet.Domain.Models;
using UniCabinet.Core.DTOs.Common;

namespace UniCabinet.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // User mappings
            CreateMap<UserEntity, UserDTO>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(src => src.SpecialtyId));
            CreateMap<UserDTO, UserDetailVM>();

            // Department mappings
            CreateMap<DepartmentDTO, DepartmentEntity>()
                .ForMember(dest => dest.UserDepartments, opt => opt.Ignore());

            // Faculty mappings
            CreateMap<FacultyCreateDTO, FacultyEntity>()
                .ForMember(dest => dest.UserFaculties, opt => opt.Ignore());
            CreateMap<FacultyEditDTO, FacultyEntity>()
                .ForMember(dest => dest.UserFaculties, opt => opt.Ignore());

            // Multi-department mappings
            CreateMap<UserMultiDepartmentDTO, UserMultiDepartmentVM>()
                .ReverseMap()
                .ForMember(dest => dest.AvailableDepartments, opt => opt.MapFrom(
                    (src, dest) => src.AvailableDepartments ?? new List<SelectListItemDTO>()));

            // Multi-faculty mappings
            CreateMap<UserMultiFacultyDTO, UserMultiFacultyVM>()
                .ReverseMap()
                .ForMember(dest => dest.AvailableFaculties, opt => opt.MapFrom(
                    (src, dest) => src.AvailableFaculties ?? new List<SelectListItemDTO>()));
        }
    }
}