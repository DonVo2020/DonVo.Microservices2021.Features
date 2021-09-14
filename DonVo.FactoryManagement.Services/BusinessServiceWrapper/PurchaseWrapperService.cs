using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.IBusinessServiceWrapper;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.CustomerView;
using DonVo.FactoryManagement.Models.ViewModels.Equipment;
using DonVo.FactoryManagement.Models.ViewModels.ExpenseType;
using DonVo.FactoryManagement.Models.ViewModels.IncomeType;
using DonVo.FactoryManagement.Models.ViewModels.InvoiceType;
using DonVo.FactoryManagement.Models.ViewModels.Item;
using DonVo.FactoryManagement.Models.ViewModels.ItemCategoryView;
using DonVo.FactoryManagement.Models.ViewModels.ItemStatus;
using DonVo.FactoryManagement.Models.ViewModels.Role;
using DonVo.FactoryManagement.Models.ViewModels.Staff;
using DonVo.FactoryManagement.Models.ViewModels.Supplier;
using System.Threading.Tasks;

namespace Service.BusinessServiceWrapper
{
    public class PurchaseWrapperService : IPurchaseWrapperService
    {
        private readonly IServiceWrapper _serviceWrapper;
        //private readonly IRepositoryWrapper _repositoryWrapper;
        //private readonly IUtilService _utilService;

        public PurchaseWrapperService(IServiceWrapper serviceWrapper
            //IRepositoryWrapper repositoryWrapper, 
            //IUtilService utilService
            )
        {
            //this._repositoryWrapper = repositoryWrapper;
            this._serviceWrapper = serviceWrapper;
            //this._utilService = utilService;
        }

        public async Task<InitialLoadDataVM> GetPurchaseInitialData(GetDataListVM getDataListVM)
        {
            InitialLoadDataVM vm = new();

            Task<WrapperItemListVM> itemListTask = _serviceWrapper.ItemService.GetListPaged(getDataListVM);
            Task<WrapperItemCategoryListVM> itemCategoryListTask = _serviceWrapper.ItemCategoryService.GetListPaged(getDataListVM);
            Task<WrapperSupplierListVM> supplierListTask = _serviceWrapper.SupplierService.GetListPaged(getDataListVM, false);
            Task<WrapperExpenseTypeListVM> expenseTypeTask = _serviceWrapper.ExpenseTypeService.GetListPaged(getDataListVM);
            Task<WrapperItemStatusListVM> itemStatusListTask = _serviceWrapper.ItemStatusService.GetListPaged(getDataListVM);
            Task<WrapperInvoiceTypeListVM> invoiceTypeListTask = _serviceWrapper.InvoiceTypeService.GetListPaged(getDataListVM);

            await Task.WhenAll(itemListTask, itemCategoryListTask, supplierListTask, expenseTypeTask, itemStatusListTask, invoiceTypeListTask);

            vm.ItemCategoryVMs = itemCategoryListTask.Result.ListOfData;
            vm.ItemVMs = itemListTask.Result.ListOfData;
            vm.SupplierVMs = supplierListTask.Result.ListOfData;
            vm.ExpenseTypeVMs = expenseTypeTask.Result.ListOfData;
            vm.ItemStatusVMs = itemStatusListTask.Result.ListOfData;
            vm.InvoiceTypeVMs = invoiceTypeListTask.Result.ListOfData;
            return vm;
        }

        public async Task<InitialLoadDataVM> GetSalesInitialData(GetDataListVM getDataListVM)
        {
            InitialLoadDataVM vm = new();

            Task<WrapperItemListVM> itemListTask = _serviceWrapper.ItemService.GetListPaged(getDataListVM);
            Task<WrapperItemCategoryListVM> itemCategoryListTask = _serviceWrapper.ItemCategoryService.GetListPaged(getDataListVM);
            Task<WrapperListCustomerVM> customerListTask = _serviceWrapper.CustomerService.GetListPaged(getDataListVM, false);
            Task<WrapperIncomeTypeListVM> incomeTypeListTask = _serviceWrapper.IncomeTypeService.GetListPaged(getDataListVM);
            Task<WrapperItemStatusListVM> itemStatusListTask = _serviceWrapper.ItemStatusService.GetListPaged(getDataListVM);
            Task<WrapperInvoiceTypeListVM> invoiceTypeListTask = _serviceWrapper.InvoiceTypeService.GetListPaged(getDataListVM);

            await Task.WhenAll(itemListTask, itemCategoryListTask, customerListTask, incomeTypeListTask, itemStatusListTask, invoiceTypeListTask);

            vm.ItemCategoryVMs = itemCategoryListTask.Result.ListOfData;
            vm.ItemVMs = itemListTask.Result.ListOfData;
            vm.CustomerVMs = customerListTask.Result.ListOfData;
            vm.IncomeTypeVMs = incomeTypeListTask.Result.ListOfData;
            vm.ItemStatusVMs = itemStatusListTask.Result.ListOfData;
            vm.InvoiceTypeVMs = invoiceTypeListTask.Result.ListOfData;
            return vm;
        }

