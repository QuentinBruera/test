using API.Repositories;
using Model.Dto;
using Model.Entities;

namespace API.Services
{
    public class DepartmentService
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentService(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartments()
        {
            IEnumerable<Department> departments = await _departmentRepository.GetDepartments();
            return departments.Select(d => MapEntityToDto(d));
        }

        public async Task<DepartmentDto> GetDepartment(int id)
        {
            Department department = await _departmentRepository.GetDepartment(id);
            if (department == null)
            {
                return null;
            }
            return MapEntityToDto(department);
        }

        public async Task<DepartmentDto> AddDepartment(DepartmentDto departmentDto)
        {
            Department newDepartment = departmentDto.ToEntity();
            newDepartment = await _departmentRepository.AddDepartment(newDepartment);
            return MapEntityToDto(newDepartment);
        }

        public async Task<bool> UpdateDepartment(DepartmentDto departmentDto)
        {
            Department department = departmentDto.ToEntity();
            return await _departmentRepository.UpdateDepartment(department);
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            return await _departmentRepository.DeleteDepartment(id);
        }

        private DepartmentDto MapEntityToDto(Department department)
        {
            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name
            };
        }
    }
}