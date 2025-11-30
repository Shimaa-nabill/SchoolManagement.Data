using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.ClassService.Dtos
{
    public class NotificationCreateDto
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string RecipientRole { get; set; }  // Student / Teacher / Admin
        public long? RecipientId { get; set; }
    }
}
