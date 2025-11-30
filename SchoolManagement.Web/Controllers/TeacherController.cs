using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Data.Entities;
using SchoolManagement.Service.AssignmentService.Dtos;
using SchoolManagement.Service.AttendanceService.Dtos;
using SchoolManagement.Service.ClassService.Dtos;
using SchoolManagement.Service.SubmissionService.Dtos;
using SchoolManagement.Service.TeacherService;

namespace SchoolManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Route("api/teacher")]
    [Authorize(Roles = "Teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeacherController(ITeacherService teacherService, UserManager<ApplicationUser> userManager)
        {
            _teacherService = teacherService;
            _userManager = userManager;
        }

        private long GetUserId()
        {
            return long.Parse(User.FindFirst("uid")!.Value);
        }

        
       
        [HttpPost("attendance")]
        public async Task<IActionResult> MarkAttendance([FromBody] AttendanceCreateDto dto)
        {
            long teacherId = GetUserId();

            var result = await _teacherService.MarkAttendanceAsync(dto, teacherId);
            return Ok(result);
        }

        [HttpGet("attendance/{classId}")]
        public async Task<IActionResult> AttendanceHistory(long classId)
        {
            var result = await _teacherService.GetAttendanceHistoryAsync(classId);
            return Ok(result);
        }

        [HttpPost("assignments")]
        public async Task<IActionResult> CreateAssignment([FromBody] AssignmentCreateDto dto)
        {
            var result = await _teacherService.CreateAssignmentAsync(dto);
            return Ok(result);
        }

       
        [HttpGet("assignments/{classId}")]
        public async Task<IActionResult> GetAssignments(long classId)
        {
            var result = await _teacherService.GetAssignmentsAsync(classId);
            return Ok(result);
        }

       
        [HttpPost("notifications")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationCreateDto dto)
        {
            long teacherId = GetUserId();
            var result = await _teacherService.SendNotificationAsync(dto, teacherId);
            return Ok(result);
        }
    }
}
