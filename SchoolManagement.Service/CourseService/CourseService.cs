using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SchoolManagement.Data.Entities;
using SchoolManagement.Repository.Interfaces;
using SchoolManagement.Service.CourseService.Dtos;
using SchoolManagement.Service.PaginationService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private const string CourseListCacheKey = "CourseListCache";

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<CourseResponseDto> GetByIdAsync(long id)
        {
            var courses = await _unitOfWork.Repository<Course>()
                .GetAllWithIncludeAsync(c => c.Department);

            var course = courses.FirstOrDefault(c => c.Id == id);
            if (course == null) return null;

            return _mapper.Map<CourseResponseDto>(course);
        }

        public async Task<PagedResponse<CourseResponseDto>> GetAllAsync(int page, int pageSize)
        {
        
            if (!_cache.TryGetValue(CourseListCacheKey, out List<Course> allCourses))
            {
                var coursesFromDb = await _unitOfWork.Repository<Course>()
                    .GetAllWithIncludeAsync(c => c.Department);

                allCourses = coursesFromDb.ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                _cache.Set(CourseListCacheKey, allCourses, cacheOptions);
            }

            var totalCount = allCourses.Count;
            var pagedCourses = allCourses.Skip((page - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToList();

            return new PagedResponse<CourseResponseDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = _mapper.Map<List<CourseResponseDto>>(pagedCourses)
            };
        }

        public async Task<CourseResponseDto> CreateAsync(CourseCreateDto dto)
        {
            var course = _mapper.Map<Course>(dto);
            await _unitOfWork.Repository<Course>().AddAsync(course);
            await _unitOfWork.CompleteAsync();

           
            _cache.Remove(CourseListCacheKey);

            return _mapper.Map<CourseResponseDto>(course);
        }

        public async Task<CourseResponseDto> UpdateAsync(long id, CourseUpdateDto dto)
        {
            var course = await _unitOfWork.Repository<Course>().GetByIdAsync(id);
            if (course == null) throw new Exception("Course not found");

            _mapper.Map(dto, course);
            _unitOfWork.Repository<Course>().Update(course);
            await _unitOfWork.CompleteAsync();

            _cache.Remove(CourseListCacheKey);

            return _mapper.Map<CourseResponseDto>(course);
        }

        public async Task DeleteAsync(long id)
        {
            var course = await _unitOfWork.Repository<Course>().GetByIdAsync(id);
            if (course == null) throw new Exception("Course not found");

            _unitOfWork.Repository<Course>().Delete(course);
            await _unitOfWork.CompleteAsync();

            _cache.Remove(CourseListCacheKey);
        }
    }
    }
