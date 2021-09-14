using DonVo.FactoryManagement.APIs.Utilities.Auth;
using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.CustomerView;
using DonVo.FactoryManagement.Models.ViewModels.Staff;
using DonVo.FactoryManagement.Models.ViewModels.Supplier;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.APIs.Controllers
{
    [FactoryAuthorize]
    [Route("api/hr")]
    [ApiController]
    public class HRController : ControllerBase
    {
        //private readonly FactoryManagementContext _context;
        //private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IServiceWrapper _serviceWrapper;
        //private readonly ILoggerManager _logger;
        private readonly IUtilService _utilService;
        public HRController(
            //FactoryManagementContext context, 
            //IRepositoryWrapper repositoryWrapper, 
            IServiceWrapper serviceWrapper, 
            //ILoggerManager logger, 
            IUtilService utilService)
        {
            //_context = context;
            //_repositoryWrapper = repositoryWrapper;
            _serviceWrapper = serviceWrapper;
            //_logger = logger;
            _utilService = utilService;
        }

        #region Customer
        [HttpPost]
        [Route("customer/getAll")]
        public async Task<ActionResult<WrapperListCustomerVM>> GetAllCustomer(GetDataListVM customer)
        {
            var data = await _serviceWrapper.CustomerService.GetListPaged(customer,true);
            _utilService.Log("Customer Successfully Getted");
            return Ok(data);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [Route("customer/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperListCustomerVM>> UpdateCustomer(string id, [FromBody]CustomerVM customer)
        {
            WrapperListCustomerVM result = new();
            result = await _serviceWrapper.CustomerService.Update(id, customer);
            _utilService.Log("Customer Successfully Updated");
            return Ok(result);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [Route("customer/add")]
        [HttpPost]
        public async Task<ActionResult<WrapperListCustomerVM>> AddCustomer([FromBody]CustomerVM customerVM)
        {
            WrapperListCustomerVM result = new();
            result = await _serviceWrapper.CustomerService.Add(customerVM);
            _utilService.Log("Customer Successfully Added");
            return Ok(result);
        }

        [HttpPost]
        [Route("customer/delete")]
        public async Task<ActionResult<WrapperListCustomerVM>> DeleteCustomer([FromBody]CustomerVM customerTemp)
        {
            WrapperListCustomerVM data = new();
            data = await _serviceWrapper.CustomerService.Delete(customerTemp);
            _utilService.Log("Customer Successfully Deleted");
            return data;
        }

        #endregion

        #region Staff
        [HttpPost]
        [Route("staff/getAll")]
        public async Task<ActionResult<WrapperStaffListVM>> GetAllStaff(GetDataListVM temp)
        {
            var data = await _serviceWrapper.StaffService.GetListPaged(temp,true);
            _utilService.Log("Staff Successfully Getted");
            return Ok(data);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [Route("staff/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperStaffListVM>> UpdateStaff(string id, [FromBody]StaffVM temp)
        {
            WrapperStaffListVM result = new();
            result = await _serviceWrapper.StaffService.Update(id, temp);
            _utilService.Log("Staff Successfully Updated");
            return Ok(result);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [Route("staff/add")]
        [HttpPost]
        public async Task<ActionResult<WrapperStaffListVM>> AddStaff([FromBody]StaffVM VM)
        {
            WrapperStaffListVM result = new();
            result = await _serviceWrapper.StaffService.Add(VM);
            _utilService.Log("Staff Successfully Added");
            return Ok(result);
        }

        [Route("staff/addToAdmin")]
        [HttpPost]
        public async Task<ActionResult<WrapperStaffListVM>> AddToStaff([FromBody]StaffVM VM)
        {
            WrapperStaffListVM result = new();
            result = await _serviceWrapper.StaffService.AddToIT_Admin(VM);
            _utilService.Log("Staff Successfully Added");
            return Ok(result);
        }

        [HttpPost]
        [Route("staff/delete")]
        public async Task<ActionResult<WrapperStaffListVM>> DeleteStaff([FromBody]StaffVM Temp)
        {
           WrapperStaffListVM data = await _serviceWrapper.StaffService.Delete(Temp);
            _utilService.Log("Staff Successfully Deleted");
            return data;
        }
        #endregion

        #region Supplier
        [HttpPost]
        [Route("supplier/getAll")]
        public async Task<ActionResult<WrapperSupplierListVM>> GetAllSupplier(GetDataListVM temp)
        {
            var data = await _serviceWrapper.SupplierService.GetListPaged(temp,true);
            _utilService.Log("Supplier Successfully Getted");
            return Ok(data);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [Route("supplier/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperSupplierListVM>> UpdateSupplier(string id, [FromBody]SupplierVM temp)
        {
            WrapperSupplierListVM result = new();
            result = await _serviceWrapper.SupplierService.Update(id, temp);
            _utilService.Log("Supplier Successfully Updated");
            return Ok(result);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        [Route("supplier/add")]
        [HttpPost]
        public async Task<ActionResult<WrapperSupplierListVM>> AddSupplier([FromBody]SupplierVM VM)
        {
            WrapperSupplierListVM result = new();
            result = await _serviceWrapper.SupplierService.Add(VM);
            _utilService.Log("Supplier Successfully Added");
            return Ok(result);
        }

        [HttpPost]
        [Route("supplier/delete")]
        public async Task<ActionResult<WrapperSupplierListVM>> DeleteSupplier([FromBody]SupplierVM Temp)
        {
            WrapperSupplierListVM vb = new();
            vb = await _serviceWrapper.SupplierService.Delete(Temp);
            _utilService.Log("Supplier Successfully Deleted");
            return vb;
        }

        #endregion
    }
}