using AutoMapper;
using SchoolManagement.Data.Entities;
using SchoolManagement.Service.CourseService.Dtos;
using SchoolManagement.Service.DepartmentService.Dtos;
using SchoolManagement.Service.UserService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.AdminService.Dtos
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Department, DepartmentResponseDto>()
                .ForMember(d => d.HeadOfDepartmentName, o => o.MapFrom(s => s.HeadOfDepartment != null ? s.HeadOfDepartment.UserName : null));
            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<DepartmentUpdateDto, Department>();

            CreateMap<Course, CourseResponseDto>()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department != null ? s.Department.Name : null));
            CreateMap<CourseCreateDto, Course>();
            CreateMap<CourseUpdateDto, Course>();

            CreateMap<ApplicationUser, UserResponseDto>()
                .ForMember(d => d.Role, o => o.Ignore()); 
        }
    }
}
