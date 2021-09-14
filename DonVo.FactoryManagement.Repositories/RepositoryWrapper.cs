using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Repositories.EntitywiseRepository;
using System;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        #region Var Declaration
        readonly FactoryManagementContext dbContext;
        IAddressRepository _Address;
        ICustomerRepository _Customer;
        IDepartmentRepository _Department;
        IEquipmentRepository _Equipment;
        IEquipmentCategoryRepository _EquipmentCategory;
        IExpenseRepository _Expense;
        IExpenseTypeRepository _ExpenseType;
        IFactoryRepository _Factory;
        IIncomeRepository _Income;
        IIncomeTypeRepository _IncomeType;
        IInvoiceRepository _Invoice;
        IInvoiceTypeRepository _InvoiceType;
        IItemRepository _Item;
        IItemCategoryRepository _ItemCategory;
        IItemStatusRepository _ItemStatus;
        IPayableRepository _Payable;
        IPaymentStatusRepository _PaymentStatus;
        IPhoneRepository _Phone;
        IProductionRepository _Production;
        IPurchaseRepository _Purchase;
        IPurchaseTypeRepository _PurchaseType;
        IReceivableRepository _Receivable;
        IRoleRepository _Role;
        ISalesRepository _Sales;
        IStaffRepository _Staff;
        IStockRepository _Stock;
        IStockInRepository _StockIn;
        IStockOutRepository _StockOut;
        ISupplierRepository _Supplier;
        ITransactionRepository _Transaction;
        ITransactionTypeRepository _TransactionType;
        IUserAuthInfoRepository _UserAuthInfo;
        IUserRoleRepository _UserRole;
        IApiResourceMappingRepository _ApiResourceMappingRepository;
        readonly IUtilService _util;
        #endregion
        public RepositoryWrapper(
         FactoryManagementContext dbContext,
         IAddressRepository Address,
         ICustomerRepository Customer,
         IDepartmentRepository Department,
         IEquipmentRepository Equipment,
         IEquipmentCategoryRepository EquipmentCategory,
         IExpenseRepository Expense,
         IExpenseTypeRepository ExpenseType,
         IFactoryRepository Factory,
         IIncomeRepository Income,
         IIncomeTypeRepository IncomeType,
         IInvoiceRepository Invoice,
         IInvoiceTypeRepository InvoiceType,
         IItemRepository Item,
         IItemCategoryRepository ItemCategory,
         IItemStatusRepository ItemStatus,
         IPayableRepository Payable,
         IPaymentStatusRepository PaymentStatus,
         IPhoneRepository Phone,
         IProductionRepository Production,
         IPurchaseRepository Purchase,
         IPurchaseTypeRepository PurchaseType,
         IReceivableRepository Receivable,
         IRoleRepository Role,
         ISalesRepository Sales,
         IStaffRepository Staff,
         IStockRepository Stock,
         IStockInRepository StockIn,
         IStockOutRepository StockOut,
         ISupplierRepository Supplier,
         ITransactionRepository Transaction,
         ITransactionTypeRepository TransactionType,
         IUserAuthInfoRepository UserAuthInfo,
         IUserRoleRepository UserRole,
         IUtilService util,
         IApiResourceMappingRepository ApiResourceMappingRepository)
        {
            if (Factory is null)
            {
                throw new ArgumentNullException(nameof(Factory));
            }

            this.dbContext = dbContext;
            this._Address = Address;
            this._Customer = Customer;
            this._Department = Department;
            this._Equipment = Equipment;
            this._EquipmentCategory = EquipmentCategory;
            this._Expense = Expense;
            this._ExpenseType = ExpenseType;
            this._Income = Income;
            this._IncomeType = IncomeType;
            this._Invoice = Invoice;
            this._InvoiceType = InvoiceType;
            this._Item = Item;
            this._ItemCategory = ItemCategory;
            this._ItemStatus = ItemStatus;
            this._Payable = Payable;
            this._PaymentStatus = PaymentStatus;
            this._Phone = Phone;
            this._Production = Production;
            this._Purchase = Purchase;
            this._PurchaseType = PurchaseType;
            this._Receivable = Receivable;
            this._Role = Role;
            this._Sales = Sales;
            this._Staff = Staff;
            this._Stock = Stock;
            this._StockIn = StockIn;
            this._StockOut = StockOut;
            this._Supplier = Supplier;
            this._Transaction = Transaction;
            this._TransactionType = TransactionType;
            this._UserAuthInfo = UserAuthInfo;
            this._UserRole = UserRole;
            this._util = util;
            this._ApiResourceMappingRepository = ApiResourceMappingRepository;
        }

        public IApiResourceMappingRepository ApiResourceMapping
        {
            get
            {
                if (_ApiResourceMappingRepository == null)
                {
                    //return new AddressRepository(dbContext,_util);
                    _ApiResourceMappingRepository = new ApiResourceMappingRepository(dbContext, _util);
                }
                return _ApiResourceMappingRepository;
            }
        }

        public IAddressRepository Address
        {
            get
            {
                if (_Address == null)
                {
                    //return new AddressRepository(dbContext,_util);
                    _Address = new AddressRepository(dbContext, _util);
                }
                return _Address;
            }
        }

        public ICustomerRepository Customer
        {
            get
            {
                if (_Customer == null)
                {
                    //return new CustomerRepository(dbContext,_util);
                    _Customer = new CustomerRepository(dbContext, _util);
                }

                return _Customer;
            }
        }

        public IDepartmentRepository Department
        {
            get
            {

                if (_Department == null)
                {
                    //return new DepartmentRepository(dbContext,_util);
                    _Department = new DepartmentRepository(dbContext, _util);
                }

                return _Department;
            }
        }

        public IEquipmentRepository Equipment
        {
            get
            {
                if (_Equipment == null)
                {
                    //return new EquipmentRepository(dbContext,_util);
                    _Equipment = new EquipmentRepository(dbContext, _util);
                }

                return _Equipment;
            }
        }

        public IEquipmentCategoryRepository EquipmentCategory
        {
            get
            {

                if (_EquipmentCategory == null)
                {
                    //return new EquipmentCategoryRepository(dbContext,_util);
                    _EquipmentCategory = new EquipmentCategoryRepository(dbContext, _util);
                }

                return _EquipmentCategory;
            }
        }

        public IExpenseRepository Expense
        {
            get
            {
                if (_Expense == null)
                {
                    //return new ExpenseRepository(dbContext,_util);
                    _Expense = new ExpenseRepository(dbContext, _util);
                }

                return _Expense;
            }
        }

        public IExpenseTypeRepository ExpenseType
        {
            get
            {
                if (_ExpenseType == null)
                {
                    //return new ExpenseTypeRepository(dbContext,_util);
                    _ExpenseType = new ExpenseTypeRepository(dbContext, _util);
                }

                return _ExpenseType;
            }
        }

        public IFactoryRepository Factory
        {
            get
            {
                if (_Factory == null)
                {
                    //return new FactoryRepository(dbContext,_util);
                    _Factory = new FactoryRepository(dbContext, _util);
                }

                return _Factory;
            }
        }

        public IIncomeRepository Income
        {
            get
            {
                if (_Income == null)
                {
                    //return new IncomeTypeRepository(dbContext,_util);
                    _Income = new IncomeRepository(dbContext, _util);
                }

                return _Income;
            }
        }

        public IIncomeTypeRepository IncomeType
        {
            get
            {
                if (_IncomeType == null)
                {
                    //return new IncomeTypeRepository(dbContext,_util);
                    _IncomeType = new IncomeTypeRepository(dbContext, _util);
                }

                return _IncomeType;
            }
        }

        public IInvoiceRepository Invoice
        {
            get
            {
                if (_Invoice == null)
                {
                    //return new InvoiceRepository(dbContext,_util);
                    _Invoice = new InvoiceRepository(dbContext, _util);
                }

                return _Invoice;
            }
        }

        public IInvoiceTypeRepository InvoiceType
        {
            get
            {
                if (_InvoiceType == null)
                {
                    //return new InvoiceTypeRepository(dbContext,_util);
                    _InvoiceType = new InvoiceTypeRepository(dbContext, _util);
                }

                return _InvoiceType;
            }
        }

        public IItemRepository Item
        {
            get
            {
                if (_Item == null)
                {
                    //return new ItemRepository(dbContext,_util);
                    _Item = new ItemRepository(dbContext, _util);
                }

                return _Item;
            }
        }

        public IItemCategoryRepository ItemCategory
        {
            get
            {
                if (_ItemCategory == null)
                {
                    //return new ItemCategoryRepository(dbContext,_util);
                    _ItemCategory = new ItemCategoryRepository(dbContext, _util);
                }

                return _ItemCategory;
            }
        }

        public IItemStatusRepository ItemStatus
        {
            get
            {
                if (_ItemStatus == null)
                {
                    //return new ItemStatusRepository(dbContext,_util);
                    _ItemStatus = new ItemStatusRepository(dbContext, _util);
                }

                return _ItemStatus;
            }
        }

        public IPayableRepository Payable
        {
            get
            {
                if (_Payable == null)
                {
                    //return new PayableRepository(dbContext,_util);
                    _Payable = new PayableRepository(dbContext, _util);
                }

                return _Payable;
            }
        }

        public IPaymentStatusRepository PaymentStatus
        {
            get
            {
                if (_PaymentStatus == null)
                {
                    //return new PaymentStatusRepository(dbContext,_util);
                    _PaymentStatus = new PaymentStatusRepository(dbContext, _util);
                }

                return _PaymentStatus;
            }
        }

        public IPhoneRepository Phone
        {
            get
            {
                if (_Phone == null)
                {
                    //return new PhoneRepository(dbContext,_util);
                    _Phone = new PhoneRepository(dbContext, _util);
                }

                return _Phone;
            }
        }

        public IProductionRepository Production
        {
            get
            {
                if (_Production == null)
                {
                    //return new ProductionRepository(dbContext,_util);
                    _Production = new ProductionRepository(dbContext, _util);
                }

                return _Production;
            }
        }

        public IPurchaseRepository Purchase
        {
            get
            {
                if (_Purchase == null)
                {
                    //return new PurchaseRepository(dbContext,_util);
                    _Purchase = new PurchaseRepository(dbContext, _util);
                }

                return _Purchase;
            }
        }

        public IPurchaseTypeRepository PurchaseType
        {
            get
            {
                if (_PurchaseType == null)
                {
                    //return new PurchaseTypeRepository(dbContext,_util);
                    _PurchaseType = new PurchaseTypeRepository(dbContext, _util);
                }

                return _PurchaseType;
            }
        }

        public IReceivableRepository Receivable
        {
            get
            {
                if (_Receivable == null)
                {
                    //return new ReceivableRepository(dbContext,_util);
                    _Receivable = new ReceivableRepository(dbContext, _util);
                }

                return _Receivable;
            }
        }

        public IRoleRepository Role
        {
            get
            {
                if (_Role == null)
                {
                    //return new RoleRepository(dbContext,_util);
                    _Role = new RoleRepository(dbContext, _util);
                }

                return _Role;
            }
        }

        public ISalesRepository Sales
        {
            get
            {
                if (_Sales == null)
                {
                    //return new SalesRepository(dbContext,_util);
                    _Sales = new SalesRepository(dbContext, _util);
                }

                return _Sales;
            }
        }

        public IStaffRepository Staff
        {
            get
            {
                if (_Staff == null)
                {
                    //return new StaffRepository(dbContext,_util);
                    _Staff = new StaffRepository(dbContext, _util);
                }

                return _Staff;
            }
        }

        public IStockRepository Stock
        {
            get
            {
                if (_Stock == null)
                {
                    //return new StockRepository(dbContext,_util);
                    _Stock = new StockRepository(dbContext, _util);
                }

                return _Stock;
            }
        }

        public IStockInRepository StockIn
        {
            get
            {
                if (_StockIn == null)
                {
                    //return new StockInRepository(dbContext,_util);
                    _StockIn = new StockInRepository(dbContext, _util);
                }

                return _StockIn;
            }
        }

        public IStockOutRepository StockOut
        {
            get
            {
                if (_StockOut == null)
                {
                    //return new StockOutRepository(dbContext,_util);
                    _StockOut = new StockOutRepository(dbContext, _util);
                }

                return _StockOut;
            }
        }

        public ISupplierRepository Supplier
        {
            get
            {
                if (_Supplier == null)
                {
                    //return new SupplierRepository(dbContext,_util);
                    _Supplier = new SupplierRepository(dbContext, _util);
                }

                return _Supplier;
            }
        }

        public ITransactionRepository Transaction
        {
            get
            {
                if (_Transaction == null)
                {
                    //return new TransactionRepository(dbContext,_util);
                    _Transaction = new TransactionRepository(dbContext, _util);
                }

                return _Transaction;
            }
        }

        public ITransactionTypeRepository TransactionType
        {
            get
            {
                if (_TransactionType == null)
                {
                    //return new TransactionTypeRepository(dbContext,_util);
                    _TransactionType = new TransactionTypeRepository(dbContext, _util);
                }

                return _TransactionType;
            }
        }

        public IUserAuthInfoRepository UserAuthInfo
        {
            get
            {
                if (_UserAuthInfo == null)
                {
                    //return new UserAuthInfoRepository(dbContext,_util);
                    _UserAuthInfo = new UserAuthInfoRepository(dbContext, _util);
                }

                return _UserAuthInfo;
            }
        }

        public IUserRoleRepository UserRole
        {
            get
            {
                if (_UserRole == null)
                {
                    //return new UserRoleRepository(dbContext,_util);
                    _UserRole = new UserRoleRepository(dbContext, _util);
                }

                return _UserRole;
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
