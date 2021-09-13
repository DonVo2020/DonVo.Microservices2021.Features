using DonVo.FactoryManagement.Models.ViewModels;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.IBusinessServiceWrapper
{
    public interface IPurchaseWrapperService
    {
        Task<InitialLoadDataVM> GetPurchaseInitialData(GetDataListVM getDataListVM);
        Task<InitialLoadDataVM> GetSalesInitialData(GetDataListVM getDataListVM);
        Task<InitialLoadDataVM> GetPaymentInitialData(GetDataListVM getDataListVM);
        Task<InitialLoadDataVM> GetProductionInitialData(GetDataListVM getDataListVM);
        Task<InitialLoadDataVM> GetStaffInitialData(GetDataListVM getDataListVM);
    }
}
