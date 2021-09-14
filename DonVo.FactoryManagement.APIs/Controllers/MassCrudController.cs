using DonVo.FactoryManagement.APIs.Utilities.Auth;
using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.CustomerView;
using DonVo.FactoryManagement.Models.ViewModels.Department;
using DonVo.FactoryManagement.Models.ViewModels.Equipment;
using DonVo.FactoryManagement.Models.ViewModels.EquipmentCategory;
using DonVo.FactoryManagement.Models.ViewModels.Expense;
using DonVo.FactoryManagement.Models.ViewModels.ExpenseType;
using DonVo.FactoryManagement.Models.ViewModels.Factory;
using DonVo.FactoryManagement.Models.ViewModels.Income;
using DonVo.FactoryManagement.Models.ViewModels.IncomeType;
using DonVo.FactoryManagement.Models.ViewModels.InvoiceType;
using DonVo.FactoryManagement.Models.ViewModels.Item;
using DonVo.FactoryManagement.Models.ViewModels.ItemCategoryView;
using DonVo.FactoryManagement.Models.ViewModels.ItemStatus;
using DonVo.FactoryManagement.Models.ViewModels.PaymentStatus;
using DonVo.FactoryManagement.Models.ViewModels.Production;
using DonVo.FactoryManagement.Models.ViewModels.Stock;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.APIs.Controllers
{
    [FactoryAuthorize]
    [Route("api")]
    [ApiController]
    public class MassCrudController : ControllerBase
    {
        //private readonly FactoryManagementContext _context;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IServiceWrapper _serviceWrapper;
        private readonly ILoggerManager _logger;
        private readonly IUtilService _utilService;
        public MassCrudController(
            //FactoryManagementContext context, 
            IRepositoryWrapper repositoryWrapper, 
            IServiceWrapper serviceWrapper, 
            ILoggerManager logger,
            IUtilService utilService)
        {
            //_context = context;
            _repositoryWrapper = repositoryWrapper;
            _serviceWrapper = serviceWrapper;
            _logger = logger;
            _utilService = utilService;
        }

        #region ExpenseType
        [HttpPost]
        [Route("ExpenseType/getAll")]
        public async Task<ActionResult<WrapperExpenseTypeListVM>> GetAllExpenseType([FromBody]GetDataListVM customer)
        {
            return await _serviceWrapper.ExpenseTypeService.GetListPaged(customer);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [Route("ExpenseType/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperExpenseTypeListVM>> UpdateExpenseType(string id, [FromBody]ExpenseTypeVM ExpenseType)
        {
            return await _serviceWrapper.ExpenseTypeService.Update(id, ExpenseType);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [HttpPost]
        [Route("ExpenseType/add")]
        public async Task<ActionResult<WrapperExpenseTypeListVM>> AddExpenseType([FromBody]ExpenseTypeVM ExpenseType)
        {
            return await _serviceWrapper.ExpenseTypeService.Add(ExpenseType);
        }

        [HttpPost]
        [Route("ExpenseType/delete")]
        public async Task<ActionResult<WrapperExpenseTypeListVM>> DeleteExpenseType([FromBody]ExpenseTypeVM itemVM)
        {
            return await _serviceWrapper.ExpenseTypeService.Delete(itemVM);
        }

        #endregion

        #region IncomeType
        [HttpPost]
        [Route("IncomeType/getAll")]
        public async Task<ActionResult<WrapperIncomeTypeListVM>> GetAllIncomeType([FromBody]GetDataListVM customer)
        {
            return await _serviceWrapper.IncomeTypeService.GetListPaged(customer);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [Route("IncomeType/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperIncomeTypeListVM>> UpdateIncomeType(string id, [FromBody]IncomeTypeVM IncomeType)
        {
            return await _serviceWrapper.IncomeTypeService.Update(id, IncomeType);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [HttpPost]
        [Route("IncomeType/add")]
        public async Task<ActionResult<WrapperIncomeTypeListVM>> AddIncomeType([FromBody]IncomeTypeVM IncomeType)
        {
            return await _serviceWrapper.IncomeTypeService.Add(IncomeType);
        }

        [HttpPost]
        [Route("IncomeType/delete")]
        public async Task<ActionResult<WrapperIncomeTypeListVM>> DeleteIncomeType([FromBody]IncomeTypeVM itemVM)
        {
            return await _serviceWrapper.IncomeTypeService.Delete(itemVM);
        }

        #endregion

        #region InvoiceType
        [HttpPost]
        [Route("InvoiceType/getAll")]
        public async Task<ActionResult<WrapperInvoiceTypeListVM>> GetAllInvoiceType([FromBody]GetDataListVM customer)
        {
            return await _serviceWrapper.InvoiceTypeService.GetListPaged(customer);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [Route("InvoiceType/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperInvoiceTypeListVM>> UpdateInvoiceType(string id, [FromBody]InvoiceTypeVM InvoiceType)
        {
            return await _serviceWrapper.InvoiceTypeService.Update(id, InvoiceType);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [HttpPost]
        [Route("InvoiceType/add")]
        public async Task<ActionResult<WrapperInvoiceTypeListVM>> AddInvoiceType([FromBody]InvoiceTypeVM InvoiceType)
        {
            return await _serviceWrapper.InvoiceTypeService.Add(InvoiceType);
        }

        [HttpPost]
        [Route("InvoiceType/delete")]
        public async Task<ActionResult<WrapperInvoiceTypeListVM>> DeleteInvoiceType([FromBody]InvoiceTypeVM itemVM)
        {
            return await _serviceWrapper.InvoiceTypeService.Delete(itemVM);
        }

        #endregion

        #region Department
        [HttpPost]
        [Route("Department/getAll")]
        public async Task<ActionResult<WrapperDepartmentListVM>> GetAllDepartment([FromBody]GetDataListVM customer)
        {
            return await _serviceWrapper.DepartmentService.GetListPaged(customer);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [Route("Department/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperDepartmentListVM>> UpdateDepartment(string id, [FromBody]DepartmentVM Department)
        {
            return await _serviceWrapper.DepartmentService.Update(id, Department);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [HttpPost]
        [Route("Department/add")]
        public async Task<ActionResult<WrapperDepartmentListVM>> AddDepartment([FromBody]DepartmentVM Department)
        {
            return await _serviceWrapper.DepartmentService.Add(Department);
        }

        [HttpPost]
        [Route("Department/delete")]
        public async Task<ActionResult<WrapperDepartmentListVM>> DeleteDepartment([FromBody]DepartmentVM itemVM)
        {
            return await _serviceWrapper.DepartmentService.Delete(itemVM);
        }

        #endregion

        #region Equipment
        [HttpPost]
        [Route("Equipment/getAll")]
        public async Task<ActionResult<WrapperEquipmentListVM>> GetAllEquipment([FromBody]GetDataListVM customer)
        {
            return await _serviceWrapper.EquipmentService.GetListPaged(customer);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [Route("Equipment/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperEquipmentListVM>> UpdateEquipment(string id, [FromBody]EquipmentVM Equipment)
        {
            return await _serviceWrapper.EquipmentService.Update(id, Equipment);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [HttpPost]
        [Route("Equipment/add")]
        public async Task<ActionResult<WrapperEquipmentListVM>> AddEquipment([FromBody]EquipmentVM Equipment)
        {
            return await _serviceWrapper.EquipmentService.Add(Equipment);
        }

        [HttpPost]
        [Route("Equipment/delete")]
        public async Task<ActionResult<WrapperEquipmentListVM>> DeleteEquipment([FromBody]EquipmentVM itemVM)
        {
            return await _serviceWrapper.EquipmentService.Delete(itemVM);
        }

        #endregion       

        #region EquipmentCategory
        [HttpPost]
        [Route("EquipmentCategory/getAll")]
        public async Task<ActionResult<WrapperEquipmentCategoryListVM>> GetAllEquipmentCategory([FromBody]GetDataListVM customer)
        {
            return await _serviceWrapper.EquipmentCategoryService.GetListPaged(customer);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [Route("EquipmentCategory/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperEquipmentCategoryListVM>> UpdateEquipmentCategory(string id, [FromBody]EquipmentCategoryVM EquipmentCategory)
        {
            return await _serviceWrapper.EquipmentCategoryService.Update(id, EquipmentCategory);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [HttpPost]
        [Route("EquipmentCategory/add")]
        public async Task<ActionResult<WrapperEquipmentCategoryListVM>> AddEquipmentCategory([FromBody]EquipmentCategoryVM EquipmentCategory)
        {
            return await _serviceWrapper.EquipmentCategoryService.Add(EquipmentCategory);
        }

        [HttpPost]
        [Route("EquipmentCategory/delete")]
        public async Task<ActionResult<WrapperEquipmentCategoryListVM>> DeleteEquipmentCategory([FromBody]EquipmentCategoryVM itemVM)
        {
            return await _serviceWrapper.EquipmentCategoryService.Delete(itemVM);
        }

        #endregion

        #region Production
        [HttpPost]
        [Route("Production/getAll")]
        public async Task<ActionResult<WrapperProductionListVM>> GetAllProduction([FromBody]GetDataListVM customer)
        {
            return await _serviceWrapper.ProductionService.GetListPaged(customer);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [Route("Production/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperProductionListVM>> UpdateProduction(string id, [FromBody]EditProductionVM Production)
        {
            return await _serviceWrapper.ProductionService.Update(id, Production);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [HttpPost]
        [Route("Production/add")]
        public async Task<ActionResult<WrapperProductionListVM>> AddProduction([FromBody]List<AddProductionVM> Production)
        {
            return await _serviceWrapper.ProductionService.AddProductions(Production);
        }

        [HttpPost]
        [Route("Production/delete")]
        public async Task<ActionResult<WrapperProductionListVM>> DeleteProduction([FromBody]AddProductionVM itemVM)
        {
            return await _serviceWrapper.ProductionService.Delete(itemVM);
        }

        #endregion

        #region Stock
        [HttpPost]
        [Route("stock/getAll")]
        public async Task<ActionResult<WrapperStockListVM>> GetAllStock(GetDataListVM temp)
        {
            var data = await _serviceWrapper.StockService.GetListPaged(temp);
            _utilService.Log("Stock Successfully Getted");
            return Ok(data);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [Route("stock/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperStockListVM>> UpdateStock(string id, [FromBody]StockVM temp)
        {
            WrapperStockListVM result = new();
            result = await _serviceWrapper.StockService.Update(id, temp);
            _utilService.Log("Stock Successfully Updated");
            return Ok(result);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [Route("stock/add")]
        [HttpPost]
        public async Task<ActionResult<WrapperStockListVM>> AddStock([FromBody]StockVM VM)
        {
            WrapperStockListVM result = new();
            VM.ExpiryDate = VM.ExpiryDate.ToLocalTime();
            result = await _serviceWrapper.StockService.Add(VM);
            _utilService.Log("Stock Successfully Added");
            return Ok(result);
        }

        [HttpPost]
        [Route("stock/delete")]
        public async Task<ActionResult<WrapperStockListVM>> DeleteStock([FromBody]StockVM Temp)
        {
            WrapperStockListVM vb = new();
            vb = await _serviceWrapper.StockService.Delete(Temp);
            _utilService.Log("Stock Successfully Deleted");
            return vb;
        }

        #endregion

        #region ItemCategory
        // GET: api/ItemCategories
        [HttpPost]
        [Route("ItemCategory/getAll")]
        public async Task<ActionResult<WrapperItemCategoryListVM>> GetAllItemCategory([FromBody]GetDataListVM customer)
        {
            return await _serviceWrapper.ItemCategoryService.GetListPaged(customer);
        }

        // GET: api/ItemCategories/5
        [HttpPost]
        [Route("ItemCategory/getById")]
        public async Task<ActionResult<ItemCategory>> GetItemCategory(string id)
        {
            //var itemCategory = await _context.ItemCategory.FindAsync(id);
            var enumerables = await _repositoryWrapper.ItemCategory.FindByConditionAsync(x => x.Id == id);
            var itemCategory = enumerables.ToList().FirstOrDefault();
            if (itemCategory == null)
            {
                return NotFound();
            }

            return itemCategory;
        }

        // PUT: api/ItemCategories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Route("ItemCategory/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperItemCategoryListVM>> UpdateItemCategory(string id, [FromBody]ItemCategoryVM itemCategory)
        {
            return await _serviceWrapper.ItemCategoryService.Update(id, itemCategory);
        }

        // POST: api/ItemCategories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Route("ItemCategory/add")]
        public async Task<ActionResult<WrapperItemCategoryListVM>> AddItemCategory([FromBody]ItemCategoryVM itemCategory)
        {
            return await _serviceWrapper.ItemCategoryService.Add(itemCategory);
        }

        // DELETE: api/ItemCategories/5
        [HttpPost]
        [Route("ItemCategory/delete")]
        public async Task<ActionResult<WrapperItemCategoryListVM>> DeleteItemCategory([FromBody]ItemCategoryVM itemVM)
        {
            return await _serviceWrapper.ItemCategoryService.Delete(itemVM);
        }
        //private bool ItemCategoryExists(string name)
        //{
        //    return _context.ItemCategory.Any(e => e.Name == name);
        //}

        #endregion

        #region Item
        [HttpPost]
        [Route("Item/getAll")]
        public async Task<ActionResult<WrapperItemListVM>> GetAllItem([FromBody]GetDataListVM dataParam)
        {
            var data = await _serviceWrapper.ItemService.GetListPaged(dataParam);
            return Ok(data);
        }

        [HttpPost]
        [Route("Item/getById")]
        public async Task<ActionResult<Item>> GetItem(string id)
        {
            // var item = await _context.Item.FindAsync(id);
            var enumerables = await _repositoryWrapper.Item.FindByConditionAsync(x => x.Id == id);
            var item = enumerables.ToList().FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Route("Item/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperItemListVM>> UpdateItem(string id, [FromBody]ItemVM item)
        {
            return Ok(await _serviceWrapper.ItemService.Update(id, item));
        }

        [HttpPost]
        [Route("Item/add")]
        public async Task<ActionResult<WrapperItemListVM>> AddItem([FromBody]ItemVM item)
        {
            return Ok(await _serviceWrapper.ItemService.Add(item));
        }

        [HttpPost]
        [Route("Item/delete")]
        public async Task<ActionResult<WrapperItemListVM>> DeleteItem([FromBody]ItemVM itemVM)
        {
            return Ok(await _serviceWrapper.ItemService.Delete(itemVM));
        }

        //private bool ItemExists(string CategoryId, decimal? UnitPrice, string Name)
        //{
        //    return _context.Item.Any(e => e.CategoryId == CategoryId && e.UnitPrice == UnitPrice && e.Name == Name);
        //}
        #endregion

        #region ItemStatus

        [HttpPost]
        [Route("Item/status/getAll")]
        public async Task<ActionResult<WrapperItemStatusListVM>> GetAllItemStatus([FromBody]GetDataListVM dataParam)
        {
            var data = await _serviceWrapper.ItemStatusService.GetListPaged(dataParam);
            return Ok(data);
        }

        [HttpPost]
        [Route("Item/status/getById")]
        public async Task<ActionResult<ItemStatus>> GetItemStatus(string id)
        {
            //var ItemStatus = await _context.ItemStatus.FindAsync(id);
            var enumerables = await _repositoryWrapper.ItemStatus.FindByConditionAsync(x => x.Id == id);
            var ItemStatus = enumerables.ToList().FirstOrDefault();
            if (ItemStatus == null)
            {
                return NotFound();
            }

            return ItemStatus;
        }

        [Route("Item/status/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperItemStatusListVM>> UpdateItemStatus(string id, [FromBody]ItemStatusVM ItemStatus)
        {
            return await _serviceWrapper.ItemStatusService.Update(id, ItemStatus);
        }

        [HttpPost]
        [Route("Item/status/add")]
        public async Task<ActionResult<WrapperItemStatusListVM>> AddItemStatus([FromBody]ItemStatusVM ItemStatus)
        {
            return await _serviceWrapper.ItemStatusService.Add(ItemStatus);
        }

        // DELETE: api/ItemCategories/5
        [HttpPost]
        [Route("Item/status/delete")]
        public async Task<ActionResult<WrapperItemStatusListVM>> DeleteItemStatus([FromBody]ItemStatusVM itemVM)
        {
            return await _serviceWrapper.ItemStatusService.Delete(itemVM);
        }

        #endregion

        #region Customer
        // GET: api/Customers/ii
        [HttpPost]
        [Route("Customer/getAll")]
        public async Task<ActionResult<WrapperListCustomerVM>> GetAllCustomer(GetDataListVM customer)
        {
            var data = await _serviceWrapper.CustomerService.GetListPaged(customer,true);
            return Ok(data);
        }

        [Route("Customer/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperListCustomerVM>> UpdateCustomer(string id, [FromBody]CustomerVM customer)
        {
            WrapperListCustomerVM result = new();
            result = await _serviceWrapper.CustomerService.Update(id, customer);
            _logger.LogInfo("Customer Successfully Updated");
            return Ok(result);
        }

        [Route("Customer/add")]
        [HttpPost]
        public async Task<ActionResult<WrapperListCustomerVM>> AddCustomer([FromBody]CustomerVM customerVM)
        {
            WrapperListCustomerVM result = new();
            result = await _serviceWrapper.CustomerService.Add(customerVM);
            _logger.LogInfo("Customer Successfully Added");
            return Ok(result);
        }

        // DELETE: api/Customers/5
        [HttpPost]
        [Route("Customer/delete")]
        public async Task<ActionResult<WrapperListCustomerVM>> DeleteCustomer([FromBody]CustomerVM customerTemp)
        {
            return await _serviceWrapper.CustomerService.Delete(customerTemp);
        }

        //private bool CustomerExists(string Name, string Email)
        //{
        //    return _context.Customer.Any(e => e.Email == Email && e.Name == Name);
        //}
        #endregion

        #region Expense
        [HttpPost]
        [Route("Expense/getAll")]
        public async Task<ActionResult<WrapperExpenseListVM>> GetAllExpense([FromBody]GetDataListVM dataParam)
        {
            var data = await _serviceWrapper.ExpenseService.GetListPaged(dataParam);
            return Ok(data);
        }

        [HttpPost]
        [Route("Expense/add")]
        public async Task<ActionResult<WrapperExpenseListVM>> AddExpense([FromBody]ExpenseVM Expense)
        {

            return Ok(await _serviceWrapper.ExpenseService.Add(Expense));
        }

        [HttpPost]
        [Route("Expense/delete")]
        public async Task<ActionResult<WrapperExpenseListVM>> DeleteExpense([FromBody]ExpenseVM ExpenseVM)
        {
            return Ok(await _serviceWrapper.ExpenseService.Delete(ExpenseVM));
        }
        #endregion

        #region Income
        [HttpPost]
        [Route("Income/getAll")]
        public async Task<ActionResult<WrapperIncomeListVM>> GetAllIncome([FromBody]GetDataListVM dataParam)
        {
            var data = await _serviceWrapper.IncomeService.GetListPaged(dataParam);
            return Ok(data);
        }

        [HttpPost]
        [Route("Income/add")]
        public async Task<ActionResult<WrapperIncomeListVM>> AddIncome([FromBody]IncomeVM Income)
        {

            return Ok(await _serviceWrapper.IncomeService.Add(Income));
        }

        [HttpPost]
        [Route("Income/delete")]
        public async Task<ActionResult<WrapperIncomeListVM>> DeleteIncome([FromBody]IncomeVM IncomeVM)
        {
            return Ok(await _serviceWrapper.IncomeService.Delete(IncomeVM));
        }
        #endregion

        #region PaymentStatus

        [HttpPost]
        [Route("payment/status/getAll")]
        public async Task<ActionResult<WrapperPaymentStatusListVM>> GetAllPaymentStatus([FromBody]GetDataListVM dataParam)
        {
            var data = await _serviceWrapper.PaymentStatusService.GetListPaged(dataParam);
            return Ok(data);
        }

        // GET: api/ItemCategories/5
        [HttpPost]
        [Route("payment/status/getById")]
        public async Task<ActionResult<PaymentStatus>> GetPaymentStatus(string id)
        {
            //var PaymentStatus = await _context.PaymentStatus.FindAsync(id);
            var enumerables = await _repositoryWrapper.PaymentStatus.FindByConditionAsync(x => x.Id == id);
            var PaymentStatus = enumerables.ToList().FirstOrDefault();
            if (PaymentStatus == null)
            {
                return NotFound();
            }

            return PaymentStatus;
        }

        [Route("payment/status/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperPaymentStatusListVM>> UpdatePaymentStatus(string id, [FromBody]PaymentStatusVM PaymentStatus)
        {
            return await _serviceWrapper.PaymentStatusService.Update(id, PaymentStatus);
        }

        // POST: api/ItemCategories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Route("payment/status/add")]
        public async Task<ActionResult<WrapperPaymentStatusListVM>> AddPaymentStatus([FromBody]PaymentStatusVM PaymentStatus)
        {
            return await _serviceWrapper.PaymentStatusService.Add(PaymentStatus);
        }

        [HttpPost]
        [Route("payment/status/delete")]
        public async Task<ActionResult<WrapperPaymentStatusListVM>> DeletePaymentStatus([FromBody]PaymentStatusVM itemVM)
        {
            return await _serviceWrapper.PaymentStatusService.Delete(itemVM);
        }

        #endregion

        #region Factory

        [HttpPost]
        [Route("factory/getAll")]
        public async Task<ActionResult<WrapperFactoryListVM>> GetAllFactory([FromBody]GetDataListVM dataParam)
        {
            var data = await _serviceWrapper.FactoryService.GetListPaged(dataParam);
            return Ok(data);
        }

        [HttpPost]
        [Route("factory/getById")]
        public async Task<ActionResult<Factory>> GetFactory(string id)
        {
            //var Factory = await _context.Factory.FindAsync(id);
            var enumerables = await _repositoryWrapper.Factory.FindByConditionAsync(x => x.Id == id);
            var Factory = enumerables.ToList().FirstOrDefault();
            if (Factory == null)
            {
                return NotFound();
            }

            return Factory;
        }

        [Route("factory/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperFactoryListVM>> UpdateFactory(string id, [FromBody]FactoryVM Factory)
        {
            return await _serviceWrapper.FactoryService.Update(id, Factory);
        }

        [HttpPost]
        [Route("factory/add")]
        public async Task<ActionResult<WrapperFactoryListVM>> AddFactory([FromBody]FactoryVM Factory)
        {
            return await _serviceWrapper.FactoryService.Add(Factory);
        }

        [HttpPost]
        [Route("factory/delete")]
        public async Task<ActionResult<WrapperFactoryListVM>> DeleteFactory([FromBody]FactoryVM itemVM)
        {
            return await _serviceWrapper.FactoryService.Delete(itemVM);
        }
        #endregion
    }
}