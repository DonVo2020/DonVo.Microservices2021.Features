using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Expense;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class ExpenseService : IExpenseService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        private readonly IUtilService _utilService;
        public ExpenseService(IRepositoryWrapper repositoryWrapper, IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;

            this._utilService = utilService;

        }
        public async Task<WrapperExpenseListVM> GetListPaged(GetDataListVM dataListVM)
        {
            //System.Linq.Expressions.Expression<Func<Expense, bool>> globalFilterExpression = (x) => true;
            //if (string.IsNullOrEmpty(dataListVM.GlobalFilter) || string.IsNullOrWhiteSpace(dataListVM.GlobalFilter))
            //{
            //    globalFilterExpression = (x) => true;
            //}
            //else
            //{
            //    globalFilterExpression = (x) =>
            //    x.Customer.Name.Contains(dataListVM.GlobalFilter);
            //}


            var ExpenseList = await _repositoryWrapper
                .Expense
                .FindAll()
                .Where(x => x.FactoryId == dataListVM.FactoryId)
                .Include(x => x.ExpenseType)
                .Include(x => x.Supplier)
                .Include(x => x.Customer)
                .Include(x => x.Staff)
                //.Where(globalFilterExpression)
                .OrderByDescending(x => x.UpdatedDateTime)
                .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
                .Take(dataListVM.PageSize)
                .ToListAsync();

            //var ExpenseList_1 = await _repositoryWrapper
            //    .Expense
            //    .FindAll()
            //    .Where(x => x.FactoryId == dataListVM.FactoryId)
            //    .Include(x => x.ExpenseType)
            //    .Include(x => x.Staff)
            //    //.Where(globalFilterExpression)
            //    .OrderByDescending(x => x.UpdatedDateTime)
            //    .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
            //    .Take(dataListVM.PageSize)
            //    .ToListAsync();

            //var ExpenseList_2 = await _repositoryWrapper
            //    .Expense
            //    .FindAll()
            //    .Where(x => x.FactoryId == dataListVM.FactoryId)
            //    .Include(x => x.ExpenseType)
            //    .Include(x => x.Customer)
            //    //.Where(globalFilterExpression)
            //    .OrderByDescending(x => x.UpdatedDateTime)
            //    .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
            //    .Take(dataListVM.PageSize)
            //    .ToListAsync();

          //  ExpenseList = _utilService.ConcatList<Expense>(ExpenseList, _utilService.ConcatList<Expense>(ExpenseList_1, ExpenseList_2));


            var dataRowCount = await _repositoryWrapper.Expense.NumOfRecord();
            List<ExpenseVM> ExpenseVMLists = new();
            ExpenseVMLists = _utilService.GetMapper().Map<List<Expense>, List<ExpenseVM>>(ExpenseList);
            var wrapper = new WrapperExpenseListVM()
            {
                ListOfData = ExpenseVMLists,
                TotalRecords = dataRowCount
            };

            return wrapper;
        }

        // Expense
        // Transaction
        // Invoice
        public async Task<WrapperExpenseListVM> Add(ExpenseVM vm)
        {
            var invoiceToAdd = _utilService.GetMapper().Map<ExpenseVM, Invoice>(vm);
            var ExpenseToAdd = _utilService.GetMapper().Map<ExpenseVM, Expense>(vm);
            var transactionToAdd = _utilService.GetMapper().Map<ExpenseVM, TblTransaction>(vm);

            //string uniqueIdTask =await _repositoryWrapper.Expense.GetUniqueId();

            //// Todo  need to aandle unique id from db
            //entityToAdd.UniqueId = uniqueIdTask;
            _repositoryWrapper.Invoice.Create(invoiceToAdd);
            ExpenseToAdd.InvoiceId = invoiceToAdd.Id;
            _repositoryWrapper.Expense.Create(ExpenseToAdd);
            transactionToAdd.InvoiceId = invoiceToAdd.Id;
            _repositoryWrapper.Transaction.Create(transactionToAdd);


            Task<int> t1 = _repositoryWrapper.Expense.SaveChangesAsync();
            Task<int> t2 = _repositoryWrapper.Invoice.SaveChangesAsync();
            Task<int> t3 = _repositoryWrapper.Transaction.SaveChangesAsync();

            await Task.WhenAll(t1, t2, t3);

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperExpenseListVM data = await GetListPaged(dataParam);
            return data;
        }
        // not used
        public async Task<WrapperExpenseListVM> Update(string id, ExpenseVM vm)
        {
            IEnumerable<Expense> ItemDB = await _repositoryWrapper.Expense.FindByConditionAsync(x => x.Id == id && x.FactoryId == vm.FactoryId);
            var ItemUpdated = _utilService.GetMapper().Map<ExpenseVM, Expense>(vm, ItemDB.ToList().FirstOrDefault());
            _repositoryWrapper.Expense.Update(ItemUpdated);
            await _repositoryWrapper.Expense.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Updating Item Cateory");


            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperExpenseListVM data = await GetListPaged(dataParam);
            return data;
        }
        public async Task<WrapperExpenseListVM> Delete(ExpenseVM itemTemp)
        {
            Task<IEnumerable<Expense>> ExpenseTask = _repositoryWrapper.Expense.FindByConditionAsync(x => x.Id == itemTemp.Id && x.FactoryId == itemTemp.FactoryId);
            Task<IEnumerable<TblTransaction>> transactionTask = _repositoryWrapper.Transaction.FindByConditionAsync(x => x.InvoiceId == itemTemp.InvoiceId && x.FactoryId == itemTemp.FactoryId);
            Task<IEnumerable<Invoice>> invoiceTask = _repositoryWrapper.Invoice.FindByConditionAsync(x => x.Id == itemTemp.InvoiceId && x.FactoryId == itemTemp.FactoryId);

            await Task.WhenAll(ExpenseTask, transactionTask, invoiceTask);

            var ExpenseToDelete = ExpenseTask.Result.ToList().FirstOrDefault();
            var transactionToDelete = transactionTask.Result.ToList().FirstOrDefault();
            var invoiceToDelete = invoiceTask.Result.ToList().FirstOrDefault();


            //if (item == null)
            //{
            //    return new WrapperExpenseListVM();
            //}
            _repositoryWrapper.Expense.Delete(ExpenseToDelete);
            _repositoryWrapper.Transaction.Delete(transactionToDelete);
            _repositoryWrapper.Invoice.Delete(invoiceToDelete);


            await _repositoryWrapper.Expense.SaveChangesAsync();
            await _repositoryWrapper.Transaction.SaveChangesAsync();
            await _repositoryWrapper.Invoice.SaveChangesAsync();


            var dataParam = new GetDataListVM()
            {
                FactoryId = itemTemp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperExpenseListVM data = await GetListPaged(dataParam);
            return data;
        }
    }
}
