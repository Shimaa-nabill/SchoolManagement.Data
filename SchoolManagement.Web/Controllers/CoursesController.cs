using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Service.CourseService.Dtos;
using SchoolManagement.Service.CourseService;

namespace SchoolManagement.Web.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return NotFound(new { Message = "Course not found." });

            return Ok(course);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var result = await _courseService.GetAllAsync(page, pageSize);
            return Ok(result);
        }

       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var course = await _courseService.CreateAsync(dto);
            return Ok(course);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] CourseUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var course = await _courseService.UpdateAsync(id, dto);
                return Ok(course);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

  
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _courseService.DeleteAsync(id);
                return Ok(new { Message = "Course deleted successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
