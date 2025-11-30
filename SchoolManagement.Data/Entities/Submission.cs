
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Entities
{
    public class Submission : BaseEntity
    {
        public long AssignmentId { get; set; }
        public long StudentId { get; set; }
        public ApplicationUser Student { get; set; }

        public DateTime SubmittedDate { get; set; }
        public string FileUrl { get; set; }
        public double? Grade { get; set; }
        public long? GradedByTeacherId { get; set; }
        public string Remarks { get; set; }
        public Assignment Assignment { get; set; }

    }
}
