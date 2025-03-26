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
            CreateMap<FacultyEntity, FacultyDTO>()
                .ForMember(dest => dest.Departments, opt => opt.MapFrom(src => src.Departments));

            CreateMap<FacultyEntity, FacultyListDTO>()
                .ForMember(dest => dest.DepartmentsCount, opt => opt.MapFrom(src => src.Departments.Count));

            CreateMap<FacultyCreateDTO, FacultyEntity>();
            CreateMap<FacultyEditDTO, FacultyEntity>();

            CreateMap<FacultyDTO, FacultyDetailsVM>();
            CreateMap<FacultyDTO, FacultyWithDepartmentsVM>();
            CreateMap<FacultyListDTO, FacultyListVM>();
            CreateMap<FacultyCreateDTO, FacultyCreateVM>().ReverseMap();
            CreateMap<FacultyEditDTO, FacultyEditVM>().ReverseMap();
        }
    }
}