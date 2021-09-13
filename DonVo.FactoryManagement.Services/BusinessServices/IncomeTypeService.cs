using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.IncomeType;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class IncomeTypeService : IIncomeTypeService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        private readonly IUtilService _utilService;

        public IncomeTypeService(IRepositoryWrapper repositoryWrapper, IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }
        public async Task<WrapperIncomeTypeListVM> GetListPaged(GetDataListVM dataListVM)
        {
            System.Linq.Expressions.Expression<Func<IncomeType, bool>> globalFilterExpression = (x) => true;
            if (string.IsNullOrEmpty(dataListVM.GlobalFilter) || string.IsNullOrWhiteSpace(dataListVM.GlobalFilter))
            {
                globalFilterExpression = (x) => true;
            }
            else
            {
                globalFilterExpression = (x) =>
                x.Name.Contains(dataListVM.GlobalFilter);
            }


            var itemCatagoryList = await _repositoryWrapper.IncomeType
                .FindAll()
                .Where(x => x.FactoryId == dataListVM.FactoryId)
                .Where(globalFilterExpression)
                .OrderByDescending(x => x.UpdatedDateTime)
                .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
                .Take(dataListVM.PageSize)
                .ToListAsync();

            var dataRowCount = await _repositoryWrapper.IncomeType.NumOfRecord();
            List<IncomeTypeVM> IncomeTypeVMLists = new();
            IncomeTypeVMLists = _utilService.GetMapper().Map<List<IncomeType>, List<IncomeTypeVM>>(itemCatagoryList);
            var wrapper = new WrapperIncomeTypeListVM()
            {
                ListOfData = IncomeTypeVMLists,
                TotalRecords = dataRowCount
            };
            this._utilService.LogInfo("Successful In Getting  Item Category");

            return wrapper;
        }
        public async Task<WrapperIncomeTypeListVM> Add(IncomeTypeVM vm)
        {
            var entityToAdd = _utilService.GetMapper().Map<IncomeTypeVM, IncomeType>(vm);
            //string uniqueIdTask =await _repositoryWrapper.IncomeType.GetUniqueId();

            //// Todo  need to aandle unique id from db
            //entityToAdd.UniqueId = uniqueIdTask;
            entityToAdd = _repositoryWrapper.IncomeType.Create(entityToAdd);
            await _repositoryWrapper.IncomeType.SaveChangesAsync();
            this._utilService.LogInfo("Successful In saving  Item Category");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperIncomeTypeListVM data = await GetListPaged(dataParam);
            return data;
        }
        public async Task<WrapperIncomeTypeListVM> Update(string id, IncomeTypeVM vm)
        {
            IEnumerable<IncomeType> ItemDB = await _repositoryWrapper.IncomeType.FindByConditionAsync(x => x.Id == id && x.FactoryId == vm.FactoryId);
            var ItemUpdated = _utilService.GetMapper().Map<IncomeTypeVM, IncomeType>(vm, ItemDB.ToList().FirstOrDefault());
            _repositoryWrapper.IncomeType.Update(ItemUpdated);
            await _repositoryWrapper.IncomeType.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Updating Item Cateory");


            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperIncomeTypeListVM data = await GetListPaged(dataParam);
            return data;
        }
        public async Task<WrapperIncomeTypeListVM> Delete(IncomeTypeVM itemTemp)
        {
            IEnumerable<IncomeType> itemTask = await _repositoryWrapper.IncomeType.FindByConditionAsync(x => x.Id == itemTemp.Id && x.FactoryId == itemTemp.FactoryId);
            var item = itemTask.ToList().FirstOrDefault();
            if (item == null)
            {
                return new WrapperIncomeTypeListVM();
            }
            _repositoryWrapper.IncomeType.Delete(item);
            await _repositoryWrapper.IncomeType.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Deleting Item Cateory");

            var dataParam = new GetDataListVM()
            {
                FactoryId = itemTemp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperIncomeTypeListVM data = await GetListPaged(dataParam);
            return data;
        }
    }
}
