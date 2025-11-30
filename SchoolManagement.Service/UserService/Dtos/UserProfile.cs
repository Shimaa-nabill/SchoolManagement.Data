using AutoMapper;
using SchoolManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.UserService.Dtos
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
           
            CreateMap<UserCreateDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

           
            CreateMap<UserUpdateDto, ApplicationUser>();

           
            CreateMap<ApplicationUser, UserResponseDto>();
        }
    }
}
