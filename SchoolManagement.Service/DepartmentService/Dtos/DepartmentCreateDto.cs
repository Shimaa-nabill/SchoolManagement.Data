using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.DepartmentService.Dtos
{
    public class DepartmentCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long HeadOfDepartmentId { get; set; } // Teacher
    }
}
