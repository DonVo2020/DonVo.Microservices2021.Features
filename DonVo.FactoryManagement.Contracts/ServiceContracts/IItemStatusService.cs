using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.ItemStatus;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IItemStatusService
    {
        Task<WrapperItemStatusListVM> Add(ItemStatusVM vm);
        Task<WrapperItemStatusListVM> Delete(ItemStatusVM itemTemp);
        Task<WrapperItemStatusListVM> GetListPaged(GetDataListVM dataListVM);
        Task<WrapperItemStatusListVM> Update(string id, ItemStatusVM vm);
    }
}
