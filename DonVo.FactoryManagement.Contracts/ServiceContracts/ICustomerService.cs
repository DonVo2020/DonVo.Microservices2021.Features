using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.CustomerView;
using DonVo.FactoryManagement.Models.ViewModels.Payment;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface ICustomerService
    {
        Task<WrapperListCustomerVM> Add(CustomerVM addCustomerViewModel);
        Task<WrapperListCustomerVM> Update(string id, CustomerVM updateCustomerViewModel);
        //Task<List<CustomerVM>> GetList(string FactoryId);
        //Task<CustomerVM> GetSingle(string cusId, string FactoryId);
        Task<WrapperListCustomerVM> GetListPaged(GetDataListVM dataListVM, bool withHistory);
        Task<WrapperListCustomerVM> Delete(CustomerVM customerTemp);
        Task<WrapperCustomerHistory> GetCustomerHistory(GetDataListHistory customerVM);
        Task<WrapperPaymentListVM> GetCustomerPaymentList(DonVo.FactoryManagement.Models.ViewModels.Payment.GetPaymentDataListVM vm);
        Task<WrapperPaymentListVM> RecieveFromCustomer(DonVo.FactoryManagement.Models.ViewModels.Payment.PaymentVM paymentVM);
        Task<WrapperPaymentListVM> DeleteCustomerPayment(DonVo.FactoryManagement.Models.ViewModels.Payment.PaymentVM vm);
        CustomerHistory GetCustomerHistoryOverview(WrapperCustomerHistory list);
    }
}
