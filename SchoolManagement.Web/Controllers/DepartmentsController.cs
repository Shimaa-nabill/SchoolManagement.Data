using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Service.DepartmentService.Dtos;
using SchoolManagement.Service.DepartmentService;

namespace SchoolManagement.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(DepartmentCreateDto dto)
        {
            var result = await _departmentService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(long id, DepartmentUpdateDto dto)
        {
            var result = await _departmentService.UpdateAsync(id, dto);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _departmentService.DeleteAsync(id);
            return result ? Ok() : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
            => Ok(await _departmentService.GetByIdAsync(id));

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _departmentService.GetAllAsync());
    }
}
