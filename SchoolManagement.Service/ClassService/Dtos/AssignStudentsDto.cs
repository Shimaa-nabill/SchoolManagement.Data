using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.ClassService.Dtos
{
    public class AssignStudentsDto
    {
        public long ClassId { get; set; }
        public List<long> StudentIds { get; set; }
    }
}
