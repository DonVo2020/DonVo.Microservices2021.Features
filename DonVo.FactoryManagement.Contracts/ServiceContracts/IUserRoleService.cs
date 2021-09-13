using DonVo.FactoryManagement.Models.DbModels;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IUserRoleService
    {
        UserRole GetUserRole(string UserId);
        Task<UserRole> GetUserRoleAsync(string UserId);
    }
}
