using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels.ApiResourceMapping;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class ApiResourceMappingService : IApiResourceMappingService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUtilService _utilService;

        public ApiResourceMappingService(IRepositoryWrapper repositoryWrapper, IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }
        public async Task<ApiResourceMappingVM> GetApiResourceMappingAsync(string Controller,string ActionMethod)
        {
            var apiResource = await _repositoryWrapper
                .ApiResourceMapping
                .FindAll()
                .Where(x => x.Controller == Controller && x.Action == ActionMethod)
                .ToListAsync();
            ApiResourceMappingVM data = _utilService.Mapper.Map<ApiResourceMapping, ApiResourceMappingVM>(apiResource.FirstOrDefault());
            return data;
        }

        public  ApiResourceMappingVM GetApiResourceMapping(string Controller, string ActionMethod)
        {
            var apiResource =  _repositoryWrapper
                .ApiResourceMapping
                .FindAll()
                .Where(x => x.Controller == Controller && x.Action == ActionMethod)
                .ToList();
            ApiResourceMappingVM data = _utilService.Mapper.Map<ApiResourceMapping, ApiResourceMappingVM>(apiResource.FirstOrDefault());
            return data;
        }
    }
}
