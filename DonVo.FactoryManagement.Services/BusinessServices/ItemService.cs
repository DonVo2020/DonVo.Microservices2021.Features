using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Item;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class ItemService : IItemService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUtilService _utilService;

        public ItemService(IRepositoryWrapper repositoryWrapper,  IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }
        public async Task<WrapperItemListVM> GetListPaged(GetDataListVM dataListVM)
        {
            System.Linq.Expressions.Expression<Func<Item, bool>> globalFilterExpression = (x) => true;
            if (string.IsNullOrEmpty(dataListVM.GlobalFilter) || string.IsNullOrWhiteSpace(dataListVM.GlobalFilter))
            {
                globalFilterExpression = (x) => true;
            }
            else {
                globalFilterExpression = (x) =>
                x.Name.Contains(dataListVM.GlobalFilter)
                || x.ItemCategory.Name.Contains(dataListVM.GlobalFilter)
                || x.UnitPrice.ToString().Contains(dataListVM.GlobalFilter);
            }
            
            
            var itemList = await _repositoryWrapper.Item
                .FindAll() 
                .Where(x => x.FactoryId == dataListVM.FactoryId)
                .Include(x => x.ItemCategory)
                .Where(globalFilterExpression)
                .OrderByDescending(x => x.UpdatedDateTime)
                .Skip((dataListVM.PageNumber - 1) * dataListVM.PageSize)
                .Take(dataListVM.PageSize)  
                .ToListAsync();

            var dataRowCount = await _repositoryWrapper.Item.NumOfRecord();
            List<ItemVM> itemVMList = new();
            itemVMList = _utilService.GetMapper().Map<List<Item>, List<ItemVM>>(itemList);
            var wrapper = new WrapperItemListVM()
            {
                ListOfData = itemVMList,
                TotalRecords = dataRowCount
            };
            return wrapper;
        }
        public async Task<WrapperItemListVM> Add(ItemVM vm)
        {
            var entityToAdd = _utilService.GetMapper().Map<ItemVM, Item>(vm);
            //string uniqueIdTask = await _repositoryWrapper.Item.GetUniqueId();
            //entityToAdd.UniqueId = uniqueIdTask;
            entityToAdd = _repositoryWrapper.Item.Create(entityToAdd);
            await _repositoryWrapper.Item.SaveChangesAsync();
            this._utilService.LogInfo("Successful In saving  Item");


            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperItemListVM data = await GetListPaged(dataParam);
            return data;
        }
        public async Task<WrapperItemListVM> Update(string id, ItemVM vm)
        {
            IEnumerable<Item> ItemDB =await _repositoryWrapper.Item.FindByConditionAsync(x => x.Id == id && x.FactoryId == vm.FactoryId);
            var ItemUpdated = _utilService.GetMapper().Map<ItemVM, Item>(vm, ItemDB.ToList().FirstOrDefault());
            _repositoryWrapper.Item.Update(ItemUpdated);
            await _repositoryWrapper.Item.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Updating Item");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperItemListVM data = await GetListPaged(dataParam);
            return data;
        }
        public async Task<WrapperItemListVM> Delete(ItemVM itemTemp)
        {
            var itemTask = await _repositoryWrapper.Item.FindByConditionAsync(x => x.Id == itemTemp.Id && x.FactoryId == itemTemp.FactoryId);
            var item = itemTask.ToList().FirstOrDefault();
            if (item == null)
            {
                return new WrapperItemListVM();
            }
            _repositoryWrapper.Item.Delete(item);
            await _repositoryWrapper.Item.SaveChangesAsync();
            var dataParam = new GetDataListVM()
            {
                FactoryId = itemTemp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperItemListVM data = await GetListPaged(dataParam);
            return data;
        }
    }
}

