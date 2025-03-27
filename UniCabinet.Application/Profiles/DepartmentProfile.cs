using AutoMapper;
using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Core.DTOs.DepartmentManagmnet;
using UniCabinet.Core.Models.ViewModel.Department;
using UniCabinet.Core.Models.ViewModel.Departmet;
using UniCabinet.Domain.Models;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Profiles;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        // 1. Маппинг DepartmentEntity <-> DepartmentDTO
        CreateMap<DepartmentEntity, DepartmentDTO>()
            .ForMember(dest => dest.FacultyId, opt => opt.MapFrom(src => src.FacultyId))
            .ForMember(dest => dest.FacultyName, opt => opt.MapFrom(src => src.Faculty != null ? src.Faculty.Name : null));

        CreateMap<DepartmentDTO, DepartmentEntity>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Faculty, opt => opt.Ignore())
            .ForMember(dest => dest.Discipline, opt => opt.Ignore());

        // 2. Маппинг DepartmentEntity <-> DepartmentsWithUsersDTO
        CreateMap<DepartmentEntity, DepartmentsWithUsersDTO>()
            .ForMember(dest => dest.FacultyId, opt => opt.MapFrom(src => src.FacultyId))
            .ForMember(dest => dest.FacultyName, opt => opt.MapFrom(src => src.Faculty != null ? src.Faculty.Name : null))
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline));

        // 3. Маппинг между DTO и ViewModel
        CreateMap<DepartmentDTO, DepartmantVM>().ReverseMap();
        CreateMap<DepartmentsWithUsersDTO, DepartmentsWithUsersVM>().ReverseMap();
        CreateMap<GetDepartmantAndUserDTO, GetDepartmantAndUserVM>().ReverseMap();
        CreateMap<DepartmentDTO, DepartmentAddEditVM>().ReverseMap();
        CreateMap<DepartmentWithDisciplinesDTO, DepartmentWithDisciplinesVM>().ReverseMap();
        CreateMap<DisciplineWithTeachersDTO, DisciplineWithTeachersVM>().ReverseMap();
    }
}