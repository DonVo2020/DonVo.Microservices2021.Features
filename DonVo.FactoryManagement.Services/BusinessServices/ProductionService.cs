using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Production;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class ProductionService : IProductionService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUtilService _utilService;

        public ProductionService(IRepositoryWrapper repositoryWrapper, IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }

        public async Task<WrapperProductionListVM> GetListPaged(GetDataListVM dataListVM)
        {
            var dataList = await _repositoryWrapper.Production
                .FindAll()
                .Include(x => x.Item)
                .Include(x => x.ItemCategory)
                .Include(x => x.Equipment)
                .Include(x => x.Staff)
                .Include(x => x.ItemStatus)
                .Where(x => x.FactoryId == dataListVM.FactoryId)
                .ToListAsync();

            var dataRowCount = await _repositoryWrapper.Production.NumOfRecord();
            List<AddProductionVM> ProductionVMLists = new();
            ProductionVMLists = _utilService.GetMapper().Map<List<Production>, List<AddProductionVM>>(dataList);

            if (string.IsNullOrEmpty(dataListVM.GlobalFilter) || string.IsNullOrWhiteSpace(dataListVM.GlobalFilter))
            {
                ProductionVMLists =
                     ProductionVMLists
                    .OrderByDescending(x => x.EntryDate)
                    .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
                    .Take(dataListVM.PageSize)
                    .ToList();
            }
            else
            {
                ProductionVMLists =
                     ProductionVMLists
                    .Where((x) =>
                         //!string.IsNullOrEmpty(x.EquipmentName) && !string.IsNullOrWhiteSpace(x.EquipmentName) && x.EquipmentName.Contains(dataListVM.GlobalFilter)
                         !string.IsNullOrEmpty(x.ItemCategoryName) && !string.IsNullOrWhiteSpace(x.ItemCategoryName) && x.ItemCategoryName.Contains(dataListVM.GlobalFilter)
                        || !string.IsNullOrEmpty(x.ItemName) && !string.IsNullOrWhiteSpace(x.ItemName) && x.ItemName.Contains(dataListVM.GlobalFilter)
                        || !string.IsNullOrEmpty(x.StaffName) && !string.IsNullOrWhiteSpace(x.StaffName) && x.StaffName.Contains(dataListVM.GlobalFilter))
                    .OrderByDescending(x => x.EntryDate)
                    .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
                    .Take(dataListVM.PageSize)
                    .ToList();
            }

            var wrapper = new WrapperProductionListVM()
            {
                ListOfData = ProductionVMLists,
                TotalRecords = dataRowCount
            };
            this._utilService.LogInfo("Successful In Getting  Item Category");

            return wrapper;
        }

        // Stock -- decrease
        // StockIn -- not
        // Payable -- 
        // Production
        public async Task<WrapperProductionListVM> AddProductions(List<AddProductionVM> vmList)
        {
            for (int i = 0; i < vmList.Count; i++)
            {
                await AddSingleProduction(vmList.ElementAt(i));
                Console.WriteLine("dd");
            }

            var dataParam = new GetDataListVM()
            {
                FactoryId = vmList.ElementAt(0).FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperProductionListVM data = await GetListPaged(dataParam);
            return data;
        }

        private async Task AddSingleProduction(AddProductionVM vm)
        {
            var productionToAdd = _utilService.GetMapper().Map<AddProductionVM, Production>(vm);
            var stockInToAdd = _utilService.GetMapper().Map<AddProductionVM, StockIn>(vm);
            var payableToAdd = _utilService.GetMapper().Map<AddProductionVM, Payable>(vm);
            var invoiceToAdd = _utilService.GetMapper().Map<AddProductionVM, Invoice>(vm);
            //string uniqueIdTask =await _repositoryWrapper.Production.GetUniqueId();

            //// Todo  need to aandle unique id from db
            //entityToAdd.UniqueId = uniqueIdTask;
            invoiceToAdd = _repositoryWrapper.Invoice.Create(invoiceToAdd);

            productionToAdd.InvoiceId = invoiceToAdd.Id;
            stockInToAdd.InvoiceId = invoiceToAdd.Id;
            payableToAdd.InvoiceId = invoiceToAdd.Id;

            productionToAdd = _repositoryWrapper.Production.Create(productionToAdd);
            stockInToAdd.ProductionId = productionToAdd.Id;
            payableToAdd.ProductionId = productionToAdd.Id;

            stockInToAdd = _repositoryWrapper.StockIn.Create(stockInToAdd);
            payableToAdd = _repositoryWrapper.Payable.Create(payableToAdd);

            Stock stockToAdd = new();
            IEnumerable<Stock> stockList = await _repositoryWrapper
                .Stock
                .FindByConditionAsync
                (x => x.FactoryId == vm.FactoryId
                && x.ItemId == vm.ItemId
                && x.ItemStatusId == vm.ItemStatusId);

            Stock existingStock = stockList.ToList().FirstOrDefault();

            // IF NOT PRESENT ADD
            if (existingStock == null)
            {
                stockToAdd = _utilService.Mapper.Map<AddProductionVM, Stock>(vm);
                _repositoryWrapper.Stock.Create(stockToAdd);
            }
            // IF PRESENT UPDATE
            else
            {
                existingStock.Quantity += vm.Quantity;
                _repositoryWrapper.Stock.Update(existingStock);
            }

            Task<int> productionToAddT = _repositoryWrapper.Production.SaveChangesAsync();
            Task<int> stockInToAddT = _repositoryWrapper.StockIn.SaveChangesAsync();
            Task<int> payableToAddT = _repositoryWrapper.Payable.SaveChangesAsync();
            Task<int> stockToAddT = _repositoryWrapper.Stock.SaveChangesAsync();
            Task<int> incoiceToAddT = _repositoryWrapper.Invoice.SaveChangesAsync();

            await Task.WhenAll(productionToAddT, stockInToAddT, payableToAddT, stockToAddT, incoiceToAddT);
        }

        public async Task<WrapperProductionListVM> Add(AddProductionVM vm)
        {
            var productionToAdd = _utilService.GetMapper().Map<AddProductionVM, Production>(vm);
            var stockInToAdd = _utilService.GetMapper().Map<AddProductionVM, StockIn>(vm);
            var payableToAdd = _utilService.GetMapper().Map<AddProductionVM, Payable>(vm);
            var invoiceToAdd = _utilService.GetMapper().Map<AddProductionVM, Invoice>(vm);
            //string uniqueIdTask =await _repositoryWrapper.Production.GetUniqueId();

            //// Todo  need to aandle unique id from db
            //entityToAdd.UniqueId = uniqueIdTask;
            invoiceToAdd = _repositoryWrapper.Invoice.Create(invoiceToAdd);

            productionToAdd.InvoiceId = invoiceToAdd.Id;
            stockInToAdd.InvoiceId = invoiceToAdd.Id;
            payableToAdd.InvoiceId = invoiceToAdd.Id;

            productionToAdd = _repositoryWrapper.Production.Create(productionToAdd);
            stockInToAdd.ProductionId = productionToAdd.Id;
            payableToAdd.ProductionId = productionToAdd.Id;

            stockInToAdd = _repositoryWrapper.StockIn.Create(stockInToAdd);
            payableToAdd = _repositoryWrapper.Payable.Create(payableToAdd);

            Stock stockToAdd = new();
            IEnumerable<Stock> stockList = await _repositoryWrapper.Stock.FindByConditionAsync(x => x.FactoryId == vm.FactoryId && x.ItemId == vm.ItemId);

            Stock existingStock = stockList.ToList().FirstOrDefault();

            // IF NOT PRESENT ADD
            if (existingStock == null)
            {
                stockToAdd = _utilService.Mapper.Map<AddProductionVM, Stock>(vm);
                _repositoryWrapper.Stock.Create(stockToAdd);
            }
            // IF PRESENT UPDATE
            else
            {
                existingStock.Quantity += vm.Quantity;
                _repositoryWrapper.Stock.Update(existingStock);
            }

            Task<int> productionToAddT = _repositoryWrapper.Production.SaveChangesAsync();
            Task<int> stockInToAddT = _repositoryWrapper.StockIn.SaveChangesAsync();
            Task<int> payableToAddT = _repositoryWrapper.Payable.SaveChangesAsync();
            Task<int> stockToAddT = _repositoryWrapper.Stock.SaveChangesAsync();
            Task<int> incoiceToAddT = _repositoryWrapper.Invoice.SaveChangesAsync();

            await Task.WhenAll(productionToAddT, stockInToAddT, payableToAddT, stockToAddT, incoiceToAddT);

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperProductionListVM data = await GetListPaged(dataParam);
            return data;
        }

        // not yet used
        public async Task<WrapperProductionListVM> Update(string id, EditProductionVM vm)
        {
            IEnumerable<Production> ItemDB = await _repositoryWrapper.Production.FindByConditionAsync(x => x.Id == id && x.FactoryId == vm.FactoryId);
            var ItemUpdated = _utilService.GetMapper().Map<EditProductionVM, Production>(vm, ItemDB.ToList().FirstOrDefault());
            _repositoryWrapper.Production.Update(ItemUpdated);
            await _repositoryWrapper.Production.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Updating Production");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperProductionListVM data = await GetListPaged(dataParam);
            return data;
        }

        // Stock -- decrease
        // StockIn --  deleted
        // Payable -- deleted
        // Production -- deleted
        public async Task<WrapperProductionListVM> Delete(AddProductionVM itemTemp)
        {
            Task<IEnumerable<Production>> productionT = _repositoryWrapper.Production.FindByConditionAsync(x => x.Id == itemTemp.Id && x.FactoryId == itemTemp.FactoryId);
            Task<IEnumerable<Stock>> stockT = _repositoryWrapper.Stock.FindByConditionAsync(x => x.FactoryId == itemTemp.FactoryId && x.ItemId == itemTemp.ItemId && x.ItemStatusId == itemTemp.ItemStatusId);
            Task<IEnumerable<Payable>> payableT = _repositoryWrapper.Payable.FindByConditionAsync(x => x.FactoryId == itemTemp.FactoryId && x.ClientId == itemTemp.StaffId && x.Amount == itemTemp.TotalAmount && x.ProductionId == itemTemp.Id);
            Task<IEnumerable<StockIn>> stockInT = _repositoryWrapper.StockIn.FindByConditionAsync(x => x.FactoryId == itemTemp.FactoryId && x.SupplierId == itemTemp.StaffId && x.ProductionId == itemTemp.Id);
            Task<IEnumerable<Invoice>> invoiceT = _repositoryWrapper.Invoice.FindByConditionAsync(x => x.FactoryId == itemTemp.FactoryId && x.Id == itemTemp.InvoiceId);

            await Task.WhenAll(productionT, stockT, payableT, stockInT, invoiceT);

            var item = productionT.Result.ToList().FirstOrDefault();
            if (item == null)
            {
                return new WrapperProductionListVM();
            }

            _repositoryWrapper.Production.Delete(item);
            _repositoryWrapper.Payable.Delete(payableT.Result.ToList().FirstOrDefault());
            _repositoryWrapper.StockIn.Delete(stockInT.Result.ToList().FirstOrDefault());
            _repositoryWrapper.Invoice.Delete(invoiceT.Result.ToList().FirstOrDefault());
            Stock existingStock = stockT.Result.ToList().FirstOrDefault();

            if (existingStock == null)
            {
            }
            else
            {
                if (existingStock.Quantity < itemTemp.Quantity)
                {
                    var dataParam1 = new GetDataListVM()
                    {
                        FactoryId = itemTemp.FactoryId,
                        PageNumber = 1,
                        PageSize = 10,
                        TotalRows = 0
                    };
                    WrapperProductionListVM data2 = await GetListPaged(dataParam1);
                    data2.HasMessage = true;
                    data2.Message = "There is not enough item to remove";
                    return data2;
                }
                else
                {
                    existingStock.Quantity -= itemTemp.Quantity;
                    _repositoryWrapper.Stock.Update(existingStock);
                }
            }

            Task<int> productionToAddT = _repositoryWrapper.Production.SaveChangesAsync();
            Task<int> payableToAddT = _repositoryWrapper.Payable.SaveChangesAsync();
            Task<int> stockToAddT = _repositoryWrapper.Stock.SaveChangesAsync();
            Task<int> stockInToAddT = _repositoryWrapper.StockIn.SaveChangesAsync();
            Task<int> invoiceToAdd = _repositoryWrapper.Invoice.SaveChangesAsync();

            await Task.WhenAll(productionToAddT, payableToAddT, stockToAddT, stockInToAddT, invoiceToAdd);

            var dataParam = new GetDataListVM()
            {
                FactoryId = itemTemp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperProductionListVM data = await GetListPaged(dataParam);
            return data;
        }
    }
}
