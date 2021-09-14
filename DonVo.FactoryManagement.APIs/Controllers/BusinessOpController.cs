using DonVo.FactoryManagement.APIs.Utilities.Auth;
using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.CustomerView;
using DonVo.FactoryManagement.Models.ViewModels.Expense;
using DonVo.FactoryManagement.Models.ViewModels.Income;
using DonVo.FactoryManagement.Models.ViewModels.Payable;
using DonVo.FactoryManagement.Models.ViewModels.Payment;
using DonVo.FactoryManagement.Models.ViewModels.Production;
using DonVo.FactoryManagement.Models.ViewModels.Purchase;
using DonVo.FactoryManagement.Models.ViewModels.Receivable;
using DonVo.FactoryManagement.Models.ViewModels.Sales;
using DonVo.FactoryManagement.Models.ViewModels.Staff;
using DonVo.FactoryManagement.Models.ViewModels.Stock;
using DonVo.FactoryManagement.Models.ViewModels.Supplier;
using DonVo.FactoryManagement.Models.ViewModels.Transaction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.APIs.Controllers
{
    [FactoryAuthorize]
    [Route("api/business")]
    [ApiController]
    public class BusinessOpController : ControllerBase
    {
        //private readonly FactoryManagementContext _context;
        //private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IServiceWrapper _serviceWrapper;
        //private readonly ILoggerManager _logger;
        //private readonly IUtilService _utilService;
        private readonly IBusinessService _businessService;

        public BusinessOpController(
            //FactoryManagementContext context,
            //IRepositoryWrapper repositoryWrapper,
            IServiceWrapper serviceWrapper,
            //ILoggerManager logger,
            //IUtilService utilService,
            IBusinessService businessService)
        {
            //_context = context;
            //_repositoryWrapper = repositoryWrapper;
            _serviceWrapper = serviceWrapper;
            //_logger = logger;
            //_utilService = utilService;
            _businessService = businessService;
        }

        #region Purchase
        [HttpPost]
        [Route("purchase/getInitData")]
        public async Task<ActionResult<InitialLoadDataVM>> GetPurchaseInitData([FromBody]GetDataListVM getDataVM)
        {
            return await _businessService.PurchaseServiceWrapper.GetPurchaseInitialData(getDataVM);
        }

        [HttpPost]
        [Route("purchase/add")]
        public async Task<WrapperPurchaseListVM> AddPurchase([FromBody]PurchaseVM purchase)
        {
            return await _serviceWrapper.PurchaseService.AddPurchaseAsync(purchase);       
        }

        [HttpPost]
        [Route("purchase/getAll")]
        public async Task<WrapperPurchaseListVM> GetAllPurchase([FromBody]GetDataListVM purchase)
        {
           return await _serviceWrapper.PurchaseService.GetAllPurchaseAsync(purchase);
        }

        [HttpPost]
        [Route("purchase/delete")]
        public async Task<WrapperPurchaseListVM> DeletePurchase([FromBody]PurchaseVM purchase)
        {
            return await _serviceWrapper.PurchaseService.DeletePurchaseAsync(purchase);
        }
        #endregion

        #region Sales
        [HttpPost]
        [Route("sales/getInitData")]
        public async Task<ActionResult<InitialLoadDataVM>> GetSalesInitData([FromBody]GetDataListVM getDataVM)
        {
            return await _businessService.PurchaseServiceWrapper.GetSalesInitialData(getDataVM);
        }

        [HttpPost]
        [Route("sales/add")]
        public async Task<WrapperSalesListVM> AddSales([FromBody]SalesVM sales)
        {
           return  await _serviceWrapper.SalesService.AddSalesAsync(sales);
        }

        [HttpPost]
        [Route("sales/getAll")]
        public async Task<WrapperSalesListVM> GetAllSales([FromBody]GetDataListVM purchase)
        {
            return await _serviceWrapper.SalesService.GetAllSalesAsync(purchase);
        }

        [HttpPost]
        [Route("sales/delete")]
        public async Task<WrapperSalesListVM> DeleteSales([FromBody]SalesVM sales)
        {
            return await _serviceWrapper.SalesService.DeleteSalesAsync(sales);
        }
        #endregion

        #region Payment

        #region Supplier
        [HttpPost]
        [Route("supplier/payment")]
        public async Task<WrapperPaymentListVM> SupplierPay([FromBody]PaymentVM vm)
        {
            return await _serviceWrapper.SupplierService.PayToSupplier(vm);
        }

        [HttpPost]
        [Route("supplier/payment/list")]
        public async Task<WrapperPaymentListVM> SupplierPaymentList([FromBody]GetPaymentDataListVM vm)
        {
            return await _serviceWrapper.SupplierService.GetSupplierPaymentList(vm);
        }

        [HttpPost]
        [Route("supplier/payment/delete")]
        public async Task<WrapperPaymentListVM> SupplierPaymentDelete([FromBody]PaymentVM vm)
        {
            return await _serviceWrapper.SupplierService.DeleteSupplierPayment(vm);
        }
        #endregion

        #region Staff
        [HttpPost]
        [Route("staff/payment")]
        public async Task<WrapperPaymentListVM> StaffPay([FromBody]PaymentVM vm)
        {
            return await _serviceWrapper.StaffService.PayToStaff(vm);
        }

        [HttpPost]
        [Route("pay/init_data")]
        public async Task<InitialLoadDataVM> GetPaymentInitData([FromBody]GetDataListVM getDataVM)
        {
            return await _businessService.PurchaseServiceWrapper.GetPaymentInitialData(getDataVM);
        }

        [HttpPost]
        [Route("staff/payment/list")]
        public async Task<WrapperPaymentListVM> StaffPaymentList([FromBody]GetPaymentDataListVM vm)
        {
            return await _serviceWrapper.StaffService.GetStaffPaymentList(vm);
        }

        [HttpPost]
        [Route("staff/payment/delete")]
        public async Task<WrapperPaymentListVM> StaffPaymentDelete([FromBody]PaymentVM vm)
        {
            return await _serviceWrapper.StaffService.DeleteStaffPayment(vm);
        }
        #endregion

        #region Customer
        [HttpPost]
        [Route("customer/payment")]
        public async Task<WrapperPaymentListVM> CustomerCash([FromBody]PaymentVM vm)
        {
            return await _serviceWrapper.CustomerService.RecieveFromCustomer(vm);
        }

        [HttpPost]
        [Route("customer/payment/list")]
        public async Task<WrapperPaymentListVM> CustomerPaymentList([FromBody]GetPaymentDataListVM vm)
        {
            return await _serviceWrapper.CustomerService.GetCustomerPaymentList(vm);
        }

        [HttpPost]
        [Route("customer/payment/delete")]
        public async Task<WrapperPaymentListVM> CustomerPaymentDelete([FromBody]PaymentVM vm)
        {
            return await _serviceWrapper.CustomerService.DeleteCustomerPayment(vm);
        }
        #endregion

        #endregion

        #region History

        #region Customer
        [HttpPost]
        [Route("supplier/history")]
        public async Task<WrapperSupplierHistory> GetSupplierHistory([FromBody]GetDataListHistory vm)
        {
            return await _serviceWrapper.SupplierService.GetSupplierHistory(vm);
        }
        #endregion

        #region Staff
        [HttpPost]
        [Route("staff/history")]
        public async Task<WrapperStaffHistory> GetStaffHistory([FromBody]GetDataListHistory vm)
        {
            return await _serviceWrapper.StaffService.GetStaffHistory(vm);
        }
        #endregion

        #region Supplier
        [HttpPost]
        [Route("customer/history")]
        public async Task<WrapperCustomerHistory> GetCustomerHistory([FromBody]GetDataListHistory vm)
        {
            return await _serviceWrapper.CustomerService.GetCustomerHistory(vm);
        }
        #endregion

        #endregion

        #region Production
        [HttpPost]
        [Route("production/getInitData")]
        public async Task<ActionResult<InitialLoadDataVM>> GetProductionInitData([FromBody]GetDataListVM getDataVM)
        {
            return await _businessService.PurchaseServiceWrapper.GetProductionInitialData(getDataVM);
        }
        #endregion

        #region SalesReturn
        [HttpPost]
        [Route("sales/return/add")]
        public async Task<WrapperSalesReturnVM> AddSalesReturn([FromBody]SalesReturnVM sales)
        {
            return await _serviceWrapper.SalesService.AddSalesReturn(sales);
        }

        [HttpPost]
        [Route("sales/return/getAll")]
        public async Task<WrapperSalesReturnVM> GetAllSalesReturn([FromBody]GetDataListVM sales)
        {
            return await _serviceWrapper.SalesService.GetAllSalesReturn(sales);
        }

        [HttpPost]
        [Route("sales/return/delete")]
        public async Task<WrapperSalesReturnVM> DeleteSalesReturn([FromBody]SalesReturnVM sales)
        {
            return await _serviceWrapper.SalesService.DeleteSalesReturn(sales);
        }
        #endregion

        #region PurchaseReturn
        [HttpPost]
        [Route("purchase/return/add")]
        public async Task<WrapperPurchaseReturnVM> AddPurchaseReturn([FromBody]PurchaseReturnVM sales)
        {
            return await _serviceWrapper.PurchaseService.AddPurchaseReturnAsync(sales);
        }

        [HttpPost]
        [Route("purchase/return/getAll")]
        public async Task<WrapperPurchaseReturnVM> GetAllPurchaseReturn([FromBody]GetDataListVM sales)
        {
            return await _serviceWrapper.PurchaseService.GetAllPurchaseReturnAsync(sales);
        }

        [HttpPost]
        [Route("purchase/return/delete")]
        public async Task<WrapperPurchaseReturnVM> DeletePurchaseReturn([FromBody]PurchaseReturnVM sales)
        {
            return await _serviceWrapper.PurchaseService.DeletePurchaseReturnAsync(sales);
        }
        #endregion

        #region Monthly Report
        [HttpPost]
        [Route("report/monthly/production")]
        public async Task<WrapperMonthProductionListVM> MonthlyProduction([FromBody]MonthlyReport mp)
        {
            return await _businessService.BusinessWrapperService.MonthlyProduction(mp);
        }

        [HttpPost]
        [Route("report/monthly/payable")]
        public async Task<WrapperMonthPayableListVM> MonthlyPayable([FromBody]MonthlyReport mp)
        {
            return await _businessService.BusinessWrapperService.MonthlyPayable(mp);
        }

        [HttpPost]
        [Route("report/monthly/Receivable")]
        public async Task<WrapperMonthReceivableVM> MonthlyReceivable([FromBody]MonthlyReport mp)
        {
            return await _businessService.BusinessWrapperService.MonthlyReceivable(mp);
        }

        [HttpPost]
        [Route("report/monthly/income")]
        public async Task<WrapperMonthIncomeVM> MonthlyIncome([FromBody]MonthlyReport mp)
        {
            return await _businessService.BusinessWrapperService.MonthlyIncome(mp);
        }

        [HttpPost]
        [Route("report/monthly/expense")]
        public async Task<WrapperMonthExpenseVM> MonthlyExpense([FromBody]MonthlyReport mp)
        {
            return await _businessService.BusinessWrapperService.MonthlyExpense(mp);
        }

        [HttpPost]
        [Route("report/monthly/transaction")]
        public async Task<WrapperMonthTransactionVM> MonthlyTransaction([FromBody]MonthlyReport mp)
        {
            return await _businessService.BusinessWrapperService.MonthlyTransaction(mp);
        }
        #endregion

        #region Change Item Status
        [HttpPost]
        [Route("item/status/change")]
        public async Task<WrapperStockListVM> ChangeItemStatusInStock([FromBody]StockVM sales)
        {
            return await _serviceWrapper.StockService.ChangeItemStatus(sales);
        }
        #endregion

        [HttpPost]
        [Route("staff/getInitData")]
        public async Task<ActionResult<InitialLoadDataVM>> GetStaffInitData([FromBody]GetDataListVM getDataVM)
        {
          return   await _businessService.PurchaseServiceWrapper.GetStaffInitialData(getDataVM);
        }
    }
}