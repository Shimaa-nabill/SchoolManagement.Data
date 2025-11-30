using AutoMapper;
using SchoolManagement.Data.Entities;
using SchoolManagement.Service.AssignmentService.Dtos;
using SchoolManagement.Service.AttendanceService.Dtos;
using SchoolManagement.Service.ClassService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.TeacherService.Dtos
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Class, ClassResponseDto>()
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name));

            CreateMap<ClassCreateDto, Class>();
            CreateMap<ClassUpdateDto, Class>();

            CreateMap<Attendance, AttendanceResponseDto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name));

            CreateMap<Assignment, AssignmentResponseDto>();
            CreateMap<AssignmentCreateDto, Assignment>();

            CreateMap<NotificationCreateDto, Notification>();
        }
    }
}
