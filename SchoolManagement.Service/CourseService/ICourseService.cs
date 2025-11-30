using SchoolManagement.Service.CourseService.Dtos;
using SchoolManagement.Service.PaginationService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.CourseService
{
    public interface ICourseService
    {
        Task<CourseResponseDto> GetByIdAsync(long id);
        Task<PagedResponse<CourseResponseDto>> GetAllAsync(int page, int pageSize);
        Task<CourseResponseDto> CreateAsync(CourseCreateDto dto);
        Task<CourseResponseDto> UpdateAsync(long id, CourseUpdateDto dto);
        Task DeleteAsync(long id);
    }
}
