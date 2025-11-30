using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Data.Entities;
using SchoolManagement.Repository.Interfaces;
using SchoolManagement.Service.AssignmentService.Dtos;
using SchoolManagement.Service.AttendanceService.Dtos;
using SchoolManagement.Service.ClassService.Dtos;
using SchoolManagement.Service.SubmissionService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.TeacherService
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeacherService(IUnitOfWork uow, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
        }

        
        public async Task<AttendanceResponseDto> MarkAttendanceAsync(AttendanceCreateDto dto, long teacherId)
        {
            var cls = await _uow.Repository<Class>().GetByIdAsync(dto.ClassId);
            if (cls == null)
                throw new Exception("Class not found.");

            var student = await _userManager.FindByIdAsync(dto.StudentId.ToString());
            if (student == null)
                throw new Exception("Student not found.");

            if (dto.Status is not ("Present" or "Absent" or "Late"))
                throw new Exception("Invalid status.");

            var attendance = new Attendance
            {
                ClassId = dto.ClassId,
                StudentId = dto.StudentId,
                Status = dto.Status,
                Date = DateTime.UtcNow,
                TeacherId = teacherId
            };

            await _uow.Repository<Attendance>().AddAsync(attendance);
            await _uow.CompleteAsync();

            return _mapper.Map<AttendanceResponseDto>(attendance);
        }

        public async Task<IReadOnlyList<AttendanceResponseDto>> GetAttendanceHistoryAsync(long classId)
        {
            var all = await _uow.Repository<Attendance>().GetAllAsync();
            var result = all.Where(a => a.ClassId == classId).ToList();

            return _mapper.Map<IReadOnlyList<AttendanceResponseDto>>(result);
        }

       
        public async Task<AssignmentResponseDto> CreateAssignmentAsync(AssignmentCreateDto dto)
        {
            var cls = await _uow.Repository<Class>().GetByIdAsync(dto.ClassId);
            if (cls == null)
                throw new Exception("Class not found.");

            var assignment = _mapper.Map<Assignment>(dto);
            await _uow.Repository<Assignment>().AddAsync(assignment);
            await _uow.CompleteAsync();

            return _mapper.Map<AssignmentResponseDto>(assignment);
        }

        public async Task<IReadOnlyList<AssignmentResponseDto>> GetAssignmentsAsync(long classId)
        {
            var list = await _uow.Repository<Assignment>().GetAllAsync();
            var filtered = list.Where(a => a.ClassId == classId).ToList();

            return _mapper.Map<IReadOnlyList<AssignmentResponseDto>>(filtered);
        }

        //public async Task<bool> GradeSubmissionAsync(long assignmentId, GradeSubmissionDto dto)
        //{
        //    var submission = await _uow.Repository<Submission>().GetByIdAsync(dto.SubmissionId);
        //    if (submission == null)
        //        throw new Exception("Submission not found.");

        //    submission.Score = dto.Score;
        //    submission.GradedAt = DateTime.UtcNow;

        //    _uow.Repository<Submission>().Update(submission);
        //    await _uow.CompleteAsync();

        //    return true;
        //}


        public async Task<bool> SendNotificationAsync(NotificationCreateDto dto, long teacherId)
        {
            var notif = new Notification
            {
                Title = dto.Title,
                Message = dto.Message,
                RecipientRole = dto.RecipientRole,
                RecipientId = dto.RecipientId,
                CreatedDate = DateTime.UtcNow,
            };

            await _uow.Repository<Notification>().AddAsync(notif);
            await _uow.CompleteAsync();
            return true;
        }
    }
}
