using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.StudentService.Dtos
{
    public class AttendanceDto
    {
        public long ClassId { get; set; }
        public string ClassName { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
