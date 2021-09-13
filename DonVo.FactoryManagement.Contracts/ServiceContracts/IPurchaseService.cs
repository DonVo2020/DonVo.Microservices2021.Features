using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Purchase;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IPurchaseService
    {
        Task<WrapperPurchaseListVM> AddPurchaseAsync(PurchaseVM purchaseVM);
        Task<WrapperPurchaseReturnVM> AddPurchaseReturnAsync(PurchaseReturnVM vm);
        Task<WrapperPurchaseListVM> DeletePurchaseAsync(PurchaseVM vm);
        Task<WrapperPurchaseReturnVM> DeletePurchaseReturnAsync(PurchaseReturnVM vm);
        Task<WrapperPurchaseListVM> GetAllPurchaseAsync(GetDataListVM getDataListVM);
        Task<WrapperPurchaseReturnVM> GetAllPurchaseReturnAsync(GetDataListVM getDataListVM);
    }
}
