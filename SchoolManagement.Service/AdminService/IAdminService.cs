using SchoolManagement.Service.CourseService.Dtos;
using SchoolManagement.Service.DepartmentService.Dtos;
using SchoolManagement.Service.UserService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.AdminService
{
    public interface IAdminService
    {
        
        Task<DepartmentResponseDto> CreateDepartmentAsync(DepartmentCreateDto dto);
        Task<DepartmentResponseDto> UpdateDepartmentAsync(long id, DepartmentUpdateDto dto);
        Task<bool> DeleteDepartmentAsync(long id);
        Task<IReadOnlyList<DepartmentResponseDto>> GetDepartmentsAsync(bool onlyActive = true);

        
        Task<CourseResponseDto> CreateCourseAsync(CourseCreateDto dto);
        Task<CourseResponseDto> UpdateCourseAsync(long id, CourseUpdateDto dto);
        Task<bool> DeleteCourseAsync(long id);
        Task<IReadOnlyList<CourseResponseDto>> GetCoursesAsync();

        Task<UserResponseDto> CreateUserAsync(UserCreateDto dto);
        Task<UserResponseDto> UpdateUserAsync(long id, UserUpdateDto dto);
        Task<bool> DeleteUserAsync(long id);
        Task<IReadOnlyList<UserResponseDto>> GetUsersAsync();
    }
}
