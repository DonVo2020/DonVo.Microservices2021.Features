using AutoMapper;
using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using Service.BusinessServices;

namespace Service
{
    public class ServiceWrapper : IServiceWrapper
    {
        IAddressService _AddressService;
        ICustomerService _CustomerService;
        IDepartmentService _DepartmentService;
        IEquipmentService _EquipmentService;
        IEquipmentCategoryService _EquipmentCategoryService;
        IExpenseService _ExpenseService;
        IExpenseTypeService _ExpenseTypeService;
        IFactoryService _FactoryService;
        IIncomeService _IncomeService;
        IIncomeTypeService _IncomeTypeService;
        IInvoiceService _InvoiceService;
        IInvoiceTypeService _InvoiceTypeService;
        IItemService _ItemService;
        IItemCategoryService _ItemCategoryService;
        IItemStatusService _ItemStatusService;
        IPayableService _PayableService;
        IPaymentStatusService _PaymentStatusService;
        IPhoneService _PhoneService;
        IProductionService _ProductionService;
        IPurchaseService _PurchaseService;
        IPurchaseTypeService _PurchaseTypeService;
        IReceivableService _ReceivableService;
        IRoleService _RoleService;
        ISalesService _SalesService;
        IStaffService _StaffService;
        IStockService _StockService;
        IStockInService _StockInService;
        IStockOutService _StockOutService;
        ISupplierService _SupplierService;
        ITransactionService _TransactionService;
        ITransactionTypeService _TransactionTypeService;
        IUserAuthInfoService _UserAuthInfoService;
        IUserRoleService _UserRoleService;
        readonly IRepositoryWrapper _repositoryWrapper;
        readonly IMapper _mapper;
        readonly ILoggerManager _loggerManager;
        readonly IUtilService _utilService;
        IApiResourceMappingService _ApiResourceMappingService;
        public ServiceWrapper(
             IAddressService AddressService,
             ICustomerService CustomerService,
             IDepartmentService DepartmentService,
             IEquipmentService EquipmentService,
             IEquipmentCategoryService EquipmentCategoryService,
             IExpenseService ExpenseService,
             IExpenseTypeService ExpenseTypeService,
             IFactoryService FactoryService,
             IIncomeService IncomeService,
             IIncomeTypeService IncomeTypeService,
             IInvoiceService InvoiceService,
             IInvoiceTypeService InvoiceTypeService,
             IItemService ItemService,
             IItemCategoryService ItemCategoryService,
             IItemStatusService ItemStatusService,
             IPayableService PayableService,
             IPaymentStatusService PaymentStatusService,
             IPhoneService PhoneService,
             IProductionService ProductionService,
             IPurchaseService PurchaseService,
             IPurchaseTypeService PurchaseTypeService,
             IReceivableService ReceivableService,
             IRoleService RoleService,
             ISalesService SalesService,
             IStaffService StaffService,
             IStockService StockService,
             IStockInService StockInService,
             IStockOutService StockOutService,
             ISupplierService SupplierService,
             ITransactionService TransactionService,
             ITransactionTypeService TransactionTypeService,
             IUserAuthInfoService UserAuthInfoService,
             IUserRoleService UserRoleService,
             IRepositoryWrapper repositoryWrapper,
             IMapper mapper,
             ILoggerManager loggerManager,
             IUtilService utilService,
             IApiResourceMappingService ApiResourceMappingService
             )
        {
            this._AddressService = AddressService;
            this._CustomerService = CustomerService;
            this._DepartmentService = DepartmentService;
            this._EquipmentService = EquipmentService;
            this._EquipmentCategoryService = EquipmentCategoryService;
            this._ExpenseService = ExpenseService;
            this._ExpenseTypeService = ExpenseTypeService;
            this._FactoryService = FactoryService;
            this._IncomeService = IncomeService;
            this._IncomeTypeService = IncomeTypeService;
            this._InvoiceService = InvoiceService;
            this._InvoiceTypeService = InvoiceTypeService;
            this._ItemService = ItemService;
            this._ItemCategoryService = ItemCategoryService;
            this._ItemStatusService = ItemStatusService;
            this._PayableService = PayableService;
            this._PaymentStatusService = PaymentStatusService;
            this._PhoneService = PhoneService;
            this._ProductionService = ProductionService;
            this._PurchaseService = PurchaseService;
            this._PurchaseTypeService = PurchaseTypeService;
            this._ReceivableService = ReceivableService;
            this._RoleService = RoleService;
            this._SalesService = SalesService;
            this._StaffService = StaffService;
            this._StockService = StockService;
            this._StockInService = StockInService;
            this._StockOutService = StockOutService;
            this._SupplierService = SupplierService;
            this._TransactionService = TransactionService;
            this._TransactionTypeService = TransactionTypeService;
            this._UserAuthInfoService = UserAuthInfoService;
            this._UserRoleService = UserRoleService;
            this._repositoryWrapper = repositoryWrapper;
            this._mapper = mapper;
            this._loggerManager = loggerManager;
            this._utilService = utilService;
            this._ApiResourceMappingService = ApiResourceMappingService;
        }

