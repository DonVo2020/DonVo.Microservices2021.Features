using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Payment;
using DonVo.FactoryManagement.Models.ViewModels.Staff;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IStaffService
    {
        Task<WrapperStaffListVM> Add(StaffVM addCustomerViewModel);
        Task<WrapperStaffListVM> Update(string id, StaffVM updateCustomerViewModel);
        Task<WrapperStaffListVM> GetListPaged(GetDataListVM dataListVM, bool withHistory);
        Task<WrapperStaffListVM> Delete(StaffVM customerTemp);
        Task<WrapperStaffHistory> GetStaffHistory(GetDataListHistory staffVM);
        Task<WrapperPaymentListVM> GetStaffPaymentList(GetPaymentDataListVM vm);
        Task<WrapperPaymentListVM> PayToStaff(PaymentVM paymentVM);
        Task<WrapperPaymentListVM> DeleteStaffPayment(PaymentVM vm);
        StaffHistory GetStaffHistoryOverview(WrapperStaffHistory list);
        Task<WrapperStaffListVM> AddToIT_Admin(StaffVM ViewModel);
    }
}
