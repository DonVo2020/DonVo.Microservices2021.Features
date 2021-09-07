using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserAsync(string id);
        Task<AppUser> GetUserByEmailAsync(string email);
        Task AddUserAsync(AppUser user, string password);
    }
}
