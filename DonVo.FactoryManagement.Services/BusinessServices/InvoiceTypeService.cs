using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.InvoiceType;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class InvoiceTypeService : IInvoiceTypeService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUtilService _utilService;

        public InvoiceTypeService(IRepositoryWrapper repositoryWrapper, IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }

        public async Task<WrapperInvoiceTypeListVM> GetListPaged(GetDataListVM dataListVM)
        {
            System.Linq.Expressions.Expression<Func<InvoiceType, bool>> globalFilterExpression = (x) => true;
            if (string.IsNullOrEmpty(dataListVM.GlobalFilter) || string.IsNullOrWhiteSpace(dataListVM.GlobalFilter))
            {
                globalFilterExpression = (x) => true;
            }
            else
            {
                globalFilterExpression = (x) =>
                x.Name.Contains(dataListVM.GlobalFilter);
            }

            var itemCatagoryList = await _repositoryWrapper.InvoiceType
                .FindAll()
                .Where(x => x.FactoryId == dataListVM.FactoryId)
                .Where(globalFilterExpression)
                .OrderByDescending(x => x.UpdatedDateTime)
                .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
                .Take(dataListVM.PageSize)
                .ToListAsync();

            var dataRowCount = await _repositoryWrapper.InvoiceType.NumOfRecord();
            List<InvoiceTypeVM> InvoiceTypeVMLists = new();
            InvoiceTypeVMLists = _utilService.GetMapper().Map<List<InvoiceType>, List<InvoiceTypeVM>>(itemCatagoryList);
            var wrapper = new WrapperInvoiceTypeListVM()
            {
                ListOfData = InvoiceTypeVMLists,
                TotalRecords = dataRowCount
            };
            this._utilService.LogInfo("Successful In Getting  Item Category");

            return wrapper;
        }

        public async Task<WrapperInvoiceTypeListVM> Add(InvoiceTypeVM vm)
        {
            var entityToAdd = _utilService.GetMapper().Map<InvoiceTypeVM, InvoiceType>(vm);
            //string uniqueIdTask =await _repositoryWrapper.InvoiceType.GetUniqueId();

            //// Todo  need to aandle unique id from db
            //entityToAdd.UniqueId = uniqueIdTask;
            entityToAdd = _repositoryWrapper.InvoiceType.Create(entityToAdd);
            await _repositoryWrapper.InvoiceType.SaveChangesAsync();
            this._utilService.LogInfo("Successful In saving  Item Category");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperInvoiceTypeListVM data = await GetListPaged(dataParam);
            return data;
        }
        public async Task<WrapperInvoiceTypeListVM> Update(string id, InvoiceTypeVM vm)
        {
            IEnumerable<InvoiceType> ItemDB = await _repositoryWrapper.InvoiceType.FindByConditionAsync(x => x.Id == id && x.FactoryId == vm.FactoryId);
            var ItemUpdated = _utilService.GetMapper().Map<InvoiceTypeVM, InvoiceType>(vm, ItemDB.ToList().FirstOrDefault());
            _repositoryWrapper.InvoiceType.Update(ItemUpdated);
            await _repositoryWrapper.InvoiceType.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Updating Item Cateory");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperInvoiceTypeListVM data = await GetListPaged(dataParam);
            return data;
        }

        public async Task<WrapperInvoiceTypeListVM> Delete(InvoiceTypeVM itemTemp)
        {
            IEnumerable<InvoiceType> itemTask = await _repositoryWrapper.InvoiceType.FindByConditionAsync(x => x.Id == itemTemp.Id && x.FactoryId == itemTemp.FactoryId);
            var item = itemTask.ToList().FirstOrDefault();
            if (item == null)
            {
                return new WrapperInvoiceTypeListVM();
            }
            _repositoryWrapper.InvoiceType.Delete(item);
            await _repositoryWrapper.InvoiceType.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Deleting Item Cateory");

            var dataParam = new GetDataListVM()
            {
                FactoryId = itemTemp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperInvoiceTypeListVM data = await GetListPaged(dataParam);
            return data;
        }
    }
}
