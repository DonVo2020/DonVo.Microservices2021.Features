using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.ItemCategoryView;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IItemCategoryService
    {
        Task<WrapperItemCategoryListVM> GetListPaged(GetDataListVM dataListVM);
        Task<WrapperItemCategoryListVM> Add(ItemCategoryVM vm);
        Task<WrapperItemCategoryListVM> Update(string id, ItemCategoryVM vm);
        Task<WrapperItemCategoryListVM> Delete(ItemCategoryVM itemTemp);
    }
}
