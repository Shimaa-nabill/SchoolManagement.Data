using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

 
        public long? HeadOfDepartmentId { get; set; } // Teacher ID
        public ApplicationUser HeadOfDepartment { get; set; }

        public ICollection<Course> Courses { get; set; }
        public bool IsActive { get; set; }
    }

    
}
