using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels.ApiResourceMapping;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;


namespace DonVo.FactoryManagement.APIs.Utilities.Auth
{
    [AttributeUsage(AttributeTargets.All)]
    public class FactoryAuthorize : Attribute, IAuthorizationFilter
    {
        private readonly int i = 0;
        private IRepositoryWrapper _repositoryWrapper;
        private IServiceWrapper _serviceWrapper;
        private FactoryManagementContext _context;
        private IUtilService _utilService;

        //public FactoryAuthorize(IRepositoryWrapper repositoryWrapper,
        //    IServiceWrapper serviceWrapper)
        //{
        //    _repositoryWrapper = repositoryWrapper;
        //    _serviceWrapper = serviceWrapper;

        //}
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var services = filterContext.HttpContext.RequestServices;
            _repositoryWrapper = (IRepositoryWrapper)services.GetService(typeof(IRepositoryWrapper));
            _serviceWrapper = (IServiceWrapper)services.GetService(typeof(IServiceWrapper));
            _utilService = (IUtilService)services.GetService(typeof(IUtilService));
            _context = new FactoryManagementContext();


            var actionName = "";
            var ctrlName = "";
            filterContext.HttpContext.Request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues authorizationToken);


            if (filterContext != null)
            {
                var _token = authorizationToken.FirstOrDefault();

                if (_token != null && _token[7..] != "null")
                {
                    string authToken = _token;
                    if (authToken != null)
                    {
                        #region Decoding Token
                        JwtSecurityToken claims = _serviceWrapper.UserAuthInfoService.DecodeJwtToken(authorizationToken.FirstOrDefault()[7..]);
                        var userId = claims.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value;
                        var userName = claims.Claims.Where(x => x.Type == "UserName").FirstOrDefault().Value;
                        var factoryName = claims.Claims.Where(x => x.Type == "FactoryName").FirstOrDefault().Value;
                        var factoryId = claims.Claims.Where(x => x.Type == "FactoryId").FirstOrDefault().Value;

                        var mvcContext = filterContext;
                        bool hasAccessRight = false;
                        if (mvcContext?.ActionDescriptor is ControllerActionDescriptor descriptor)
                        {
                            actionName = descriptor.ActionName;
                            ctrlName = descriptor.ControllerName;
                        }
                        UserRole roleVm = GetUserRole(userId);
                        if (roleVm.Role.Name == "SUPER_ADMIN")
                        {
                            filterContext.HttpContext.Response.Headers.Add("authToken", authToken);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");
                            filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");
                            return;
                        }

                        ApiResourceMappingVM vm = GetApiResourceMapping(ctrlName, actionName);
                        // UserRole roleVm = GetUserRole(userId);

                        var property = vm.Resource == "Common" ? null : roleVm.Role.GetType().GetProperty(vm.Resource);
                        if (vm.Resource == "Common")
                        {
                            hasAccessRight = true;
                        }

                        else if (property != null)
                        {
                            hasAccessRight = (bool)property.GetValue(roleVm.Role);
                        }
                        #endregion
                        if (IsValidToken(authToken[7..]) && hasAccessRight)
                        {
                            filterContext.HttpContext.Response.Headers.Add("authToken", authToken);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");

                            filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");

                            return;
                        }
                        else
                        {
                            filterContext.HttpContext.Response.Headers.Add("authToken", authToken);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                            filterContext.Result = new JsonResult("NotAuthorized")
                            {
                                Value = new
                                {
                                    Status = "Error",
                                    Message = "Invalid Token"
                                },
                            };
                        }

                    }

                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Provide authToken";
                    filterContext.Result = new JsonResult("Please Provide authToken")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Please Provide authToken"
                        },
                    };
                }
            }
        }
        public bool IsValidToken(string authToken)
        {
            //validate Token here  
            return _serviceWrapper.UserAuthInfoService.ValidateCurrentToken(authToken);
        }
        public ApiResourceMappingVM GetApiResourceMapping(string Controller, string ActionMethod)
        {
            var connectionstring = "server=DESKTOP-VBDPK1F\\SQLEXPRESS; database=FactoryManagementDB; Integrated Security=true";
            var optionsBuilder = new DbContextOptionsBuilder<FactoryManagementContext>();
            optionsBuilder.UseSqlServer(connectionstring);


            // FactoryManagementContext dbContext = new FactoryManagementContext(optionsBuilder.Options);

            // Or you can also instantiate inside using

            using FactoryManagementContext dbContext = new(optionsBuilder.Options);
            //...do stuff
            var sss = dbContext.ApiResourceMapping.ToList();
            var apiResource = dbContext
                        .ApiResourceMapping
                        .Where(x => x.Controller == Controller && x.Action == ActionMethod)
                        .ToList();
            ApiResourceMappingVM data = _utilService.Mapper.Map<ApiResourceMapping, ApiResourceMappingVM>(apiResource.FirstOrDefault());
            return data;
            // var context = new FactoryManagementContext();

        }
        public UserRole GetUserRole(string UserId)
        {
            var connectionstring = "server=DESKTOP-VBDPK1F\\SQLEXPRESS; database=FactoryManagementDB; Integrated Security=true";
            var optionsBuilder = new DbContextOptionsBuilder<FactoryManagementContext>();
            optionsBuilder.UseSqlServer(connectionstring);
            using FactoryManagementContext dbContext = new(optionsBuilder.Options);
            var userAuthInfo = dbContext
                .UserAuthInfo
                .Where(x => x.UserId == UserId).FirstOrDefault();

            var userRole = dbContext
                .UserRole
                .Where(x => x.UserId == userAuthInfo.Id)
                .Include(x => x.UserAuthInfo)
                .Include(x => x.Role)
                .ToList().FirstOrDefault();
            return userRole;

        }

    }
}
