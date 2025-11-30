namespace SchoolManagement.Data.Entities
{
    public class StudentClass : BaseEntity
    {
        public long StudentId { get; set; }
        public long ClassId { get; set; }
        public DateTime EnrollmentDate { get; set; }

       
        public ApplicationUser Student { get; set; }
        public Class Class { get; set; }
    }
}