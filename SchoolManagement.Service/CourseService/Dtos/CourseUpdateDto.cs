using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.CourseService.Dtos
{
    public class CourseUpdateDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public long DepartmentId { get; set; }
        public int Credits { get; set; }
    }
}
