using SchoolManagement.Service.ClassService.Dtos;
using SchoolManagement.Service.PaginationService.Dtos;
using SchoolManagement.Service.StudentService.Dtos;
using SchoolManagement.Service.SubmissionService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.StudentService
{
    public interface IStudentService
    {
        Task<IReadOnlyList<StudentClassDto>> GetStudentClassesAsync(long studentId, int page, int pageSize);
        Task<IReadOnlyList<AttendanceDto>> GetAttendanceAsync(long studentId, int page, int pageSize);
        Task<IReadOnlyList<AssignmentDto>> GetAssignmentsAsync(long studentId, int page, int pageSize);
        Task<SubmissionDto> SubmitAssignmentAsync(long assignmentId, long studentId, string fileUrl);
        Task<IReadOnlyList<GradeDto>> GetGradesAsync(long studentId, int page, int pageSize);
       // Task<IReadOnlyList<NotificationCreateDto>> GetNotificationsAsync(long studentId, int page, int pageSize);
    }



}
