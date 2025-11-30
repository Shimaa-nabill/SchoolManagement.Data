using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.AssignmentService.Dtos
{
    public class GradeAssignmentDto
    {
        public long SubmissionId { get; set; }
        public double Grade { get; set; }
    }
}
