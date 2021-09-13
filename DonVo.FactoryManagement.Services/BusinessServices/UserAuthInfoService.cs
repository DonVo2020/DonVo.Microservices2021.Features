using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Auth;
using DonVo.FactoryManagement.Models.ViewModels.Factory;
using DonVo.FactoryManagement.Models.ViewModels.Role;
using DonVo.FactoryManagement.Models.ViewModels.UserAuthInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class UserAuthInfoService : IUserAuthInfoService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        private readonly IUtilService _utilService;

        public UserAuthInfoService(IRepositoryWrapper repositoryWrapper, IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }

        public async Task<WrapperUserAuthInfoListVM> GetListPaged(GetDataListVM dataListVM)
        {
            System.Linq.Expressions.Expression<Func<UserAuthInfo, bool>> globalFilterExpression = (x) => true;
            if (string.IsNullOrEmpty(dataListVM.GlobalFilter) || string.IsNullOrWhiteSpace(dataListVM.GlobalFilter))
            {
                globalFilterExpression = (x) => true;
            }
            else
            {
                globalFilterExpression = (x) =>
                x.UserName.Contains(dataListVM.GlobalFilter);
            }

            var itemCatagoryList = await _repositoryWrapper.UserAuthInfo
                .FindAll()
                .Where(x => x.FactoryId == dataListVM.FactoryId)
                .Where(globalFilterExpression)
                .OrderByDescending(x => x.UpdatedDateTime)
                .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
                .Take(dataListVM.PageSize)
                .ToListAsync();

            var dataRowCount = await _repositoryWrapper.UserAuthInfo.NumOfRecord();
            List<UserAuthInfoVM> UserAuthInfoVMLists = new();
            UserAuthInfoVMLists = _utilService.GetMapper().Map<List<UserAuthInfo>, List<UserAuthInfoVM>>(itemCatagoryList);
            var wrapper = new WrapperUserAuthInfoListVM()
            {
                ListOfData = UserAuthInfoVMLists,
                TotalRecords = dataRowCount
            };
            this._utilService.LogInfo("Successful In Getting  Item Category");

            return wrapper;
        }
        public async Task<WrapperUserAuthInfoListVM> Add(UserAuthInfoVM vm)
        {
            var entityToAdd = _utilService.GetMapper().Map<UserAuthInfoVM, UserAuthInfo>(vm);
            //string uniqueIdTask =await _repositoryWrapper.UserAuthInfo.GetUniqueId();

            //// Todo  need to aandle unique id from db
            //entityToAdd.UniqueId = uniqueIdTask;
            entityToAdd = _repositoryWrapper.UserAuthInfo.Create(entityToAdd);
            await _repositoryWrapper.UserAuthInfo.SaveChangesAsync();
            this._utilService.LogInfo("Successful In saving  Item Category");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperUserAuthInfoListVM data = await GetListPaged(dataParam);
            return data;
        }
        public async Task<WrapperUserAuthInfoListVM> Update(string id, UserAuthInfoVM vm)
        {
            IEnumerable<UserAuthInfo> ItemDB = await _repositoryWrapper.UserAuthInfo.FindByConditionAsync(x => x.Id == id && x.FactoryId == vm.FactoryId);
            var ItemUpdated = _utilService.GetMapper().Map<UserAuthInfoVM, UserAuthInfo>(vm, ItemDB.ToList().FirstOrDefault());
            _repositoryWrapper.UserAuthInfo.Update(ItemUpdated);
            await _repositoryWrapper.UserAuthInfo.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Updating Item Cateory");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperUserAuthInfoListVM data = await GetListPaged(dataParam);
            return data;
        }
        public async Task<WrapperUserAuthInfoListVM> Delete(UserAuthInfoVM itemTemp)
        {
            IEnumerable<UserAuthInfo> itemTask = await _repositoryWrapper.UserAuthInfo.FindByConditionAsync(x => x.Id == itemTemp.Id && x.FactoryId == itemTemp.FactoryId);
            var item = itemTask.ToList().FirstOrDefault();
            if (item == null)
            {
                return new WrapperUserAuthInfoListVM();
            }
            _repositoryWrapper.UserAuthInfo.Delete(item);
            await _repositoryWrapper.UserAuthInfo.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Deleting Item Cateory");

            var dataParam = new GetDataListVM()
            {
                FactoryId = itemTemp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperUserAuthInfoListVM data = await GetListPaged(dataParam);
            return data;
        }
        public LoginResponseVM IsUserAuthentic(LoginVM loginVM)
        {
            LoginResponseVM response = new();
            List<UserAuthInfo> user = _repositoryWrapper
                                        .UserAuthInfo
                                        .FindByCondition(x => x.UserName == loginVM.UserName && x.Password == loginVM.Password)
                                        .Include(x => x.Factory)
                                        .ToList();

            if (user == null)
            {
                response.ResponseMessage = "No User Exist";
            }
            else if (user.Count == 1)
            {
                List<UserRole> roleList = _repositoryWrapper
                            .UserRole
                            .FindByCondition(x => x.UserId == user.FirstOrDefault().Id)
                            .Include(x => x.Role)
                            .Include(x => x.UserAuthInfo)
                            .ToList();
                response.UserAuthInfoVM = _utilService.Mapper.Map<UserAuthInfo, UserAuthInfoVM>(user.FirstOrDefault());
                response.FactoryVM = _utilService.Mapper.Map<Factory, FactoryVM>(user.FirstOrDefault().Factory);
                response.RoleVM = _utilService.Mapper.Map<Role, RoleVM>(roleList.FirstOrDefault().Role);
                response.LoginSuccess = true;
                response.Leader = false;
                if (response.RoleVM.Name == "SUPER_ADMIN")
                {
                    response.Leader = true;
                }
                response.ResponseMessage = "Login Success";
                response.AuthToken = GetAuthToken(response);
            }
            else if (user.Count > 1)
            {
                response.ResponseMessage = "Multiple User Exist With Same Username";
            }
            return response;
        }

        //  https://www.c-sharpcorner.com/article/asp-net-core-web-api-creating-and-validating-jwt-json-web-token/
        private static string GetAuthToken(LoginResponseVM resp)
        {
            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserName", resp.UserAuthInfoVM.UserName),
                new Claim("UserId", resp.UserAuthInfoVM.UserId),
                new Claim("FactoryName", resp.FactoryVM.Name),
                new Claim("FactoryId", resp.FactoryVM.Id)
            };
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:63048",
                audience: "http://localhost:4200",
                claims: permClaims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
            // return Ok(new { Token = tokenString });
        }
        /// <summary>
        /// https://dotnetcoretutorials.com/2020/01/15/creating-and-validating-jwt-tokens-in-asp-net-core/
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool ValidateCurrentToken(string token)
        {
            var mySecret = "superSecretKey@345";
            // var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));
            var mySecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(mySecret));

            var myIssuer = "http://localhost:63048";
            var myAudience = "http://localhost:4200";

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool IsUserExist(LoginVM loginVM)
        {
            List<UserAuthInfo> lst = _repositoryWrapper
                     .UserAuthInfo
                     .FindAll()
                     .Where(x => x.UserName == loginVM.UserName)
                     .ToList();
            if (lst.FirstOrDefault() == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public JwtSecurityToken DecodeJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            JwtSecurityToken tokenS = handler.ReadToken(token) as JwtSecurityToken;
            return tokenS;
        }
    }
}
