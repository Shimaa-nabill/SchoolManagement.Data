using AutoMapper;
using SchoolManagement.Data.Entities;
using SchoolManagement.Repository.Interfaces;
using SchoolManagement.Service.DepartmentService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DepartmentResponseDto> CreateAsync(DepartmentCreateDto dto)
        {
            var entity = _mapper.Map<Department>(dto);

            await _unitOfWork.Repository<Department>().AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<DepartmentResponseDto>(entity);
        }

        public async Task<DepartmentResponseDto> UpdateAsync(long id, DepartmentUpdateDto dto)
        {
            var repo = _unitOfWork.Repository<Department>();
            var entity = await repo.GetByIdAsync(id);

            if (entity == null)
                return null;

            _mapper.Map(dto, entity);

            repo.Update(entity);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<DepartmentResponseDto>(entity);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var repo = _unitOfWork.Repository<Department>();
            var entity = await repo.GetByIdAsync(id);

            if (entity == null)
                return false;

            repo.Delete(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<DepartmentResponseDto> GetByIdAsync(long id)
        {
            var entity = await _unitOfWork.Repository<Department>().GetByIdAsync(id);

            return entity == null ? null : _mapper.Map<DepartmentResponseDto>(entity);
        }

        public async Task<IReadOnlyList<DepartmentResponseDto>> GetAllAsync()
        {
            var data = await _unitOfWork.Repository<Department>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<DepartmentResponseDto>>(data);
        }
    }

}
