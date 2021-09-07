using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces
{
    public interface ICollectionPointRepository
    {
        Task<CollectionPoint> GetCollectionPointAsync(int employeeId, int collectionPointId, CancellationToken cancellationToken = default);
        //Task<IEnumerable<CollectionPoint>> GetCollectionPointsAsync(string userId, CollectionPointParams CollectionPointParams, CancellationToken cancellationToken = default);
        Task AddCollectionPointAsync(CollectionPoint CollectionPoint, CancellationToken cancellationToken);

        /// <summary>
        /// Gets CollectionPoints that matches the passed in list of CollectionPoint ids while ignoring query filters
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="CollectionPointIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>CollectionPoints with the specified ids if found</returns>
        Task<IReadOnlyCollection<CollectionPoint>> GetCollectionPointsAsync(int employeeId, IEnumerable<int> collectionPointIds, CancellationToken cancellationToken = default);
        void RemoveCollectionPoints(IEnumerable<CollectionPoint> CollectionPoints);
    }
}
