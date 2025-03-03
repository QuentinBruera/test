using API.Repositories;
using Model.Dto;
using Model.Entities;

namespace API.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetEmployees();
            return employees.Select(e => MapEntityToDto(e));
        }

        public async Task<EmployeeDto> GetEmployee(int id)
        {
            Employee employee = await _employeeRepository.GetEmployee(id);
            if (employee == null)
            {
                return null;
            }
            return MapEntityToDto(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> SearchEmployees(string searchTerm)
        {
            IEnumerable<Employee> employees = await _employeeRepository.SearchEmployees(searchTerm);
            return employees.Select(e => MapEntityToDto(e));
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesBySite(int siteId)
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetEmployeesBySite(siteId);
            return employees.Select(e => MapEntityToDto(e));
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartment(int departmentId)
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetEmployeesByDepartment(departmentId);
            return employees.Select(e => MapEntityToDto(e));
        }

        public async Task<EmployeeDto> AddEmployee(EmployeeDto employeeDto)
        {
            Employee newEmployee = employeeDto.ToEntity();
            newEmployee = await _employeeRepository.AddEmployee(newEmployee);
            return MapEntityToDto(newEmployee);
        }

        public async Task<bool> UpdateEmployee(EmployeeDto employeeDto)
        {
            Employee employee = employeeDto.ToEntity();
            return await _employeeRepository.UpdateEmployee(employee);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            return await _employeeRepository.DeleteEmployee(id);
        }

        private EmployeeDto MapEntityToDto(Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Phone = employee.Phone,
                MobilePhone = employee.MobilePhone,
                Email = employee.Email,
                DepartmentId = employee.DepartmentId,
                SiteId = employee.SiteId
            };
        }
    }
}