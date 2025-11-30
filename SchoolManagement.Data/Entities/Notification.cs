using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Entities
{
    public class Notification : BaseEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string RecipientRole { get; set; } // Admin / Teacher / Student
        public long? RecipientId { get; set; } 
        public bool IsRead { get; set; } = false;

        public ApplicationUser Recipient { get; set; }
    }
}
