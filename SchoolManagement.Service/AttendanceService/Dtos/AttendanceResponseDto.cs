using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.AttendanceService.Dtos
{
    public class AttendanceResponseDto
    {
        public long Id { get; set; }
        public string StudentName { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}
