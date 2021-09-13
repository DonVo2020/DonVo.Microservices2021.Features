using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Item;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IItemService
    {
        Task<WrapperItemListVM> GetListPaged(GetDataListVM dataListVM);
        Task<WrapperItemListVM> Add(ItemVM vm);
        Task<WrapperItemListVM> Update(string id, ItemVM vm);
        Task<WrapperItemListVM> Delete(ItemVM itemTemp);
    }
}
