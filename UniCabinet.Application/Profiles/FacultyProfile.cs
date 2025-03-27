using AutoMapper;
using UniCabinet.Core.DTOs.FacultyManagement;
using UniCabinet.Core.Models.ViewModel.Faculty;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Profiles
{
    public class FacultyProfile : Profile
    {
        public FacultyProfile()
        {
            // Existing mappings
            CreateMap<FacultyEntity, FacultyDTO>()
                .ForMember(dest => dest.Departments, opt => opt.MapFrom(src => src.Departments));

            CreateMap<FacultyEntity, FacultyListDTO>()
                .ForMember(dest => dest.DepartmentsCount, opt => opt.MapFrom(src => src.Departments.Count));

            // New mapping for FacultyDTO to FacultyEditVM
            CreateMap<FacultyDTO, FacultyEditVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            // Existing create and edit mappings
            CreateMap<FacultyCreateDTO, FacultyEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Departments, opt => opt.Ignore());

            CreateMap<FacultyEditDTO, FacultyEntity>()
                .ForMember(dest => dest.Departments, opt => opt.Ignore());

            // View Model Mappings
            CreateMap<FacultyDTO, FacultyDetailsVM>();
            CreateMap<FacultyDTO, FacultyWithDepartmentsVM>();
            CreateMap<FacultyListDTO, FacultyListVM>();
            CreateMap<FacultyCreateDTO, FacultyCreateVM>().ReverseMap();
            CreateMap<FacultyEditDTO, FacultyEditVM>().ReverseMap();
        }
    }
}