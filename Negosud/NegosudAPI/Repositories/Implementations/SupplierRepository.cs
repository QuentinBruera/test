using Microsoft.EntityFrameworkCore;
using NegosudAPI.Repositories.Interfaces;
using NegosudModel.Context;
using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Implementations
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly NegosudContext _context;

        public SupplierRepository(NegosudContext context)
        {
            _context = context;
        }

        public async Task<Supplier?> GetSupplier(int id)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task CreateSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSupplier(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSupplier(int id)
        {
            Supplier? supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null) _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
        }
    }
}
