using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.ItemStatus;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class ItemStatusService : IItemStatusService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        private readonly IUtilService _utilService;

        public ItemStatusService(IRepositoryWrapper repositoryWrapper,IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }

        public async Task<WrapperItemStatusListVM> GetListPaged(GetDataListVM dataListVM)
        {
            var itemList = await _repositoryWrapper.ItemStatus
                .FindAll()
                .Where(x => x.FactoryId == dataListVM.FactoryId)
                .OrderByDescending(x => x.UpdatedDateTime)
                .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
                .Take(dataListVM.PageSize)
                .ToListAsync();

            var dataRowCount = await _repositoryWrapper.ItemStatus.NumOfRecord();
            List<ItemStatusVM> itemVMLists = new();
            itemVMLists = this._utilService.Mapper.Map<List<ItemStatus>, List<ItemStatusVM>>(itemList);
            var wrapper = new WrapperItemStatusListVM()
            {
                ListOfData = itemVMLists,
                TotalRecords = dataRowCount
            };
            this._utilService.Log("Successful In Getting  Item Status");
            return wrapper;
        }
        public async Task<WrapperItemStatusListVM> Add(ItemStatusVM vm)
        {
            var entityToAdd = _utilService.GetMapper().Map<ItemStatusVM, ItemStatus>(vm);
            //string uniqueIdTask =await _repositoryWrapper.ItemStatus.GetUniqueId();

            //// Todo  need to aandle unique id from db
            //entityToAdd.UniqueId = uniqueIdTask;
            entityToAdd = _repositoryWrapper.ItemStatus.Create(entityToAdd);
            await _repositoryWrapper.ItemStatus.SaveChangesAsync();
            this._utilService.LogInfo("Successful In saving  Item Category");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperItemStatusListVM data = await GetListPaged(dataParam);
            return data;
        }
        public async Task<WrapperItemStatusListVM> Update(string id, ItemStatusVM vm)
        {
            IEnumerable<ItemStatus> ItemDB = await _repositoryWrapper.ItemStatus.FindByConditionAsync(x => x.Id == id && x.FactoryId == vm.FactoryId);
            var ItemUpdated = _utilService.GetMapper().Map<ItemStatusVM, ItemStatus>(vm, ItemDB.ToList().FirstOrDefault());
            _repositoryWrapper.ItemStatus.Update(ItemUpdated);
            await _repositoryWrapper.ItemStatus.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Updating Item Cateory");


            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperItemStatusListVM data = await GetListPaged(dataParam);
            return data;
        }
        public async Task<WrapperItemStatusListVM> Delete(ItemStatusVM itemTemp)
        {
            IEnumerable<ItemStatus> itemTask = await _repositoryWrapper.ItemStatus.FindByConditionAsync(x => x.Id == itemTemp.Id && x.FactoryId == itemTemp.FactoryId);
            var item = itemTask.ToList().FirstOrDefault();
            if (item == null)
            {
                return new WrapperItemStatusListVM();
            }
            _repositoryWrapper.ItemStatus.Delete(item);
            await _repositoryWrapper.ItemStatus.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Deleting Item Cateory");

            var dataParam = new GetDataListVM()
            {
                FactoryId = itemTemp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperItemStatusListVM data = await GetListPaged(dataParam);
            return data;
        }
    }
}
