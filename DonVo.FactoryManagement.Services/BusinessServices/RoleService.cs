using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Role;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class RoleService : IRoleService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUtilService _utilService;

        public RoleService(IRepositoryWrapper repositoryWrapper, IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }

        // ALTER TABLE Role ADD column_name bit;

        public async Task<WrapperRoleListVM> GetListPaged(GetDataListVM dataListVM)
        {
            //System.Linq.Expressions.Expression<Func<Role, bool>> globalFilterExpression = (x) => true;
            //if (string.IsNullOrEmpty(dataListVM.GlobalFilter) || string.IsNullOrWhiteSpace(dataListVM.GlobalFilter))
            //{
            //    globalFilterExpression = (x) => true;
            //}
            //else
            //{
            //    globalFilterExpression = (x) =>
            //    x.Name.Contains(dataListVM.GlobalFilter);
            //}

            var itemCatagoryList = await _repositoryWrapper
                .Role
                .FindAll()
                // .Where(x => x.FactoryId == dataListVM.FactoryId)
                // .Where(globalFilterExpression)
                //.OrderByDescending(x => x.UpdatedDateTime)
                //.Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
                //.Take(dataListVM.PageSize)
                .ToListAsync();

            var dataRowCount = await _repositoryWrapper.Role.NumOfRecord();
            List<RoleVM> RoleVMLists = new();
            RoleVMLists = _utilService.GetMapper().Map<List<Role>, List<RoleVM>>(itemCatagoryList);
            var wrapper = new WrapperRoleListVM()
            {
                ListOfData = RoleVMLists,
                TotalRecords = dataRowCount
            };
            this._utilService.LogInfo("Successful In Getting  Item Category");

            return wrapper;
        }

        public async Task<WrapperRoleListVM> Add(RoleVM vm)
        {
            var entityToAdd = _utilService.GetMapper().Map<RoleVM, Role>(vm);
            //string uniqueIdTask =await _repositoryWrapper.Role.GetUniqueId();

            //// Todo  need to aandle unique id from db
            //entityToAdd.UniqueId = uniqueIdTask;
            entityToAdd = _repositoryWrapper.Role.Create(entityToAdd);
            await _repositoryWrapper.Role.SaveChangesAsync();
            this._utilService.LogInfo("Successful In saving  Item Category");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperRoleListVM data = await GetListPaged(dataParam);
            return data;
        }

        public async Task<WrapperRoleListVM> Update(string id, RoleVM vm)
        {
            IEnumerable<Role> ItemDB = await _repositoryWrapper.Role.FindByConditionAsync(x => x.Id == id && x.FactoryId == vm.FactoryId);
            var ItemUpdated = _utilService.GetMapper().Map<RoleVM, Role>(vm, ItemDB.ToList().FirstOrDefault());
            _repositoryWrapper.Role.Update(ItemUpdated);
            await _repositoryWrapper.Role.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Updating Item Cateory");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperRoleListVM data = await GetListPaged(dataParam);
            return data;
        }

        public async Task<WrapperRoleListVM> Delete(RoleVM itemTemp)
        {
            IEnumerable<Role> itemTask = await _repositoryWrapper.Role.FindByConditionAsync(x => x.Id == itemTemp.Id && x.FactoryId == itemTemp.FactoryId);
            var item = itemTask.ToList().FirstOrDefault();
            if (item == null)
            {
                return new WrapperRoleListVM();
            }
            _repositoryWrapper.Role.Delete(item);
            await _repositoryWrapper.Role.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Deleting Item Cateory");

            var dataParam = new GetDataListVM()
            {
                FactoryId = itemTemp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperRoleListVM data = await GetListPaged(dataParam);
            return data;
        }
    }
}
