using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Role;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IRoleService
    {
        Task<WrapperRoleListVM> Add(RoleVM vm);
        Task<WrapperRoleListVM> Delete(RoleVM itemTemp);
        Task<WrapperRoleListVM> GetListPaged(GetDataListVM dataListVM);
        Task<WrapperRoleListVM> Update(string id, RoleVM vm);
    }
}
