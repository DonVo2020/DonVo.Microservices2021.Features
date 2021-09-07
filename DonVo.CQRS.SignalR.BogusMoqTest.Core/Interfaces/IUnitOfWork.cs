using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves any changes made tracked by EF. Cancellationtoken is default but should be passed in
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>If any changes were saved</returns>
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