        public IApiResourceMappingService ApiResourceMappingService
        {
            get
            {
                if (_ApiResourceMappingService == null)
                {
                    //return new AddressRepository(dbContext);
                    _ApiResourceMappingService = new ApiResourceMappingService(_repositoryWrapper, _utilService);
                }
                return _ApiResourceMappingService;
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                if (_CustomerService == null)
                {
                    //return new AddressRepository(dbContext);
                    _CustomerService = new CustomerService(_repositoryWrapper, _utilService);
                }
                return _CustomerService;
            }
        }

        public IAddressService AddressService
        {
            get
            {
                if (_AddressService == null)
                {
                    _AddressService = new AddressService(_repositoryWrapper,  _utilService);
                }
                return _AddressService;
            }
        }
        public IDepartmentService DepartmentService
        {
            get
            {
                if (_DepartmentService == null)
                {
                    _DepartmentService = new DepartmentService(_repositoryWrapper,  _utilService);
                }
                return _DepartmentService;
            }
        }
        public IEquipmentService EquipmentService
        {
            get
            {
                if (_EquipmentService == null)
                {
                    _EquipmentService = new EquipmentService(_repositoryWrapper,  _utilService);
                }
                return _EquipmentService;
            }
        }
        public IEquipmentCategoryService EquipmentCategoryService
        {
            get
            {
                if (_EquipmentCategoryService == null)
                {
                    _EquipmentCategoryService = new EquipmentCategoryService(_repositoryWrapper,  _utilService);
                }
                return _EquipmentCategoryService;
            }
        }
        public IExpenseService ExpenseService
        {
            get
            {
                if (_ExpenseService == null)
                {
                    _ExpenseService = new ExpenseService(_repositoryWrapper,  _utilService);
                }
                return _ExpenseService;
            }
        }
        public IExpenseTypeService ExpenseTypeService
        {
            get
            {
                if (_ExpenseTypeService == null)
                {
                    _ExpenseTypeService = new ExpenseTypeService(_repositoryWrapper,  _utilService);
                }
                return _ExpenseTypeService;
            }
        }
        public IFactoryService FactoryService
        {
            get
            {
                if (_FactoryService == null)
                {
                    _FactoryService = new FactoryService(_repositoryWrapper,  _utilService);
                }
                return _FactoryService;
            }
        }
        public IIncomeService IncomeService
        {
            get
            {
                if (_IncomeService == null)
                {
                    _IncomeService = new IncomeService(_repositoryWrapper, _utilService);
                }
                return _IncomeService;
            }
        }
        public IIncomeTypeService IncomeTypeService
        {
            get
            {
                if (_IncomeTypeService == null)
                {
                    _IncomeTypeService = new IncomeTypeService(_repositoryWrapper,  _utilService);
                }
                return _IncomeTypeService;
            }
        }
        public IInvoiceService InvoiceService
        {
            get
            {
                if (_InvoiceService == null)
                {
                    _InvoiceService = new InvoiceService(_repositoryWrapper,  _utilService);
                }
                return _InvoiceService;
            }
        }
        public IInvoiceTypeService InvoiceTypeService
        {
            get
            {
                if (_InvoiceTypeService == null)
                {
                    _InvoiceTypeService = new InvoiceTypeService(_repositoryWrapper,  _utilService);
                }
                return _InvoiceTypeService;
            }
        }
        public IItemService ItemService
        {
            get
            {
                if (_ItemService == null)
                {
                    _ItemService = new ItemService(_repositoryWrapper,   _utilService);
                }
                return _ItemService;
            }
        }
        public IItemCategoryService ItemCategoryService
        {
            get
            {
                if (_ItemCategoryService == null)
                {
                    _ItemCategoryService = new ItemCategoryService(_repositoryWrapper,   _utilService);
                }
                return _ItemCategoryService;
            }
        }
        public IItemStatusService ItemStatusService
        {
            get
            {
                if (_ItemStatusService == null)
                {
                    _ItemStatusService = new ItemStatusService(_repositoryWrapper,  _utilService);
                }
                return _ItemStatusService;
            }
        }
        public IPayableService PayableService
        {
            get
            {
                if (_PayableService == null)
                {
                    _PayableService = new PayableService(_repositoryWrapper,  _utilService);
                }
                return _PayableService;
            }
        }
        public IPaymentStatusService PaymentStatusService
        {
            get
            {
                if (_PaymentStatusService == null)
                {
                    _PaymentStatusService = new PaymentStatusService(_repositoryWrapper,  _utilService);
                }
                return _PaymentStatusService;
            }
        }
        public IPhoneService PhoneService
        {
            get
            {
                if (_PhoneService == null)
                {
                    _PhoneService = new PhoneService(_repositoryWrapper,  _utilService);
                }
                return _PhoneService;
            }
        }
        public IProductionService ProductionService
        {
            get
            {
                if (_ProductionService == null)
                {
                    _ProductionService = new ProductionService(_repositoryWrapper,  _utilService);
                }
                return _ProductionService;
            }
        }
        public IPurchaseService PurchaseService
        {
            get
            {
                if (_PurchaseService == null)
                {
                    _PurchaseService = new PurchaseService(_repositoryWrapper,  _utilService);
                }
                return _PurchaseService;
            }
        }
        public IPurchaseTypeService PurchaseTypeService
        {
            get
            {
                if (_PurchaseTypeService == null)
                {
                    _PurchaseTypeService = new PurchaseTypeService(_repositoryWrapper,  _utilService);
                }
                return _PurchaseTypeService;
            }
        }
        public IReceivableService ReceivableService
        {
            get
            {
                if (_ReceivableService == null)
                {
                    _ReceivableService = new ReceivableService(_repositoryWrapper,  _utilService);
                }
                return _ReceivableService;
            }
        }
        public IRoleService RoleService
        {
            get
            {
                if (_RoleService == null)
                {
                    _RoleService = new RoleService(_repositoryWrapper,  _utilService);
                }
                return _RoleService;
            }
        }
        public ISalesService SalesService
        {
            get
            {
                if (_SalesService == null)
                {
                    _SalesService = new SalesService(_repositoryWrapper,  _utilService);
                }
                return _SalesService;
            }
        }
        public IStaffService StaffService
        {
            get
            {
                if (_StaffService == null)
                {
                    _StaffService = new StaffService(_repositoryWrapper,  _utilService);
                }
                return _StaffService;
            }
        }
        public IStockService StockService
        {
            get
            {
                if (_StockService == null)
                {
                    _StockService = new StockService(_repositoryWrapper,  _utilService);
                }
                return _StockService;
            }
        }
        public IStockInService StockInService
        {
            get
            {
                if (_StockInService == null)
                {
                    _StockInService = new StockInService(_repositoryWrapper,  _utilService);
                }
                return _StockInService;
            }
        }
        public IStockOutService StockOutService
        {
            get
            {
                if (_StockOutService == null)
                {
                    _StockOutService = new StockOutService(_repositoryWrapper,  _utilService);
                }
                return _StockOutService;
            }
        }
        public ISupplierService SupplierService
        {
            get
            {
                if (_SupplierService == null)
                {
                    _SupplierService = new SupplierService(_repositoryWrapper,  _utilService);
                }
                return _SupplierService;
            }
        }
        public ITransactionService TransactionService
        {
            get
            {
                if (_TransactionService == null)
                {
                    _TransactionService = new TransactionService(_repositoryWrapper,  _utilService);
                }
                return _TransactionService;
            }
        }
        public ITransactionTypeService TransactionTypeService
        {
            get
            {
                if (_TransactionTypeService == null)
                {
                    _TransactionTypeService = new TransactionTypeService(_repositoryWrapper,  _utilService);
                }
                return _TransactionTypeService;
            }
        }
        public IUserAuthInfoService UserAuthInfoService
        {
            get
            {
                if (_UserAuthInfoService == null)
                {
                    _UserAuthInfoService = new UserAuthInfoService(_repositoryWrapper,  _utilService);
                }
                return _UserAuthInfoService;
            }
        }
        public IUserRoleService UserRoleService
        {
            get
            {
                if (_UserRoleService == null)
                {
                    _UserRoleService = new UserRoleService(_repositoryWrapper,  _utilService);
                }
                return _UserRoleService;
            }

        }
    }
}
