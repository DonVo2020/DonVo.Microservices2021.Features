using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.EquipmentCategory;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IEquipmentCategoryService
    {
        Task<WrapperEquipmentCategoryListVM> GetListPaged(GetDataListVM dataListVM);
        Task<WrapperEquipmentCategoryListVM> Add(EquipmentCategoryVM vm);
        Task<WrapperEquipmentCategoryListVM> Update(string id, EquipmentCategoryVM vm);
        Task<WrapperEquipmentCategoryListVM> Delete(EquipmentCategoryVM itemTemp);
    }
}
