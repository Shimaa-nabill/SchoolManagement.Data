namespace SchoolManagement.Data.Entities
{
    public class Attendance : BaseEntity
    {
        public long ClassId { get; set; }
        public long StudentId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } // Present / Absent / Late
        public long TeacherId { get; set; }
        public Class Class { get; set; }
        public ApplicationUser Student { get; set; }
        public ApplicationUser Teacher { get; set; }
    }
}