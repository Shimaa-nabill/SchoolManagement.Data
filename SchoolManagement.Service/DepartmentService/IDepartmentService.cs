using SchoolManagement.Service.DepartmentService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.DepartmentService
{
    public interface IDepartmentService
    {
        Task<DepartmentResponseDto> CreateAsync(DepartmentCreateDto dto);
        Task<DepartmentResponseDto> UpdateAsync(long id, DepartmentUpdateDto dto);
        Task<bool> DeleteAsync(long id);
        Task<DepartmentResponseDto> GetByIdAsync(long id);
        Task<IReadOnlyList<DepartmentResponseDto>> GetAllAsync();
    }
}
