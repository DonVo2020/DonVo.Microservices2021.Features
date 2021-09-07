using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces
{
    public interface ISupplierRepository
    {
        /// <summary>
        /// Get Suppliers without tracking
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Suppliers including related entities without tracking</returns>
        Task<IEnumerable<Supplier>> GetSuppliersAsync(CancellationToken cancellationToken);
        Task AddSupplierAsync(Supplier supplier, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a Supplier by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns>A Suppliers</returns>
        Task<Supplier> GetSupplierByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Checks whether a Supplier with the specified Supplier id and user id exists
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <param name="userId"></param>
        /// <returns>True or false whether the Supplier exists or not</returns>
        Task<bool> SupplierExistsAsync(int supplierId, CancellationToken cancellationToken = default);
    }
}
