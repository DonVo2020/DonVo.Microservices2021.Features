using DonVo.FactoryManagement.CommonUtils.Exception.Sales;
using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.Enums;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Sales;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class SalesService : ISalesService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        private readonly IUtilService _utilService;
        public SalesService(IRepositoryWrapper repositoryWrapper, IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }
        // Invoice
        // Income
        // Receivable
        // Sales
        // Stock
        // StockOut
        // Transaction
        public async Task<WrapperSalesListVM> AddSalesAsync(SalesVM salesVM)
        {
            // Invoice
            Invoice invoiceToAdd = new();
            invoiceToAdd = _utilService.Mapper.Map<SalesVM, Invoice>(salesVM);
            _repositoryWrapper.Invoice.Create(invoiceToAdd);

            // Setting Up Invoice Id
            salesVM.InvoiceId = invoiceToAdd.Id;
            foreach (var item in salesVM.ItemList)
            {
                item.InvoiceId = invoiceToAdd.Id;
            }

            // Income 
            Income incomeToAdd = new();
            incomeToAdd = this._utilService.Mapper.Map<SalesVM, Income>(salesVM);
            _repositoryWrapper.Income.Create(incomeToAdd);

            // Receivable
            Receivable ReceivableToAdd = new();
            ReceivableToAdd = _utilService.Mapper.Map<SalesVM, Receivable>(salesVM);
            _repositoryWrapper.Receivable.Create(ReceivableToAdd);

            // Sales
            List<Sales> listOfSalesToAdd = new();
            listOfSalesToAdd = _utilService.Mapper.Map<List<SalesItemVM>, List<Sales>>(salesVM.ItemList);
            _repositoryWrapper.Sales.CreateAll(listOfSalesToAdd);

            // Stock
            Stock stockToAdd = new();
            IEnumerable<Stock> stockList = await _repositoryWrapper.Stock.FindByConditionAsync(x => x.FactoryId == salesVM.FactoryId);
            for (int i = 0; i < salesVM.ItemList.Count; i++)
            {
                Stock existingStock = stockList.ToList().Where(x => x.ItemId == salesVM.ItemList[i].Item.Id && x.ItemStatusId == salesVM.ItemList[i].ItemStatus.Id).FirstOrDefault();

                if (existingStock == null)
                {
                    var getDatalistVM2 = new GetDataListVM()
                    {
                        FactoryId = salesVM.FactoryId,
                        PageNumber = 1,
                        PageSize = 10
                    };
                    WrapperSalesListVM dataToReturn = await GetAllSalesAsync(getDatalistVM2);
                    dataToReturn.HasMessage = true;
                    dataToReturn.Message = "Stock Is Empty";
                    return dataToReturn;
                    // _utilService.Log("Stock Is Empty. Not Enough Stock available");
                    // throw new StockEmptyException();
                }
                else
                {
                    if (existingStock.Quantity >= salesVM.ItemList[i].Quantity)
                    {
                        existingStock.Quantity -= salesVM.ItemList[i].Quantity;
                        _repositoryWrapper.Stock.Update(existingStock);
                    }
                    else
                    {
                        _utilService.Log("Stock Is Empty");
                        return new WrapperSalesListVM();
                    }
                }
            }

            // StockOut
            List<StockOut> stockOutsToAdd = new();
            stockOutsToAdd = _utilService.Mapper.Map<List<SalesItemVM>, List<StockOut>>(salesVM.ItemList);
            _repositoryWrapper.StockOut.CreateAll(stockOutsToAdd);


            // Transaction
            TblTransaction transactionRecieved = new();
            transactionRecieved = _utilService.Mapper.Map<SalesVM, TblTransaction>(salesVM);
            transactionRecieved.Amount = salesVM.PaidAmount;
            transactionRecieved.PaymentStatus = PAYMENT_STATUS.CASH_RECIEVED.ToString();
            transactionRecieved.TransactionType = TRANSACTION_TYPE.CREDIT.ToString();
            _repositoryWrapper.Transaction.Create(transactionRecieved);


            //TblTransaction transactionReceivable = new TblTransaction();
            //transactionReceivable = _utilService.Mapper.Map<SalesVM, TblTransaction>(salesVM);
            //transactionReceivable.Amount = salesVM.DueAmount;
            //transactionReceivable.PaymentStatus = PAYMENT_STATUS.CASH_Receivable.ToString();
            //transactionReceivable.TransactionType = TRANSACTION_TYPE.NOT_YET_EXECUTED.ToString();
            //transactionReceivable.TransactionId = transactionRecieved.TransactionId;
            //_repositoryWrapper.Transaction.Create(transactionReceivable);

            Task<int> Invoice = _repositoryWrapper.Invoice.SaveChangesAsync();
            Task<int> Income = _repositoryWrapper.Income.SaveChangesAsync();
            Task<int> Receivable = _repositoryWrapper.Receivable.SaveChangesAsync();
            Task<int> Sales = _repositoryWrapper.Sales.SaveChangesAsync();
            Task<int> Stock = _repositoryWrapper.Stock.SaveChangesAsync();
            Task<int> StockOut = _repositoryWrapper.StockOut.SaveChangesAsync();
            Task<int> Transaction = _repositoryWrapper.Transaction.SaveChangesAsync();
            await Task.WhenAll(Invoice, Income, Receivable, Sales, Stock, StockOut, Transaction);

            var getDatalistVM = new GetDataListVM()
            {
                FactoryId = salesVM.FactoryId,
                PageNumber = 1,
                PageSize = 10
            };

            return await GetAllSalesAsync(getDatalistVM);
        }
        public async Task<WrapperSalesListVM> GetAllSalesAsync(GetDataListVM getDataListVM)
        {
            WrapperSalesListVM vm = new();
            List<SalesVM> vmList = new();

            Task<List<Invoice>> invoicesT = _repositoryWrapper
                .Invoice
                .FindAll()
                .Include(x => x.Customer)
                .Include(x => x.InvoiceType)
                .Where(x => x.FactoryId == getDataListVM.FactoryId
                && x.InvoiceType.Name == TypeInvoice.Sales.ToString())
                .Skip((getDataListVM.PageNumber - 1) * (getDataListVM.PageSize))
                .Take(getDataListVM.PageSize)
                .ToListAsync();


            Task<List<Sales>> salesT = _repositoryWrapper
                .Sales
                .FindAll()
                .Where(x => x.FactoryId == getDataListVM.FactoryId)
                .Include(x => x.Item)
                .ThenInclude(s => s.ItemCategory)
                .ToListAsync();

            await Task.WhenAll(invoicesT, salesT);
            List<Sales> dbListSales = salesT.Result.ToList();
            vmList = _utilService.Mapper.Map<List<Invoice>, List<SalesVM>>(invoicesT.Result.ToList());
            List<Sales> temp = new();

            for (int i = 0; i < vmList.Count; i++)
            {
                temp = dbListSales.Where(x => x.InvoiceId == vmList.ElementAt(i).InvoiceId).ToList();
                vmList.ElementAt(i).ItemList = _utilService.Mapper.Map<List<Sales>, List<SalesItemVM>>(temp);
            }
            vm.ListOfData = vmList;
            vm.TotalRecords = invoicesT.Result.ToList().Count;
            return vm;
        }
        // Invoice
        // Income
        // Receivable
        // Sales
        // Stock
        // StockOut
        // Transaction
        public async Task<WrapperSalesListVM> DeleteSalesAsync(SalesVM vm) {
            Task<List<Invoice>> invoiceToDelete = _repositoryWrapper
                                .Invoice
                                .FindAll()
                                .Where(x => x.Id == vm.InvoiceId && x.FactoryId == vm.FactoryId)
                                .ToListAsync();
            Task<List<Income>> incomeToDelete = _repositoryWrapper
                                .Income
                                .FindAll()
                                .Where(x => x.InvoiceId == vm.InvoiceId && x.FactoryId == vm.FactoryId)
                                .ToListAsync();
            Task<List<Receivable>> ReceivableToDelete = _repositoryWrapper
                                .Receivable
                                .FindAll()
                                .Where(x => x.InvoiceId == vm.InvoiceId && x.FactoryId == vm.FactoryId)
                                .ToListAsync();
            Task<List<Sales>> salesToDelete = _repositoryWrapper
                                .Sales
                                .FindAll()
                                .Where(x => x.InvoiceId == vm.InvoiceId && x.FactoryId == vm.FactoryId)
                                .ToListAsync();

            await Task.WhenAll(invoiceToDelete, incomeToDelete, ReceivableToDelete, salesToDelete);
            // Stock
            IEnumerable<Stock> stockList = await _repositoryWrapper
                .Stock
                .FindByConditionAsync(
                x => x.FactoryId == vm.FactoryId);
                //&& salesToDelete
                //.Result
                //.ToList()
                //.FirstOrDefault(d => d.ItemId == x.ItemId) != null);

            for (int i = 0; i < salesToDelete.Result.Count; i++)
            {
                Sales sales = salesToDelete.Result.ElementAt(i);
                Stock existingStock = stockList.Where(x => x.ItemId == sales.ItemId).FirstOrDefault();

                if (existingStock == null)
                {
                   // _utilService.Log("Stock Is Empty. Not Enough Stock available");
                   // throw new StockEmptyException();
                }
                else
                {
                    existingStock.Quantity += sales.Quantity;
                    _repositoryWrapper.Stock.Update(existingStock);
                }
            }

            Task<List<StockOut>> stockOutToDelete = _repositoryWrapper
                                .StockOut
                                .FindAll()
                                .Where(x => x.InvoiceId == vm.InvoiceId && x.FactoryId == vm.FactoryId)
                                .ToListAsync();

            Task<List<TblTransaction>> transactionToDelete = _repositoryWrapper
                                .Transaction
                                .FindAll()
                                .Where(x => x.InvoiceId == vm.InvoiceId && x.FactoryId == vm.FactoryId)
                                .ToListAsync();

            _repositoryWrapper.Invoice.DeleteAll(invoiceToDelete.Result.ToList());
            _repositoryWrapper.Income.DeleteAll(incomeToDelete.Result.ToList());
            _repositoryWrapper.Receivable.DeleteAll(ReceivableToDelete.Result.ToList());
            _repositoryWrapper.Sales.DeleteAll(salesToDelete.Result.ToList());
            _repositoryWrapper.StockOut.DeleteAll(stockOutToDelete.Result.ToList());
            _repositoryWrapper.Transaction.DeleteAll(transactionToDelete.Result.ToList());

            Task<int> Invoice = _repositoryWrapper.Invoice.SaveChangesAsync();
            Task<int> Income = _repositoryWrapper.Income.SaveChangesAsync();
            Task<int> Receivable = _repositoryWrapper.Receivable.SaveChangesAsync();
            Task<int> Sales = _repositoryWrapper.Sales.SaveChangesAsync();
            Task<int> StockOut = _repositoryWrapper.StockOut.SaveChangesAsync();
            Task<int> TblTransaction = _repositoryWrapper.Transaction.SaveChangesAsync();
            Task<int> Stock = _repositoryWrapper.Stock.SaveChangesAsync();

            await Task.WhenAll(Invoice, Income, Receivable, Sales, StockOut, TblTransaction, Stock);

            var getDatalistVM = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10
            };

            return await GetAllSalesAsync(getDatalistVM);
        }
        public async Task<WrapperSalesReturnVM> AddSalesReturn(SalesReturnVM vm)
        {
            // StockIn
            // Invoice
            // Payable
            // Stock
            Invoice invoiceToAdd = new();
            invoiceToAdd = _utilService.Mapper.Map<SalesReturnVM, Invoice>(vm);
            _repositoryWrapper.Invoice.Create(invoiceToAdd);
            vm.InvoiceId = invoiceToAdd.Id;
      
            Stock stockToAdd = new();
            stockToAdd = _utilService.Mapper.Map<SalesReturnVM, Stock>(vm);

            // Stock
            IEnumerable<Stock> stockList = await _repositoryWrapper.Stock.FindByConditionAsync(x => x.FactoryId == vm.FactoryId);
            Stock existingStock = stockList.ToList().Where(x => x.ItemId == vm.ItemId).FirstOrDefault();
            // IF NOT PRESENT ADD
            if (existingStock == null)
            {
                _utilService.Log("Stock Is Empty. Not Enough Stock available");
                throw new StockEmptyException();
                //stockToAdd = _utilService.Mapper.Map<SalesItemVM, Stock>(salesVM.ItemList[i]);
                //_repositoryWrapper.Stock.Create(stockToAdd);
            }
            // IF PRESENT UPDATE
            else
            {
                existingStock.Quantity += vm.Quantity;
                _repositoryWrapper.Stock.Update(existingStock);
            }

            StockIn stockInToAdd = new();
            stockInToAdd = _utilService.Mapper.Map<SalesReturnVM, StockIn>(vm);

            if (!vm.IsFullyPaid)
            {
                Payable payableToAdd = new();
                payableToAdd = _utilService.Mapper.Map<SalesReturnVM, Payable>(vm);
                _repositoryWrapper.Payable.Create(payableToAdd);
            }

            _repositoryWrapper.StockIn.Create(stockInToAdd);

            if (vm.AmountPaid > 0)
            {
                TblTransaction tblTransactionToAdd = new();
                tblTransactionToAdd = _utilService.Mapper.Map<SalesReturnVM, TblTransaction>(vm);
                _repositoryWrapper.Transaction.Create(tblTransactionToAdd);

                Expense expenseToAdd = new();
                expenseToAdd = _utilService.Mapper.Map<SalesReturnVM, Expense>(vm);
                _repositoryWrapper.Expense.Create(expenseToAdd);
            }

            Task<int> invT = _repositoryWrapper.Invoice.SaveChangesAsync();
            Task<int> stockInT = _repositoryWrapper.StockIn.SaveChangesAsync();
            Task<int> stockT = _repositoryWrapper.Stock.SaveChangesAsync();
            Task<int> payableT = _repositoryWrapper.Payable.SaveChangesAsync();
            Task<int> transactionT = _repositoryWrapper.Transaction.SaveChangesAsync();
            Task<int> expenseT = _repositoryWrapper.Expense.SaveChangesAsync();

            await Task.WhenAll(invT, stockInT, stockT, payableT, transactionT, expenseT);

            var data = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageSize = 15,
                PageNumber = 1
            };
            return await GetAllSalesReturn(data);
        }
        public async Task<WrapperSalesReturnVM> GetAllSalesReturn(GetDataListVM getDataListVM)
        {
            // Invoice
            // StockOut
            WrapperSalesReturnVM vm = new();

            List<SalesReturnVM> vmList = new();

            Task<List<Invoice>> invoicesT = _repositoryWrapper
                .Invoice
                .FindAll()
                .Include(x => x.Customer)
                .Include(x => x.InvoiceType)
                .Where(x => x.FactoryId == getDataListVM.FactoryId
                && x.InvoiceType.Name == TypeInvoice.SalesReturn.ToString())
                //.Skip((getDataListVM.PageNumber - 1) * (getDataListVM.PageSize))
                //.Take(getDataListVM.PageSize)
                .ToListAsync();

            Task<List<StockIn>> stockInT = _repositoryWrapper
                .StockIn
                .FindAll()
                .Include(x => x.Item)
                .ThenInclude(x => x.ItemCategory)
                .Include(x => x.ItemStatus)
                .Where(x => x.FactoryId == getDataListVM.FactoryId)
                //.Skip((getDataListVM.PageNumber - 1) * (getDataListVM.PageSize))
                //.Take(getDataListVM.PageSize)
                .ToListAsync();

            Task<List<Payable>> payableT = _repositoryWrapper
                .Payable
                .FindAll()
                .Include(x => x.Customer)
                .Where(x => x.FactoryId == getDataListVM.FactoryId)
                //.Skip((getDataListVM.PageNumber - 1) * (getDataListVM.PageSize))
                //.Take(getDataListVM.PageSize)
                .ToListAsync();

            await Task.WhenAll(invoicesT, stockInT, payableT);
            List<Invoice> invoiceList = invoicesT.Result.ToList();
            List<StockIn> stockInList = stockInT.Result.ToList();
            List<Payable> payableList = payableT.Result.ToList();

            SalesReturnVM vc = new();
            for (int i = 0; i < invoiceList.Count; i++)
            {
                vc = new SalesReturnVM();
                Invoice temp = invoiceList.ElementAt(i);
                vc = _utilService.Mapper.Map<Invoice, SalesReturnVM>(temp, vc);

                var stockIn = stockInList.Where(x => x.InvoiceId ==
                temp.Id).ToList().FirstOrDefault();
                if (stockIn != null) {
                    vc = _utilService.Mapper.Map<StockIn, SalesReturnVM>(stockIn, vc);
                }
                
                var payable = payableList.Where(x => x.InvoiceId == temp.Id).ToList().FirstOrDefault();

                if (payable != null) {
                    vc = _utilService.Mapper.Map<Payable, SalesReturnVM>(payable, vc);
                }
                vmList.Add(vc);
            }

            vm.TotalRecords = vmList.Count;
            vm.ListOfData =
                vmList
                .OrderByDescending(x => x.OccurranceDate)
                .Skip((getDataListVM.PageNumber - 1) * (getDataListVM.PageSize))
                .Take(getDataListVM.PageSize)
                .ToList();

            return vm;
        }
        public async Task<WrapperSalesReturnVM> DeleteSalesReturn(SalesReturnVM vm)
        {
            Task<List<Invoice>> invoiceToDelete = _repositoryWrapper
                    .Invoice
                    .FindAll()
                    .Where(x => x.Id == vm.InvoiceId && x.FactoryId == vm.FactoryId)
                    .ToListAsync();
            Task<List<StockIn>> stockInToDelete = _repositoryWrapper
                    .StockIn
                    .FindAll()
                    .Where(x => x.InvoiceId == vm.InvoiceId && x.FactoryId == vm.FactoryId)
                    .ToListAsync();
            Task<List<Payable>> payableToDelete = _repositoryWrapper
                    .Payable
                    .FindAll()
                    .Where(x => x.InvoiceId == vm.InvoiceId && x.FactoryId == vm.FactoryId)
                    .ToListAsync();

           await  Task.WhenAll(invoiceToDelete, stockInToDelete, payableToDelete);

            // Stock
            IEnumerable<Stock> stockList = await _repositoryWrapper.Stock.FindByConditionAsync(x => x.FactoryId == vm.FactoryId && x.ItemId == vm.ItemId);

            Stock existingStock = stockList.ToList().Where(x => x.ItemId == vm.ItemId).FirstOrDefault();
            // IF NOT PRESENT ADD
            if (existingStock == null)
            {
                _utilService.Log("Stock Is Empty. Not Enough Stock available");
                throw new StockEmptyException();
                //stockToAdd = _utilService.Mapper.Map<SalesItemVM, Stock>(salesVM.ItemList[i]);
                //_repositoryWrapper.Stock.Create(stockToAdd);
            }
            // IF PRESENT UPDATE
            else
            {
                existingStock.Quantity -= vm.Quantity;
                _repositoryWrapper.Stock.Update(existingStock);
            }

            Invoice invDelete = _repositoryWrapper.Invoice.Delete(invoiceToDelete.Result.ToList().FirstOrDefault());
            StockIn StockInDelete = _repositoryWrapper.StockIn.Delete(stockInToDelete.Result.ToList().FirstOrDefault());
            Payable PayableDelete = _repositoryWrapper.Payable.Delete(payableToDelete.Result.ToList().FirstOrDefault());

            Task<int> inv = _repositoryWrapper.Invoice.SaveChangesAsync();
            Task<int> StockIn = _repositoryWrapper.StockIn.SaveChangesAsync();
            Task<int> Stock = _repositoryWrapper.Stock.SaveChangesAsync();
            Task<int> Payable = _repositoryWrapper.Payable.SaveChangesAsync();

            await Task.WhenAll(inv, StockIn, Payable, Stock);

            var data = new GetDataListVM() { 
            FactoryId = vm.FactoryId,
            PageSize = 15,
            PageNumber = 1
         
            };
            return  await GetAllSalesReturn(data);
        }
    }
}
