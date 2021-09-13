using DonVo.FactoryManagement.CommonUtils;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.CustomerView;
using DonVo.FactoryManagement.Models.ViewModels.Expense;
using DonVo.FactoryManagement.Models.ViewModels.Income;
using DonVo.FactoryManagement.Models.ViewModels.Payable;
using DonVo.FactoryManagement.Models.ViewModels.Payment;
using DonVo.FactoryManagement.Models.ViewModels.Production;
using DonVo.FactoryManagement.Models.ViewModels.Receivable;
using DonVo.FactoryManagement.Models.ViewModels.Staff;
using DonVo.FactoryManagement.Models.ViewModels.Supplier;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.IBusinessServiceWrapper
{
    public interface IBusinessWrapperService
    {
        //Task<WrapperPaymentListVM> PayToSupplier(PaymentVM paymentVM);
        //Task<WrapperPaymentListVM> PayToStaff(PaymentVM paymentVM);
        //Task<WrapperPaymentListVM> RecieveFromCustomer(PaymentVM paymentVM);
        //Task<WrapperCustomerHistory> GetCustomerHistory(GetDataListHistory customerVM);
        //Task<WrapperSupplierHistory> GetSupplierHistory(GetDataListHistory supplierVM);
        //Task<WrapperStaffHistory> GetStaffHistory(GetDataListHistory staffVM); 
        //Task<WrapperPaymentListVM> GetSupplierPaymentList(GetPaymentDataListVM vm);
        //Task<WrapperPaymentListVM> DeleteSupplierPayment(PaymentVM vm);
        //Task<WrapperPaymentListVM> GetStaffPaymentList(GetPaymentDataListVM vm);
        //Task<WrapperPaymentListVM> DeleteStaffPayment(PaymentVM vm);
        //Task<WrapperPaymentListVM> GetCustomerPaymentList(GetPaymentDataListVM vm);
        //Task<WrapperPaymentListVM> DeleteCustomerPayment(PaymentVM vm);
        Task<WrapperMonthExpenseVM> MonthlyExpense(MonthlyReport vm);
        Task<WrapperMonthIncomeVM> MonthlyIncome(MonthlyReport vm);
        Task<WrapperMonthPayableListVM> MonthlyPayable(MonthlyReport vm);
        Task<WrapperMonthProductionListVM> MonthlyProduction(MonthlyReport vm);
        Task<WrapperMonthReceivableVM> MonthlyReceivable(MonthlyReport vm);
        Task<DonVo.FactoryManagement.Models.ViewModels.Transaction.WrapperMonthTransactionVM> MonthlyTransaction(MonthlyReport vm);
    }
}
