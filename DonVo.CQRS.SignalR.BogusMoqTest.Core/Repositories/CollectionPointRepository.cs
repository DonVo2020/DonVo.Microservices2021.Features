using DonVo.CQRS.SignalR.BogusMoqTest.Core.Data;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Repositories
{
    public class CollectionPointRepository : ICollectionPointRepository
    {
        private readonly DataContext _context;

        public CollectionPointRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddCollectionPointAsync(CollectionPoint CollectionPoint, CancellationToken cancellationToken)
        {
            await _context.CollectionPoints.AddAsync(CollectionPoint, cancellationToken);
        }

        public async Task<CollectionPoint> GetCollectionPointAsync(int employeeId, int CollectionPointId, CancellationToken cancellationToken = default)
        {
            return await _context.CollectionPoints.FirstOrDefaultAsync(x => x.Id == CollectionPointId && x.EmployeeId == employeeId, cancellationToken);
        }

        public async Task<IReadOnlyCollection<CollectionPoint>> GetCollectionPointsAsync(int employeeId, IEnumerable<int> collectionPointIds, CancellationToken cancellationToken = default)
        {
            return await _context.CollectionPoints.Where(x => x.EmployeeId == employeeId)
                .Where(collectionPoint => collectionPointIds.Contains(collectionPoint.Id)).IgnoreQueryFilters().ToListAsync(cancellationToken);
        }

        public void RemoveCollectionPoints(IEnumerable<CollectionPoint> collectionPoints)
        {
            _context.RemoveRange(collectionPoints);
        }
    }
}
