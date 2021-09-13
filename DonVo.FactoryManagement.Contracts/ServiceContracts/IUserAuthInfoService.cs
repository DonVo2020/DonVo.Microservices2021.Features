using DonVo.FactoryManagement.Models.ViewModels.Auth;
using DonVo.FactoryManagement.Models.ViewModels.UserAuthInfo;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IUserAuthInfoService
    {
        Task<WrapperUserAuthInfoListVM> Add(UserAuthInfoVM vm);
        JwtSecurityToken DecodeJwtToken(string token);
        Task<WrapperUserAuthInfoListVM> Delete(UserAuthInfoVM itemTemp);
        Task<WrapperUserAuthInfoListVM> GetListPaged(DonVo.FactoryManagement.Models.ViewModels.GetDataListVM dataListVM);
        LoginResponseVM IsUserAuthentic(LoginVM loginVM);
        bool IsUserExist(LoginVM loginVM);
        Task<WrapperUserAuthInfoListVM> Update(string id, UserAuthInfoVM vm);
        bool ValidateCurrentToken(string token);
    }
}
