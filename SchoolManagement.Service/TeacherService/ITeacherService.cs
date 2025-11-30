using SchoolManagement.Service.AssignmentService.Dtos;
using SchoolManagement.Service.AttendanceService.Dtos;
using SchoolManagement.Service.ClassService.Dtos;
using SchoolManagement.Service.SubmissionService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.TeacherService
{
    public interface ITeacherService
    {
        Task<AttendanceResponseDto> MarkAttendanceAsync(AttendanceCreateDto dto, long teacherId);
        Task<IReadOnlyList<AttendanceResponseDto>> GetAttendanceHistoryAsync(long classId);

        Task<AssignmentResponseDto> CreateAssignmentAsync(AssignmentCreateDto dto);
        Task<IReadOnlyList<AssignmentResponseDto>> GetAssignmentsAsync(long classId);

       // Task<bool> GradeSubmissionAsync(long assignmentId, GradeSubmissionDto dto);

        Task<bool> SendNotificationAsync(NotificationCreateDto dto, long teacherId);
    }
}
