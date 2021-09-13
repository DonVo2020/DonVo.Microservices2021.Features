using System.Threading.Tasks;
using DonVo.SignalR_MediatR.Core.Models.Entities;

namespace DonVo.SignalR_MediatR.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserAsync(string id);
        Task<AppUser> GetUserByEmailAsync(string email);
        Task AddUserAsync(AppUser user, string password);
    }
}
