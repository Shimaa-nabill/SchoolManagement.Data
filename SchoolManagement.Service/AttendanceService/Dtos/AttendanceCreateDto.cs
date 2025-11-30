using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.AttendanceService.Dtos
{
    public class AttendanceCreateDto
    {
        public long ClassId { get; set; }
        public long StudentId { get; set; }
        public string Status { get; set; }
    }
}
