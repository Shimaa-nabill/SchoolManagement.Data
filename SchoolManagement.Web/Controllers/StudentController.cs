using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Service.StudentService;

namespace SchoolManagement.Web.Controllers
{
    [ApiController]
    [Route("api/student")]
    [Authorize(Roles = "Student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("classes")]
        public async Task<IActionResult> GetClasses([FromQuery] long studentId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _studentService.GetStudentClassesAsync(studentId, page, pageSize);
            return Ok(result);
        }

        [HttpGet("attendance")]
        public async Task<IActionResult> GetAttendance([FromQuery] long studentId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _studentService.GetAttendanceAsync(studentId, page, pageSize);
            return Ok(result);
        }

        [HttpGet("assignments")]
        public async Task<IActionResult> GetAssignments([FromQuery] long studentId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _studentService.GetAssignmentsAsync(studentId, page, pageSize);
            return Ok(result);
        }

        [HttpPost("assignments/{id}/submit")]
        public async Task<IActionResult> SubmitAssignment(long id, [FromQuery] long studentId, [FromBody] string fileUrl)
        {
            var result = await _studentService.SubmitAssignmentAsync(id, studentId, fileUrl);
            return Ok(result);
        }

        [HttpGet("grades")]
        public async Task<IActionResult> GetGrades([FromQuery] long studentId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _studentService.GetGradesAsync(studentId, page, pageSize);
            return Ok(result);
        }

        //[HttpGet("notifications")]
        //public async Task<IActionResult> GetNotifications([FromQuery] long studentId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        //{
        //    var result = await _studentService.GetNotificationsAsync(studentId, page, pageSize);
        //    return Ok(result);
        //}
    }
}