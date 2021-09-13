using DonVo.FactoryManagement.Models.ViewModels.Factory;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IFactoryService
    {
        Task<WrapperFactoryListVM> Add(FactoryVM vm);
        Task<WrapperFactoryListVM> Delete(FactoryVM itemTemp);
        Task<WrapperFactoryListVM> GetListPaged(DonVo.FactoryManagement.Models.ViewModels.GetDataListVM dataListVM);
        Task<WrapperFactoryListVM> Update(string id, FactoryVM vm);
    }
}
