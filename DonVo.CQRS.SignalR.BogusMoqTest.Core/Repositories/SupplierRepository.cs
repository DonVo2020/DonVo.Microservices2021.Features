using DonVo.CQRS.SignalR.BogusMoqTest.Core.Data;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly DataContext _context;

        public SupplierRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync(CancellationToken cancellationToken)
        {
            return await _context.Suppliers.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task AddSupplierAsync(Supplier supplier, CancellationToken cancellationToken)
        {
            await _context.AddAsync(supplier, cancellationToken);
        }

        public async Task<Supplier> GetSupplierByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == id.ToString(), cancellationToken);
        }

        public async Task<bool> SupplierExistsAsync(int supplierId, CancellationToken cancellationToken = default)
        {
            return await _context.Suppliers.AnyAsync(x => x.Id == supplierId.ToString(), cancellationToken);
        }
    }
}
