using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.SubmissionService.Dtos
{
    public class SubmissionDto
    {
        public long AssignmentId { get; set; }
        public long StudentId { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string FileUrl { get; set; }
        public double? Grade { get; set; }
        public string Remarks { get; set; }
    }
}
