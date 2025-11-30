using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Service.AdminService;
using SchoolManagement.Service.CourseService.Dtos;
using SchoolManagement.Service.DepartmentService.Dtos;
using SchoolManagement.Service.UserService.Dtos;

namespace SchoolManagement.Web.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _admin;
        public AdminController(IAdminService admin) { _admin = admin; }

        
        [HttpPost("departments")]
        public async Task<IActionResult> CreateDepartment(DepartmentCreateDto dto) => Ok(await _admin.CreateDepartmentAsync(dto));

        [HttpGet("departments")]
        public async Task<IActionResult> GetDepartments() => Ok(await _admin.GetDepartmentsAsync());

        [HttpPut("departments/{id}")]
        public async Task<IActionResult> UpdateDepartment(long id, DepartmentUpdateDto dto) => Ok(await _admin.UpdateDepartmentAsync(id, dto));

        [HttpDelete("departments/{id}")]
        public async Task<IActionResult> DeleteDepartment(long id) => Ok(await _admin.DeleteDepartmentAsync(id));

       
        [HttpPost("courses")]
        public async Task<IActionResult> CreateCourse(CourseCreateDto dto) => Ok(await _admin.CreateCourseAsync(dto));

        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses() => Ok(await _admin.GetCoursesAsync());

        [HttpPut("courses/{id}")]
        public async Task<IActionResult> UpdateCourse(long id, CourseUpdateDto dto) => Ok(await _admin.UpdateCourseAsync(id, dto));

        [HttpDelete("courses/{id}")]
        public async Task<IActionResult> DeleteCourse(long id) => Ok(await _admin.DeleteCourseAsync(id));

        
        [HttpPost("users")]
        public async Task<IActionResult> CreateUser(UserCreateDto dto) => Ok(await _admin.CreateUserAsync(dto));

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers() => Ok(await _admin.GetUsersAsync());

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(long id, UserUpdateDto dto) => Ok(await _admin.UpdateUserAsync(id, dto));

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(long id) => Ok(await _admin.DeleteUserAsync(id));
    }
}
