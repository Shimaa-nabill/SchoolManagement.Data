using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Data.Entities;
using SchoolManagement.Repository.Interfaces;
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
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AdminService(IUnitOfWork uow, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _uow = uow;
            _userManager = userManager;
            _mapper = mapper;
        }

        
        public async Task<DepartmentResponseDto> CreateDepartmentAsync(DepartmentCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name)) throw new ArgumentException("Name required");
            var deptRepo = _uow.Repository<Department>();

            var all = await deptRepo.GetAllAsync();
            if (all.Any(d => string.Equals(d.Name, dto.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Department name must be unique.");

           
            if (dto.HeadOfDepartmentId != 0)
            {
                var head = await _userManager.FindByIdAsync(dto.HeadOfDepartmentId.ToString());
                if (head == null || !(await _userManager.IsInRoleAsync(head, "Teacher")))
                    throw new InvalidOperationException("Head must be an existing Teacher.");
            }

            var ent = new Department
            {
                Name = dto.Name.Trim(),
                Description = dto.Description,
                HeadOfDepartmentId = dto.HeadOfDepartmentId == 0 ? (long?)null : dto.HeadOfDepartmentId,
                CreatedDate = DateTime.UtcNow
            };

            await deptRepo.AddAsync(ent);
            await _uow.CompleteAsync();
            return _mapper.Map<DepartmentResponseDto>(ent);
        }

        public async Task<DepartmentResponseDto> UpdateDepartmentAsync(long id, DepartmentUpdateDto dto)
        {
            var deptRepo = _uow.Repository<Department>();
            var ent = await deptRepo.GetByIdAsync(id);
            if (ent == null) throw new KeyNotFoundException("Department not found.");

            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                var allNames = await deptRepo.GetDistinctAsync(d => d.Name);
                if (allNames.Any(n => n != ent.Name && string.Equals(n, dto.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException("Department name must be unique.");
                ent.Name = dto.Name.Trim();
            }

            ent.Description = dto.Description ?? ent.Description;

            if (dto.HeadOfDepartmentId != 0)
            {
                var head = await _userManager.FindByIdAsync(dto.HeadOfDepartmentId.ToString());
                if (head == null || !(await _userManager.IsInRoleAsync(head, "Teacher")))
                    throw new InvalidOperationException("Head must be a Teacher.");
                ent.HeadOfDepartmentId = dto.HeadOfDepartmentId;
            }

            ent.UpdatedDate = DateTime.UtcNow;
            deptRepo.Update(ent);
            await _uow.CompleteAsync();
            return _mapper.Map<DepartmentResponseDto>(ent);
        }

        public async Task<bool> DeleteDepartmentAsync(long id)
        {
            var repo = _uow.Repository<Department>();
            var ent = await repo.GetByIdAsync(id);
            if (ent == null) return false;
            repo.Delete(ent);
            await _uow.CompleteAsync();
            return true;
        }

        public async Task<IReadOnlyList<DepartmentResponseDto>> GetDepartmentsAsync(bool onlyActive = true)
        {
            var list = await _uow.Repository<Department>().GetAllAsync();
            if (onlyActive) list = list.Where(d => d.IsActive).ToList();
            return _mapper.Map<IReadOnlyList<DepartmentResponseDto>>(list);
        }

       
        public async Task<CourseResponseDto> CreateCourseAsync(CourseCreateDto dto)
        {
            var courseRepo = _uow.Repository<Course>();
           
            var all = await courseRepo.GetAllAsync();
            if (all.Any(c => c.DepartmentId == dto.DepartmentId && string.Equals(c.Code, dto.Code, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Course code must be unique per department.");

            var course = new Course
            {
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                Credits = dto.Credits,
                DepartmentId = dto.DepartmentId,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };
            await courseRepo.AddAsync(course);
            await _uow.CompleteAsync();
            return _mapper.Map<CourseResponseDto>(course);
        }

        public async Task<CourseResponseDto> UpdateCourseAsync(long id, CourseUpdateDto dto)
        {
            var repo = _uow.Repository<Course>();
            var ent = await repo.GetByIdAsync(id);
            if (ent == null) throw new KeyNotFoundException("Course not found.");

           
            if (!string.IsNullOrWhiteSpace(dto.Code) && !string.Equals(ent.Code, dto.Code, StringComparison.OrdinalIgnoreCase))
            {
                var exists = (await repo.GetAllAsync()).Any(c => c.DepartmentId == dto.DepartmentId && string.Equals(c.Code, dto.Code, StringComparison.OrdinalIgnoreCase));
                if (exists) throw new InvalidOperationException("Course code must be unique per department.");
                ent.Code = dto.Code;
            }

            ent.Name = dto.Name ?? ent.Name;
            ent.Description = dto.Description ?? ent.Description;
            ent.Credits = dto.Credits;
            ent.DepartmentId = dto.DepartmentId;
            ent.UpdatedDate = DateTime.UtcNow;

            repo.Update(ent);
            await _uow.CompleteAsync();
            return _mapper.Map<CourseResponseDto>(ent);
        }

        public async Task<bool> DeleteCourseAsync(long id)
        {
            var repo = _uow.Repository<Course>();
            var ent = await repo.GetByIdAsync(id);
            if (ent == null) return false;
            repo.Delete(ent);
            await _uow.CompleteAsync();
            return true;
        }

        public async Task<IReadOnlyList<CourseResponseDto>> GetCoursesAsync()
        {
            var list = await _uow.Repository<Course>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<CourseResponseDto>>(list);
        }

        
        public async Task<UserResponseDto> CreateUserAsync(UserCreateDto dto)
        {
            var u = new ApplicationUser { UserName = dto.Email, Email = dto.Email, Name = dto.Name };
            var result = await _userManager.CreateAsync(u, dto.Password);
            if (!result.Succeeded) throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            if (!await _userManager.IsInRoleAsync(u, dto.Role))
                await _userManager.AddToRoleAsync(u, dto.Role);
            return _mapper.Map<UserResponseDto>(u);
        }

        public async Task<UserResponseDto> UpdateUserAsync(long id, UserUpdateDto dto)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) throw new KeyNotFoundException("User not found.");
            user.Name = dto.Name ?? user.Name;
            user.Email = dto.Email ?? user.Email;
            user.UserName = dto.Email ?? user.UserName;
            await _userManager.UpdateAsync(user);
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<bool> DeleteUserAsync(long id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return false;
            await _userManager.DeleteAsync(user);
            return true;
        }

        public async Task<IReadOnlyList<UserResponseDto>> GetUsersAsync()
        {
            var users = _userManager.Users.ToList();
            return _mapper.Map<IReadOnlyList<UserResponseDto>>(users);
        }
    }
}
