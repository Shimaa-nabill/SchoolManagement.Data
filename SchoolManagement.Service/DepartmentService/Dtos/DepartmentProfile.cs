using AutoMapper;
using SchoolManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.DepartmentService.Dtos
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
           
            CreateMap<DepartmentCreateDto, Department>();

            
            CreateMap<DepartmentUpdateDto, Department>();

           
            CreateMap<Department, DepartmentResponseDto>()
                .ForMember(dest => dest.HeadOfDepartmentName,
                           opt => opt.MapFrom(src => src.HeadOfDepartment.Name));
        }
    }
 }