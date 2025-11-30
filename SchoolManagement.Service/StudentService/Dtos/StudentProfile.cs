using AutoMapper;
using SchoolManagement.Data.Entities;
using SchoolManagement.Service.SubmissionService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.StudentService.Dtos
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentClass, StudentClassDto>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Class.Course.Name))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Class.Teacher.Name))
                .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => src.Class.Semester))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Class.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Class.EndDate));

            CreateMap<Attendance, AttendanceDto>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name));

            CreateMap<Assignment, AssignmentDto>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name));

            CreateMap<Submission, SubmissionDto>();

            CreateMap<Submission, GradeDto>()
                .ForMember(dest => dest.AssignmentTitle, opt => opt.MapFrom(src => src.Assignment.Title));
        }
    }


}
