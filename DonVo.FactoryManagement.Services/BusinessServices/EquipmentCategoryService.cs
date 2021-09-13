using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.EquipmentCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class EquipmentCategoryService : IEquipmentCategoryService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUtilService _utilService;
        public EquipmentCategoryService(IRepositoryWrapper repositoryWrapper,IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }
        public async Task<WrapperEquipmentCategoryListVM> GetListPaged(GetDataListVM dataListVM)
        {
            System.Linq.Expressions.Expression<Func<EquipmentCategory, bool>> globalFilterExpression = (x) => true;
            if (string.IsNullOrEmpty(dataListVM.GlobalFilter) || string.IsNullOrWhiteSpace(dataListVM.GlobalFilter))
            {
                globalFilterExpression = (x) => true;
            }
            else
            {
                globalFilterExpression = (x) =>
                x.Name.Contains(dataListVM.GlobalFilter);
            }


            var itemCatagoryList = await _repositoryWrapper.EquipmentCategory
                .FindAll()
                .Where(x => x.FactoryId == dataListVM.FactoryId)
                .Where(globalFilterExpression)
                .OrderByDescending(x => x.UpdatedDateTime)
                .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
                .Take(dataListVM.PageSize)
                .ToListAsync();

            var dataRowCount = await _repositoryWrapper.EquipmentCategory.NumOfRecord();
            List<EquipmentCategoryVM> EquipmentCategoryVMLists = new();
            EquipmentCategoryVMLists = _utilService.GetMapper().Map<List<EquipmentCategory>, List<EquipmentCategoryVM>>(itemCatagoryList);
            var wrapper = new WrapperEquipmentCategoryListVM()
            {
                ListOfData = EquipmentCategoryVMLists,
                TotalRecords = dataRowCount
            };
            this._utilService.LogInfo("Successful In Getting  Item Category");

            return wrapper;
        }
        public async Task<WrapperEquipmentCategoryListVM> Add(EquipmentCategoryVM vm)
        {
            var entityToAdd = _utilService.GetMapper().Map<EquipmentCategoryVM, EquipmentCategory>(vm);
            //string uniqueIdTask =await _repositoryWrapper.EquipmentCategory.GetUniqueId();

            //// Todo  need to aandle unique id from db
            //entityToAdd.UniqueId = uniqueIdTask;
            entityToAdd = _repositoryWrapper.EquipmentCategory.Create(entityToAdd);
            await _repositoryWrapper.EquipmentCategory.SaveChangesAsync();
            this._utilService.LogInfo("Successful In saving  Item Category");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperEquipmentCategoryListVM data = await GetListPaged(dataParam);
            return data;
        }
        public async Task<WrapperEquipmentCategoryListVM> Update(string id, EquipmentCategoryVM vm)
        {
            IEnumerable<EquipmentCategory> ItemDB = await _repositoryWrapper.EquipmentCategory.FindByConditionAsync(x => x.Id == id && x.FactoryId == vm.FactoryId);
            var ItemUpdated = _utilService.GetMapper().Map<EquipmentCategoryVM, EquipmentCategory>(vm, ItemDB.ToList().FirstOrDefault());
            _repositoryWrapper.EquipmentCategory.Update(ItemUpdated);
            await _repositoryWrapper.EquipmentCategory.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Updating Item Cateory");


            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperEquipmentCategoryListVM data = await GetListPaged(dataParam);
            return data;
        }
        public async Task<WrapperEquipmentCategoryListVM> Delete(EquipmentCategoryVM itemTemp)
        {
            IEnumerable<EquipmentCategory> itemTask = await _repositoryWrapper.EquipmentCategory.FindByConditionAsync(x => x.Id == itemTemp.Id && x.FactoryId == itemTemp.FactoryId);
            var item = itemTask.ToList().FirstOrDefault();
            if (item == null)
            {
                return new WrapperEquipmentCategoryListVM();
            }
            _repositoryWrapper.EquipmentCategory.Delete(item);
            await _repositoryWrapper.EquipmentCategory.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Deleting Item Cateory");

            var dataParam = new GetDataListVM()
            {
                FactoryId = itemTemp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperEquipmentCategoryListVM data = await GetListPaged(dataParam);
            return data;
        }













    }
}
