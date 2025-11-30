using AutoMapper;
using SchoolManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.AttendanceService.Dtos
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile()
        {
            CreateMap<Attendance, AttendanceResponseDto>()
                .ForMember(dest => dest.StudentName,
                           opt => opt.MapFrom(src => src.Student.Name));

            CreateMap<AttendanceCreateDto, Attendance>();
        }
    }
}
