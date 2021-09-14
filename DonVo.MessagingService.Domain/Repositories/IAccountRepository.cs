using DonVo.MessagingService.Domain.Model;
using DonVo.MessagingService.Domain.Repositories.Base;

namespace DonVo.MessagingService.Domain.Repositories
{
    public interface IAccountRepository : IGenericRepository<AccountModel>
    {
        void BlockUser(string userId, string opponent);
        void UpdateLastLogin(string userId);
        bool IsBlocked(string userId, string opponent);
    }
}
