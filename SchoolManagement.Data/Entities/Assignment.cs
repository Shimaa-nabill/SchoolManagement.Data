

namespace SchoolManagement.Data.Entities
{
    public class Assignment : BaseEntity
    {
        public long ClassId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public Class Class { get; set; }

        public long TeacherId { get; set; }
        public ApplicationUser Teacher { get; set; }

        public ICollection<Submission> Submissions { get; set; }
    }
}