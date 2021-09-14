using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Income;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class IncomeService : IIncomeService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUtilService _utilService;

        public IncomeService(IRepositoryWrapper repositoryWrapper, IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }

        public async Task<WrapperIncomeListVM> GetListPaged(GetDataListVM dataListVM)
        {
            //System.Linq.Expressions.Expression<Func<Income, bool>> globalFilterExpression = (x) => true;
            //if (string.IsNullOrEmpty(dataListVM.GlobalFilter) || string.IsNullOrWhiteSpace(dataListVM.GlobalFilter))
            //{
            //    globalFilterExpression = (x) => true;
            //}
            //else
            //{
            //    globalFilterExpression = (x) =>
            //    x.Customer.Name.Contains(dataListVM.GlobalFilter);
            //}

            var incomeList = await _repositoryWrapper
                .Income
                .FindAll()
                .Where(x => x.FactoryId == dataListVM.FactoryId)
                 .Include(x => x.Supplier)
                .Include(x => x.Customer)
                .Include(x => x.Staff)
                .Include(x => x.IncomeType)
                //.Where(globalFilterExpression)
                .OrderByDescending(x => x.UpdatedDateTime)
                .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
                .Take(dataListVM.PageSize)
                .ToListAsync();
            //var incomeList_1 = await _repositoryWrapper
            //    .Income
            //    .FindAll()
            //    .Where(x => x.FactoryId == dataListVM.FactoryId)
            //    .Include(x => x.Staff)
            //    .Include(x => x.IncomeType)
            //    //.Where(globalFilterExpression)
            //    .OrderByDescending(x => x.UpdatedDateTime)
            //    .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
            //    .Take(dataListVM.PageSize)
            //    .ToListAsync();

            //var incomeList_2 = await _repositoryWrapper
            //    .Income
            //    .FindAll()
            //    .Where(x => x.FactoryId == dataListVM.FactoryId)
            //    .Include(x => x.Supplier)
            //    .Include(x => x.IncomeType)
            //    //.Where(globalFilterExpression)
            //    .OrderByDescending(x => x.UpdatedDateTime)
            //    .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
            //    .Take(dataListVM.PageSize)
            //    .ToListAsync();

            //incomeList = _utilService.ConcatList<Income>(incomeList.ToList(), _utilService.ConcatList<Income>(incomeList_1, incomeList_2));

            var dataRowCount = await _repositoryWrapper.Income.NumOfRecord();
            List<IncomeVM> IncomeVMLists = new();
            IncomeVMLists = _utilService.GetMapper().Map<List<Income>, List<IncomeVM>>(incomeList);
            var wrapper = new WrapperIncomeListVM()
            {
                ListOfData = IncomeVMLists,
                TotalRecords = dataRowCount
            };

            return wrapper;
        }

        // Income
        // Transaction
        // Invoice
        public async Task<WrapperIncomeListVM> Add(IncomeVM vm)
        {
            var invoiceToAdd = _utilService.GetMapper().Map<IncomeVM, Invoice>(vm);
            var incomeToAdd = _utilService.GetMapper().Map<IncomeVM, Income>(vm);
            var transactionToAdd = _utilService.GetMapper().Map<IncomeVM, TblTransaction>(vm);

            //string uniqueIdTask =await _repositoryWrapper.Income.GetUniqueId();

            //// Todo  need to aandle unique id from db
            //entityToAdd.UniqueId = uniqueIdTask;
            _repositoryWrapper.Invoice.Create(invoiceToAdd);
            incomeToAdd.InvoiceId = invoiceToAdd.Id;
            _repositoryWrapper.Income.Create(incomeToAdd);
            transactionToAdd.InvoiceId = invoiceToAdd.Id;
            _repositoryWrapper.Transaction.Create(transactionToAdd);

            Task<int> t1 =  _repositoryWrapper.Income.SaveChangesAsync();
            Task<int> t2 = _repositoryWrapper.Invoice.SaveChangesAsync();
            Task<int> t3 = _repositoryWrapper.Transaction.SaveChangesAsync();

            await Task.WhenAll(t1,t2,t3);

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperIncomeListVM data = await GetListPaged(dataParam);
            return data;
        }
    
        // not used
        public async Task<WrapperIncomeListVM> Update(string id, IncomeVM vm)
        {
            IEnumerable<Income> ItemDB = await _repositoryWrapper.Income.FindByConditionAsync(x => x.Id == id && x.FactoryId == vm.FactoryId);
            var ItemUpdated = _utilService.GetMapper().Map<IncomeVM, Income>(vm, ItemDB.ToList().FirstOrDefault());
            _repositoryWrapper.Income.Update(ItemUpdated);
            await _repositoryWrapper.Income.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Updating Item Cateory");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperIncomeListVM data = await GetListPaged(dataParam);
            return data;
        }   
        
        public async Task<WrapperIncomeListVM> Delete(IncomeVM itemTemp)
        {
            Task<IEnumerable<Income>> incomeTask =  _repositoryWrapper.Income.FindByConditionAsync(x => x.Id == itemTemp.Id && x.FactoryId == itemTemp.FactoryId);
            Task<IEnumerable<TblTransaction>> transactionTask = _repositoryWrapper.Transaction.FindByConditionAsync(x => x.InvoiceId == itemTemp.InvoiceId && x.FactoryId == itemTemp.FactoryId);
            Task<IEnumerable<Invoice>> invoiceTask = _repositoryWrapper.Invoice.FindByConditionAsync(x => x.Id == itemTemp.InvoiceId && x.FactoryId == itemTemp.FactoryId);

            await Task.WhenAll(incomeTask, transactionTask, invoiceTask);

            var incomeToDelete = incomeTask.Result.ToList().FirstOrDefault();
            var transactionToDelete = transactionTask.Result.ToList().FirstOrDefault();
            var invoiceToDelete = invoiceTask.Result.ToList().FirstOrDefault();

            //if (item == null)
            //{
            //    return new WrapperIncomeListVM();
            //}
            _repositoryWrapper.Income.Delete(incomeToDelete);
            _repositoryWrapper.Transaction.Delete(transactionToDelete);
            _repositoryWrapper.Invoice.Delete(invoiceToDelete);

            await _repositoryWrapper.Income.SaveChangesAsync();
            await _repositoryWrapper.Transaction.SaveChangesAsync();
            await _repositoryWrapper.Invoice.SaveChangesAsync();

            var dataParam = new GetDataListVM()
            {
                FactoryId = itemTemp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperIncomeListVM data = await GetListPaged(dataParam);
            return data;
        }
    }
}
