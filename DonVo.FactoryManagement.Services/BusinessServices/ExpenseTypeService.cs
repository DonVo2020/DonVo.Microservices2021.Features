using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.ExpenseType;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        private readonly IUtilService _utilService;
        public ExpenseTypeService(IRepositoryWrapper repositoryWrapper,IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }

        public async Task<WrapperExpenseTypeListVM> GetListPaged(GetDataListVM dataListVM)
        {
            System.Linq.Expressions.Expression<Func<ExpenseType, bool>> globalFilterExpression = (x) => true;
            if (string.IsNullOrEmpty(dataListVM.GlobalFilter) || string.IsNullOrWhiteSpace(dataListVM.GlobalFilter))
            {
                globalFilterExpression = (x) => true;
            }
            else
            {
                globalFilterExpression = (x) =>
                x.Name.Contains(dataListVM.GlobalFilter);
            }

            var itemCatagoryList = await _repositoryWrapper.ExpenseType
                .FindAll()
                .Where(x => x.FactoryId == dataListVM.FactoryId)
                .Where(globalFilterExpression)
                .OrderByDescending(x => x.UpdatedDateTime)
                .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
                .Take(dataListVM.PageSize)
                .ToListAsync();

            var dataRowCount = await _repositoryWrapper.ExpenseType.NumOfRecord();
            List<ExpenseTypeVM> ExpenseTypeVMLists = new();
            ExpenseTypeVMLists = _utilService.GetMapper().Map<List<ExpenseType>, List<ExpenseTypeVM>>(itemCatagoryList);
            var wrapper = new WrapperExpenseTypeListVM()
            {
                ListOfData = ExpenseTypeVMLists,
                TotalRecords = dataRowCount
            };
            this._utilService.LogInfo("Successful In Getting  Item Category");

            return wrapper;
        }

        public async Task<WrapperExpenseTypeListVM> Add(ExpenseTypeVM vm)
        {
            var entityToAdd = _utilService.GetMapper().Map<ExpenseTypeVM, ExpenseType>(vm);
            //string uniqueIdTask =await _repositoryWrapper.ExpenseType.GetUniqueId();

            //// Todo  need to aandle unique id from db
            //entityToAdd.UniqueId = uniqueIdTask;
            entityToAdd = _repositoryWrapper.ExpenseType.Create(entityToAdd);
            await _repositoryWrapper.ExpenseType.SaveChangesAsync();
            this._utilService.LogInfo("Successful In saving  Item Category");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperExpenseTypeListVM data = await GetListPaged(dataParam);
            return data;
        }

        public async Task<WrapperExpenseTypeListVM> Update(string id, ExpenseTypeVM vm)
        {
            IEnumerable<ExpenseType> ItemDB = await _repositoryWrapper.ExpenseType.FindByConditionAsync(x => x.Id == id && x.FactoryId == vm.FactoryId);
            var ItemUpdated = _utilService.GetMapper().Map<ExpenseTypeVM, ExpenseType>(vm, ItemDB.ToList().FirstOrDefault());
            _repositoryWrapper.ExpenseType.Update(ItemUpdated);
            await _repositoryWrapper.ExpenseType.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Updating Item Cateory");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperExpenseTypeListVM data = await GetListPaged(dataParam);
            return data;
        }

        public async Task<WrapperExpenseTypeListVM> Delete(ExpenseTypeVM itemTemp)
        {
            IEnumerable<ExpenseType> itemTask = await _repositoryWrapper.ExpenseType.FindByConditionAsync(x => x.Id == itemTemp.Id && x.FactoryId == itemTemp.FactoryId);
            var item = itemTask.ToList().FirstOrDefault();
            if (item == null)
            {
                return new WrapperExpenseTypeListVM();
            }
            _repositoryWrapper.ExpenseType.Delete(item);
            await _repositoryWrapper.ExpenseType.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Deleting Item Cateory");

            var dataParam = new GetDataListVM()
            {
                FactoryId = itemTemp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperExpenseTypeListVM data = await GetListPaged(dataParam);
            return data;
        }
    }
}
