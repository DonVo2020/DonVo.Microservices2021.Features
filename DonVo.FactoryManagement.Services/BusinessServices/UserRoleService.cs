using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        private readonly IUtilService _utilService;

        public UserRoleService(IRepositoryWrapper repositoryWrapper, IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }

        public  UserRole GetUserRole(string UserId)
        {
            var userRole = _repositoryWrapper
                .UserRole
                .FindAll()
                .Where(x => x.UserId == UserId)
                .Include(x => x.UserAuthInfo)
                .Include(x => x.Role)
                .ToList().FirstOrDefault();
            return userRole; 
        }
        public async Task<UserRole> GetUserRoleAsync(string UserId)
        {
            var userRole = await _repositoryWrapper
                .UserRole
                .FindAll()
                .Where(x => x.UserId == UserId)
                .Include(x => x.UserAuthInfo)
                .Include(x => x.Role)
                .ToListAsync();

            return userRole.FirstOrDefault();
        }
    }
}
