using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Payment;
using DonVo.FactoryManagement.Models.ViewModels.Supplier;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface ISupplierService
    {
        Task<WrapperSupplierListVM> Add(SupplierVM ViewModel);
        Task<WrapperSupplierListVM> Update(string id, SupplierVM ViewModel);
        Task<WrapperSupplierListVM> GetListPaged(GetDataListVM dataListVM, bool withHistory);
        Task<WrapperSupplierListVM> Delete(SupplierVM customerTemp);
        Task<WrapperPaymentListVM> GetSupplierPaymentList(GetPaymentDataListVM vm);
        Task<WrapperPaymentListVM> DeleteSupplierPayment(PaymentVM vm);
        Task<WrapperPaymentListVM> PayToSupplier(PaymentVM paymentVM);
        Task<WrapperSupplierHistory> GetSupplierHistory(GetDataListHistory supplierVM);
        SupplierHistory GetSupplierHistoryOverview(WrapperSupplierHistory list);
    }
}
