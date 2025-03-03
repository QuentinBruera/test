using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entities;

namespace API.Repositories
{
    public class DepartmentRepository
    {
        private readonly DirectoryDbContext _context;

        public DepartmentRepository(DirectoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _context.Department.ToListAsync();
        }

        public async Task<Department> GetDepartment(int id)
        {
            return await _context.Department.FindAsync(id);
        }

        public async Task<Department> AddDepartment(Department department)
        {
            _context.Department.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<bool> UpdateDepartment(Department department)
        {
            try
            {
                var existingDepartment = await _context.Department.FindAsync(department.Id);
                if (existingDepartment == null)
                {
                    return false;
                }

                // Mise à jour des propriétés
                existingDepartment.Name = department.Name;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return false;
            }

            // Vérifier s'il y a des employés associés à ce département
            bool hasEmployees = await _context.Employee.AnyAsync(e => e.DepartmentId == id);
            if (hasEmployees)
            {
                // Si des employés sont associés, nous pouvons choisir de ne pas supprimer
                // ou de gérer différemment (par exemple, déplacer les employés)
                return false;
            }

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}