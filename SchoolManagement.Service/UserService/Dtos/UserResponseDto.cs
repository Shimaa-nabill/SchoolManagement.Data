using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.UserService.Dtos
{
    public class UserResponseDto 
    {
        public long Id { get; set; } 
        public string Name { get; set; } 
        public string Email { get; set; }
        public string Role { get; set; } 
    }
}
