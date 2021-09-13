using DonVo.FactoryManagement.Contracts.ServiceContracts;

namespace DonVo.FactoryManagement.Contracts
{
    public interface IServiceWrapper
    {
        IAddressService AddressService { get; }
        ICustomerService CustomerService { get; }
        IDepartmentService DepartmentService { get; }
        IEquipmentService EquipmentService { get; }
        IEquipmentCategoryService EquipmentCategoryService { get; }
        IExpenseService ExpenseService { get; }
        IExpenseTypeService ExpenseTypeService { get; }
        IFactoryService FactoryService { get; }
        IIncomeService IncomeService { get; }
        IIncomeTypeService IncomeTypeService { get; }
        IInvoiceService InvoiceService { get; }
        IInvoiceTypeService InvoiceTypeService { get; }
        IItemService ItemService { get; }
        IItemCategoryService ItemCategoryService { get; }
        IItemStatusService ItemStatusService { get; }
        IPayableService PayableService { get; }
        IPaymentStatusService PaymentStatusService { get; }
        IPhoneService PhoneService { get; }
        IProductionService ProductionService { get; }
        IPurchaseService PurchaseService { get; }
        IPurchaseTypeService PurchaseTypeService { get; }
        IReceivableService ReceivableService { get; }
        IRoleService RoleService { get; }
        ISalesService SalesService { get; }
        IStaffService StaffService { get; }
        IStockService StockService { get; }
        IStockInService StockInService { get; }
        IStockOutService StockOutService { get; }
        ISupplierService SupplierService { get; }
        ITransactionService TransactionService { get; }
        ITransactionTypeService TransactionTypeService { get; }
        IUserAuthInfoService UserAuthInfoService { get; }
        IUserRoleService UserRoleService { get; }
        IApiResourceMappingService ApiResourceMappingService { get; }
    }
}
