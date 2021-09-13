using System.Threading;
using System.Threading.Tasks;
using DonVo.SignalR_MediatR.Core.Data;
using DonVo.SignalR_MediatR.Core.Interfaces;

namespace DonVo.SignalR_MediatR.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
