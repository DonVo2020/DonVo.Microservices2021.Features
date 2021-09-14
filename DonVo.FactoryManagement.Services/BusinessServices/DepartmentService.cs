using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Department;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUtilService _utilService;

        public DepartmentService(IRepositoryWrapper repositoryWrapper,IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }

        public async Task<WrapperDepartmentListVM> GetListPaged(GetDataListVM dataListVM)
        {
            System.Linq.Expressions.Expression<Func<Department, bool>> globalFilterExpression = (x) => true;
            if (string.IsNullOrEmpty(dataListVM.GlobalFilter) || string.IsNullOrWhiteSpace(dataListVM.GlobalFilter))
            {
                globalFilterExpression = (x) => true;
            }
            else
            {
                globalFilterExpression = (x) =>
                x.Name.Contains(dataListVM.GlobalFilter);
            }

            var itemCatagoryList = await _repositoryWrapper.Department
                .FindAll()
                .Where(x => x.FactoryId == dataListVM.FactoryId)
                .Where(globalFilterExpression)
                .OrderByDescending(x => x.UpdatedDateTime)
                .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
                .Take(dataListVM.PageSize)
                .ToListAsync();

            var dataRowCount = await _repositoryWrapper.Department.NumOfRecord();
            List<DepartmentVM> DepartmentVMLists = new();
            DepartmentVMLists = _utilService.GetMapper().Map<List<Department>, List<DepartmentVM>>(itemCatagoryList);

            var wrapper = new WrapperDepartmentListVM()
            {
                ListOfData = DepartmentVMLists,
                TotalRecords = dataRowCount
            };

            this._utilService.LogInfo("Successful In Getting  Item Category");

            return wrapper;
        }

        public async Task<WrapperDepartmentListVM> Add(DepartmentVM vm)
        {
            var entityToAdd = _utilService.GetMapper().Map<DepartmentVM, Department>(vm);
            //string uniqueIdTask =await _repositoryWrapper.Department.GetUniqueId();

            //// Todo  need to aandle unique id from db
            //entityToAdd.UniqueId = uniqueIdTask;
            entityToAdd = _repositoryWrapper.Department.Create(entityToAdd);
            await _repositoryWrapper.Department.SaveChangesAsync();
            this._utilService.LogInfo("Successful In saving  Item Category");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperDepartmentListVM data = await GetListPaged(dataParam);
            return data;
        }

        public async Task<WrapperDepartmentListVM> Update(string id, DepartmentVM vm)
        {
            IEnumerable<Department> ItemDB = await _repositoryWrapper.Department.FindByConditionAsync(x => x.Id == id && x.FactoryId == vm.FactoryId);
            var ItemUpdated = _utilService.GetMapper().Map<DepartmentVM, Department>(vm, ItemDB.ToList().FirstOrDefault());
            _repositoryWrapper.Department.Update(ItemUpdated);
            await _repositoryWrapper.Department.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Updating Item Cateory");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperDepartmentListVM data = await GetListPaged(dataParam);
            return data;
        }

        public async Task<WrapperDepartmentListVM> Delete(DepartmentVM itemTemp)
        {
            IEnumerable<Department> itemTask = await _repositoryWrapper.Department.FindByConditionAsync(x => x.Id == itemTemp.Id && x.FactoryId == itemTemp.FactoryId);
            var item = itemTask.ToList().FirstOrDefault();
            if (item == null)
            {
                return new WrapperDepartmentListVM();
            }
            _repositoryWrapper.Department.Delete(item);
            await _repositoryWrapper.Department.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Deleting Item Cateory");

            var dataParam = new GetDataListVM()
            {
                FactoryId = itemTemp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperDepartmentListVM data = await GetListPaged(dataParam);
            return data;
        }
    }
}
