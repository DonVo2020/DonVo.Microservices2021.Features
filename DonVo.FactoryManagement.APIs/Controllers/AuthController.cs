using DonVo.FactoryManagement.APIs.Utilities.Auth;
using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Auth;
using DonVo.FactoryManagement.Models.ViewModels.Role;
using DonVo.FactoryManagement.Models.ViewModels.UserAuthInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.APIs.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly FactoryManagementContext _context;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IServiceWrapper _serviceWrapper;
        //private readonly ILoggerManager _logger;
        private readonly IUtilService _utilService;
        //private readonly IBusinessService _businessService;

        public AuthController(
            //FactoryManagementContext context,
            IRepositoryWrapper repositoryWrapper,
            IServiceWrapper serviceWrapper,
            //ILoggerManager logger,
            IUtilService utilService
            //IBusinessService businessService
            )
        {
            //_context = context;
            _repositoryWrapper = repositoryWrapper;
            _serviceWrapper = serviceWrapper;
            //_logger = logger;
            _utilService = utilService;
            //_businessService = businessService;
        }

        #region Role

        [HttpPost]
        [Route("Role/getAll")]
        public async Task<ActionResult<WrapperRoleListVM>> GetAllRole([FromBody]GetDataListVM dataParam)
        {
            var data = await _serviceWrapper.RoleService.GetListPaged(dataParam);
            return Ok(data);
        }

        [FactoryAuthorize]
        [HttpPost]
        [Route("Role/getByUserId")]
        public async Task<ActionResult<RoleVM>> GetRoleByUserId([FromBody]GetDataListHistory dataParam)
        {
            //var Role = await _context.Role.FindAsync(id);
            IEnumerable<UserRole> roleList = await  _repositoryWrapper
                             .UserRole
                             .FindAll()
                             .Where(x => x.UserId == dataParam.ClientId)
                             .Include(x => x.Role)
                             .Include(x => x.UserAuthInfo)
                             .ToListAsync();

            RoleVM roleVM = _utilService.Mapper.Map<Role, RoleVM>(roleList.FirstOrDefault().Role);
            return roleVM;
        }

        [Route("Role/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperRoleListVM>> UpdateRole(string id, [FromBody]RoleVM Role)
        {
            return await _serviceWrapper.RoleService.Update(id, Role);
        }

        [HttpPost]
        [Route("Role/add")]
        public async Task<ActionResult<WrapperRoleListVM>> AddRole([FromBody]RoleVM Role)
        {
            return await _serviceWrapper.RoleService.Add(Role);
        }

        [HttpPost]
        [Route("Role/delete")]
        public async Task<ActionResult<WrapperRoleListVM>> DeleteRole([FromBody]RoleVM itemVM)
        {
            return await _serviceWrapper.RoleService.Delete(itemVM);
        }
        #endregion

        #region UserAuthInfo

        [HttpPost]
        [Route("UserAuthInfo/getAll")]
        public async Task<ActionResult<WrapperUserAuthInfoListVM>> GetAllUserAuthInfo([FromBody]GetDataListVM dataParam)
        {
            var data = await _serviceWrapper.UserAuthInfoService.GetListPaged(dataParam);
            return Ok(data);
        }

        [HttpPost]
        [Route("UserAuthInfo/getById")]
        public async Task<ActionResult<UserAuthInfo>> GetUserAuthInfo(string id)
        {
            //var UserAuthInfo = await _context.UserAuthInfo.FindAsync(id);
            var enumerables = await _repositoryWrapper.UserAuthInfo.FindByConditionAsync(x => x.Id == id);
            var UserAuthInfo = enumerables.ToList().FirstOrDefault();
            if (UserAuthInfo == null)
            {
                return NotFound();
            }

            return UserAuthInfo;
        }

        [Route("UserAuthInfo/update/{id}")]
        [HttpPost]
        public async Task<ActionResult<WrapperUserAuthInfoListVM>> UpdateUserAuthInfo(string id, [FromBody]UserAuthInfoVM UserAuthInfo)
        {
            return await _serviceWrapper.UserAuthInfoService.Update(id, UserAuthInfo);
        }

        [HttpPost]
        [Route("UserAuthInfo/add")]
        public async Task<ActionResult<WrapperUserAuthInfoListVM>> AddUserAuthInfo([FromBody]UserAuthInfoVM UserAuthInfo)
        {
            return await _serviceWrapper.UserAuthInfoService.Add(UserAuthInfo);
        }

        [HttpPost]
        [Route("UserAuthInfo/delete")]
        public async Task<ActionResult<WrapperUserAuthInfoListVM>> DeleteUserAuthInfo([FromBody]UserAuthInfoVM itemVM)
        {
            return await _serviceWrapper.UserAuthInfoService.Delete(itemVM);
        }
        #endregion

        [HttpPost]
        [Route("login")]
        public ActionResult<LoginResponseVM> Login([FromBody]LoginVM user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            LoginResponseVM resp = _serviceWrapper.UserAuthInfoService.IsUserAuthentic(user);
            return resp;
        }

        [HttpPost, Route("userNameExist")]
        public ActionResult<bool> IsUserNameExist([FromBody]LoginVM user)
        {
            return _serviceWrapper.UserAuthInfoService.IsUserExist(user);
        }

        [HttpGet("gettoken")]
        public object GetToken()
        {
            string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
            var issuer = "http://mysite.com";  //normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("valid", "1"),
                new Claim("userid", "1"),
                new Claim("name", "bilal")
            };

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };
        }
    }
}