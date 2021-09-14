using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Stock;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class StockService : IStockService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUtilService _utilService;

        public StockService(IRepositoryWrapper repositoryWrapper, IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }

        public async Task<WrapperStockListVM> Add(StockVM ViewModel)
        {
            var itemToAdd = _utilService.GetMapper().Map<StockVM, Stock>(ViewModel);
            itemToAdd = _repositoryWrapper.Stock.Create(itemToAdd);
            Task<int> t1 = _repositoryWrapper.Stock.SaveChangesAsync();
            await Task.WhenAll(t1);
            this._utilService.Log("Successful In Adding Data");

            var dataParam = new GetDataListVM()
            {
                FactoryId = itemToAdd.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperStockListVM data = await GetListPaged(dataParam);
            return data;
        }

        public async Task<WrapperStockListVM> Update(string id, StockVM ViewModel)
        {
            if (id != ViewModel.Id)
            {
                _ = new WrapperStockListVM();
            }

            Task<IEnumerable<Stock>> itemsDB = _repositoryWrapper.Stock.FindByConditionAsync(x => x.Id == id && x.FactoryId == ViewModel.FactoryId);
            await Task.WhenAll(itemsDB);

            var itemUpdated = _utilService.GetMapper().Map<StockVM, Stock>(ViewModel, itemsDB.Result.ToList().FirstOrDefault());
            _repositoryWrapper.Stock.Update(itemUpdated);

            Task<int> t1 = _repositoryWrapper.Stock.SaveChangesAsync();
            await Task.WhenAll(t1);
            this._utilService.Log("Successful In Updating Data");

            var dataParam = new GetDataListVM()
            {
                FactoryId = ViewModel.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperStockListVM data = await GetListPaged(dataParam);
            return data;
        }

        public async Task<WrapperStockListVM> GetListPaged(GetDataListVM dataListVM)
        {
            IEnumerable<Stock> ListTask =
                await _repositoryWrapper.Stock
                .FindByCondition(x => x.FactoryId == dataListVM.FactoryId)
                .Include(x => x.Item)
                .Include(x => x.ItemStatus)
                .ToListAsync();
            long noOfRecordTask = await _repositoryWrapper.Stock.NumOfRecord();

            List<Stock> List = ListTask.ToList().OrderByDescending(x => x.UpdatedDateTime).ToList();//.Skip((dataListVM.PageNumber - 1) * dataListVM.PageSize).Take(dataListVM.PageSize).OrderByDescending(x => x.CreatedDateTime).ToList();

            List<StockVM> outputList = new();

            outputList = _utilService.GetMapper().Map<List<Stock>, List<StockVM>>(List);

            if (!string.IsNullOrEmpty(dataListVM.GlobalFilter) && !string.IsNullOrWhiteSpace(dataListVM.GlobalFilter))
            {
                outputList = outputList.Where(output =>
                output.ItemName != null ? output.ItemName.Contains(dataListVM.GlobalFilter, StringComparison.OrdinalIgnoreCase) : false
                             || output.ItemStatus != null ? output.ItemStatus.Contains(dataListVM.GlobalFilter, StringComparison.OrdinalIgnoreCase) : (false
                             || output.Quantity.ToString() != null) && output.Quantity.ToString().Contains(dataListVM.GlobalFilter, StringComparison.OrdinalIgnoreCase))
                                         .ToList();
            }

            outputList = outputList.Skip((dataListVM.PageNumber - 1) * dataListVM.PageSize).Take(dataListVM.PageSize).ToList();
            var data = new WrapperStockListVM
            {
                ListOfData = outputList,
                TotalRecords = noOfRecordTask
            };
            this._utilService.Log("Successful In Getting Data");
            return data;
        }

        public async Task<WrapperStockListVM> Delete(StockVM Temp)
        {
            var Task = await _repositoryWrapper.Stock.FindByConditionAsync(x => x.Id == Temp.Id && x.FactoryId == Temp.FactoryId);
            var datarow = Task.ToList().FirstOrDefault();
            if (datarow == null)
            {
                return new WrapperStockListVM();
            }
            _repositoryWrapper.Stock.Delete(datarow);
            await _repositoryWrapper.Stock.SaveChangesAsync();
            this._utilService.Log("Successful In Deleting Data");
            var dataParam = new GetDataListVM()
            {
                FactoryId = Temp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperStockListVM data = await GetListPaged(dataParam);
            return data;
        }

        public async Task<WrapperStockListVM> ChangeItemStatus(StockVM ViewModel)
        {
            var dataParam6 = new GetDataListVM()
            {
                FactoryId = ViewModel.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            Task<IEnumerable<Stock>> itemsDB = _repositoryWrapper.Stock.FindByConditionAsync(x => x.Id == ViewModel.Id && x.FactoryId == ViewModel.FactoryId);
            Task<WrapperStockListVM> data1 = GetListPaged(dataParam6);
            Task<IEnumerable<Stock>> stockDB = _repositoryWrapper.Stock.FindByConditionAsync(x => x.FactoryId == ViewModel.FactoryId && x.ItemStatusId == ViewModel.ItemStatusId);
            await Task.WhenAll(itemsDB, data1, stockDB);

            Stock stock = itemsDB.Result.ToList().FirstOrDefault();

            if (stock.Quantity < ViewModel.Quantity || ViewModel.ItemStatusId == stock.ItemStatusId)
            {
                return data1.Result;
            }

            stock.Quantity -= ViewModel.Quantity;
            _repositoryWrapper.Stock.Update(stock);

            // Task<int> t3 = _repositoryWrapper.Stock.SaveChangesAsync();
            // await Task.WhenAll(t3);

            await stockDB;

            Stock tempStock = stockDB.Result.ToList().FirstOrDefault();
            if (tempStock == null)
            {
                return await Add(ViewModel);
            }
            else
            {
                tempStock.Quantity += ViewModel.Quantity;
                _repositoryWrapper.Stock.Update(tempStock);
            }

            await _repositoryWrapper.Stock.SaveChangesAsync();

            WrapperStockListVM data2 = await GetListPaged(dataParam6);
            return data2;
            //var itemToAdd = _utilService.GetMapper().Map<StockVM, Stock>(ViewModel);
            //itemToAdd = _repositoryWrapper.Stock.Create(itemToAdd);
            //Task<int> t1 = _repositoryWrapper.Stock.SaveChangesAsync();
            //await Task.WhenAll(t1);
            //this._utilService.Log("Successful In Adding Data");

            //var dataParam = new GetDataListVM()
            //{
            //    FactoryId = itemToAdd.FactoryId,
            //    PageNumber = 1,
            //    PageSize = 10,
            //    TotalRows = 0
            //};
            //WrapperStockListVM data = await GetListPaged(dataParam);
            //return data;
        }
    }
}
