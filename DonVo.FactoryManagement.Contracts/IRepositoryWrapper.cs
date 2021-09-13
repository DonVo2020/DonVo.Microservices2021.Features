using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts
{
    public interface IRepositoryWrapper
    {
        #region Variable Declaration

        IAddressRepository Address { get; }
        ICustomerRepository Customer { get; }
        IDepartmentRepository Department { get; }
        IEquipmentRepository Equipment { get; }
        IEquipmentCategoryRepository EquipmentCategory { get; }
        IExpenseRepository Expense { get; }
        IExpenseTypeRepository ExpenseType { get; }
        IFactoryRepository Factory { get; }
        IIncomeRepository Income { get; }
        IIncomeTypeRepository IncomeType { get; }
        IInvoiceRepository Invoice { get; }
        IInvoiceTypeRepository InvoiceType { get; }
        IItemRepository Item { get; }
        IItemCategoryRepository ItemCategory { get; }
        IItemStatusRepository ItemStatus { get; }
        IPayableRepository Payable { get; }
        IPaymentStatusRepository PaymentStatus { get; }
        IPhoneRepository Phone { get; }
        IProductionRepository Production { get; }
        IPurchaseRepository Purchase { get; }
        IPurchaseTypeRepository PurchaseType { get; }
        IReceivableRepository Receivable { get; }
        IRoleRepository Role { get; }
        ISalesRepository Sales { get; }
        IStaffRepository Staff { get; }
        IStockRepository Stock { get; }
        IStockInRepository StockIn { get; }
        IStockOutRepository StockOut { get; }
        ISupplierRepository Supplier { get; }
        ITransactionRepository Transaction { get; }
        ITransactionTypeRepository TransactionType { get; }
        IUserAuthInfoRepository UserAuthInfo { get; }
        IUserRoleRepository UserRole { get; }
        IApiResourceMappingRepository ApiResourceMapping { get; }
        #endregion

        void Save();
        Task SaveAsync();

    }
}