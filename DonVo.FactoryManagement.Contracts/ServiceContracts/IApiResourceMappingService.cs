using DonVo.FactoryManagement.Models.ViewModels.ApiResourceMapping;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IApiResourceMappingService
    {
        Task<ApiResourceMappingVM> GetApiResourceMappingAsync(string Controller, string ActionMethod);
        ApiResourceMappingVM GetApiResourceMapping(string Controller, string ActionMethod);
    
    }
}