        public async Task<InitialLoadDataVM> GetPaymentInitialData(GetDataListVM getDataListVM)
        {
            InitialLoadDataVM vm = new();
            Task<WrapperListCustomerVM> customerListTask = _serviceWrapper.CustomerService.GetListPaged(getDataListVM, false);
            Task<WrapperIncomeTypeListVM> incomeTypeListTask = _serviceWrapper.IncomeTypeService.GetListPaged(getDataListVM);
            Task<WrapperInvoiceTypeListVM> invoiceTypeListTask = _serviceWrapper.InvoiceTypeService.GetListPaged(getDataListVM);
            Task<WrapperExpenseTypeListVM> expenseTypeTask = _serviceWrapper.ExpenseTypeService.GetListPaged(getDataListVM);
            Task<WrapperSupplierListVM> supplierListTask = _serviceWrapper.SupplierService.GetListPaged(getDataListVM, false);
            Task<WrapperStaffListVM> staffListTask = _serviceWrapper.StaffService.GetListPaged(getDataListVM, false);

            Task<WrapperItemListVM> itemTask = _serviceWrapper.ItemService.GetListPaged(getDataListVM);
            Task<WrapperItemCategoryListVM> itemCategoryTask = _serviceWrapper.ItemCategoryService.GetListPaged(getDataListVM);
            Task<WrapperItemStatusListVM> itemStatusTask = _serviceWrapper.ItemStatusService.GetListPaged(getDataListVM);

            await Task.WhenAll(customerListTask, incomeTypeListTask,
                invoiceTypeListTask, expenseTypeTask, supplierListTask, staffListTask
                , itemTask, itemCategoryTask, itemStatusTask);

            vm.CustomerVMs = customerListTask.Result.ListOfData;
            vm.IncomeTypeVMs = incomeTypeListTask.Result.ListOfData;
            vm.InvoiceTypeVMs = invoiceTypeListTask.Result.ListOfData;
            vm.ExpenseTypeVMs = expenseTypeTask.Result.ListOfData;
            vm.SupplierVMs = supplierListTask.Result.ListOfData;
            vm.StaffVMs = staffListTask.Result.ListOfData;

            vm.ItemCategoryVMs = itemCategoryTask.Result.ListOfData;
            vm.ItemVMs = itemTask.Result.ListOfData;
            vm.ItemStatusVMs = itemStatusTask.Result.ListOfData;
            return vm;
        }

        public async Task<InitialLoadDataVM> GetProductionInitialData(GetDataListVM getDataListVM)
        {
            InitialLoadDataVM vm = new();
            Task<WrapperItemListVM> itemListTask = _serviceWrapper.ItemService.GetListPaged(getDataListVM);
            Task<WrapperItemCategoryListVM> itemCategoryListTask = _serviceWrapper.ItemCategoryService.GetListPaged(getDataListVM);
            Task<WrapperListCustomerVM> customerListTask = _serviceWrapper.CustomerService.GetListPaged(getDataListVM, false);
            Task<WrapperIncomeTypeListVM> incomeTypeListTask = _serviceWrapper.IncomeTypeService.GetListPaged(getDataListVM);
            Task<WrapperInvoiceTypeListVM> invoiceTypeListTask = _serviceWrapper.InvoiceTypeService.GetListPaged(getDataListVM);
            Task<WrapperExpenseTypeListVM> expenseTypeTask = _serviceWrapper.ExpenseTypeService.GetListPaged(getDataListVM);
            Task<WrapperSupplierListVM> supplierListTask = _serviceWrapper.SupplierService.GetListPaged(getDataListVM, false);
            Task<WrapperStaffListVM> staffListTask = _serviceWrapper.StaffService.GetListPaged(getDataListVM, false);
            Task<WrapperEquipmentListVM> equipmentListTask = _serviceWrapper.EquipmentService.GetListPaged(getDataListVM);

            Task<WrapperItemStatusListVM> itemStatusListTask = _serviceWrapper.ItemStatusService.GetListPaged(getDataListVM);

            await Task.WhenAll(customerListTask, incomeTypeListTask,
                invoiceTypeListTask, expenseTypeTask, supplierListTask, staffListTask,
                itemListTask, itemCategoryListTask, equipmentListTask, itemStatusListTask);

            vm.ItemVMs = itemListTask.Result.ListOfData;
            vm.ItemCategoryVMs = itemCategoryListTask.Result.ListOfData;
            vm.CustomerVMs = customerListTask.Result.ListOfData;
            vm.IncomeTypeVMs = incomeTypeListTask.Result.ListOfData;
            vm.InvoiceTypeVMs = invoiceTypeListTask.Result.ListOfData;
            vm.ExpenseTypeVMs = expenseTypeTask.Result.ListOfData;
            vm.SupplierVMs = supplierListTask.Result.ListOfData;
            vm.StaffVMs = staffListTask.Result.ListOfData;
            vm.EquipmentVMs = equipmentListTask.Result.ListOfData;
            vm.ItemStatusVMs = itemStatusListTask.Result.ListOfData;
            return vm;
        }

        public async Task<InitialLoadDataVM> GetStaffInitialData(GetDataListVM getDataListVM)
        {
            InitialLoadDataVM vm = new();
            Task<WrapperRoleListVM> roleListTask = _serviceWrapper.RoleService.GetListPaged(getDataListVM);
            await Task.WhenAll(roleListTask);
            vm.RoleVMs = roleListTask.Result.ListOfData;
            return vm;
        }
    }
}
