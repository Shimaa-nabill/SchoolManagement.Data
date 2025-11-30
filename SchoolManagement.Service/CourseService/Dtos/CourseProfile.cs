using AutoMapper;
using SchoolManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.CourseService.Dtos
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
         
            CreateMap<CourseCreateDto, Course>();

           
            CreateMap<CourseUpdateDto, Course>();

            
            CreateMap<Course, CourseResponseDto>()
                .ForMember(dest => dest.DepartmentName,
                           opt => opt.MapFrom(src => src.Department.Name));
        }
    }
}
