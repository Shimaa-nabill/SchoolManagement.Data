using AutoMapper;
using SchoolManagement.Data.Entities;
using SchoolManagement.Repository.Interfaces;
using SchoolManagement.Service.PaginationService.Dtos;
using SchoolManagement.Service.StudentService.Dtos;
using SchoolManagement.Service.SubmissionService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<StudentClassDto>> GetStudentClassesAsync(long studentId, int page, int pageSize)
        {
            var classes = _unitOfWork.Repository<StudentClass>()
                .GetAllAsync().Result
                .Where(sc => sc.StudentId == studentId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return _mapper.Map<List<StudentClassDto>>(classes);
        }

        public async Task<IReadOnlyList<AttendanceDto>> GetAttendanceAsync(long studentId, int page, int pageSize)
        {
            var attendance = _unitOfWork.Repository<Attendance>()
                .GetAllAsync().Result
                .Where(a => a.StudentId == studentId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return _mapper.Map<List<AttendanceDto>>(attendance);
        }

        public async Task<IReadOnlyList<AssignmentDto>> GetAssignmentsAsync(long studentId, int page, int pageSize)
        {
            var classes = _unitOfWork.Repository<StudentClass>()
                .GetAllAsync().Result
                .Where(sc => sc.StudentId == studentId)
                .Select(sc => sc.ClassId)
                .ToList();

            var assignments = _unitOfWork.Repository<Assignment>()
                .GetAllAsync().Result
                .Where(a => classes.Contains(a.ClassId))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return _mapper.Map<List<AssignmentDto>>(assignments);
        }

        public async Task<SubmissionDto> SubmitAssignmentAsync(long assignmentId, long studentId, string fileUrl)
        {
            var submission = new Submission
            {
                AssignmentId = assignmentId,
                StudentId = studentId,
                FileUrl = fileUrl,
                SubmittedDate = DateTime.UtcNow
            };

            await _unitOfWork.Repository<Submission>().AddAsync(submission);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<SubmissionDto>(submission);
        }

        public async Task<IReadOnlyList<GradeDto>> GetGradesAsync(long studentId, int page, int pageSize)
        {
            var grades = _unitOfWork.Repository<Submission>()
                .GetAllAsync().Result
                .Where(s => s.StudentId == studentId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return _mapper.Map<List<GradeDto>>(grades);
        }

       
    }


}
