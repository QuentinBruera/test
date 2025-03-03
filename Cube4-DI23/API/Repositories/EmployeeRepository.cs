using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entities;

namespace API.Repositories
{
    public class EmployeeRepository
    {
        private readonly DirectoryDbContext _context;

        public EmployeeRepository(DirectoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employee
                .Include(e => e.Department)
                .Include(e => e.Site)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _context.Employee
                .Include(e => e.Department)
                .Include(e => e.Site)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Employee>> SearchEmployees(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();

            return await _context.Employee
                .Include(e => e.Department)
                .Include(e => e.Site)
                .Where(e => e.LastName.ToLower().Contains(searchTerm) ||
                            e.FirstName.ToLower().Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesBySite(int siteId)
        {
            return await _context.Employee
                .Include(e => e.Department)
                .Include(e => e.Site)
                .Where(e => e.SiteId == siteId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartment(int departmentId)
        {
            return await _context.Employee
                .Include(e => e.Department)
                .Include(e => e.Site)
                .Where(e => e.DepartmentId == departmentId)
                .ToListAsync();
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            try
            {
                var existingEmployee = await _context.Employee.FindAsync(employee.Id);
                if (existingEmployee == null)
                {
                    return false;
                }

                // Mise à jour des propriétés
                existingEmployee.LastName = employee.LastName;
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.MobilePhone = employee.MobilePhone;
                existingEmployee.Email = employee.Email;
                existingEmployee.DepartmentId = employee.DepartmentId;
                existingEmployee.SiteId = employee.SiteId;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return false;
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}