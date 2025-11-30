using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.SubmissionService.Dtos
{
    public class GradeDto
    {
        public long AssignmentId { get; set; }
        public string AssignmentTitle { get; set; }
        public double? Grade { get; set; }
        public string Remarks { get; set; }
    }
}
