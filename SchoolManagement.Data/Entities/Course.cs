using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
        public bool IsActive { get; set; } = true; // For soft delete

        public long DepartmentId { get; set; }
        
        public Department Department { get; set; }

        public ICollection<Class> Classes { get; set; }
    }
}