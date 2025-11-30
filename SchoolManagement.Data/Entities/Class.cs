namespace SchoolManagement.Data.Entities
{
    public class Class : BaseEntity
    {
        public string Name { get; set; }
        public string Semester { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;

        
        public long CourseId { get; set; }
        public long TeacherId { get; set; }

       
        public Course Course { get; set; }
        public ApplicationUser Teacher { get; set; }

        public ICollection<StudentClass> StudentClasses { get; set; } // Enrollments
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
}