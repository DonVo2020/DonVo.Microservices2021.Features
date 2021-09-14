using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Factory;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class FactoryService : IFactoryService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUtilService _utilService;

        public FactoryService(IRepositoryWrapper repositoryWrapper, IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }

        public async Task<WrapperFactoryListVM> GetListPaged(GetDataListVM dataListVM)
        {
            var itemList = await _repositoryWrapper.Factory
                .FindAll()
                //.Where(x => x.FactoryId == dataListVM.FactoryId)
                .OrderByDescending(x => x.UpdatedDateTime)
                .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
                .Take(dataListVM.PageSize)
                .ToListAsync();

            var dataRowCount = await _repositoryWrapper.Factory.NumOfRecord();
            List<FactoryVM> itemVMLists = new();
            itemVMLists = this._utilService.Mapper.Map<List<Factory>, List<FactoryVM>>(itemList);
            var wrapper = new WrapperFactoryListVM()
            {
                ListOfData = itemVMLists,
                TotalRecords = dataRowCount
            };
            return wrapper;
        }

        public async Task<WrapperFactoryListVM> Add(FactoryVM vm)
        {
            var entityToAdd = _utilService.GetMapper().Map<FactoryVM, Factory>(vm);
            entityToAdd = this._repositoryWrapper.Factory.Create(entityToAdd);

            #region Invoice Type
            this._repositoryWrapper.InvoiceType.Create(new InvoiceType()
            {
                FactoryId = entityToAdd.Id,
                Name = "Expense",
            });
            this._repositoryWrapper.InvoiceType.Create(new InvoiceType()
            {
                FactoryId = entityToAdd.Id,
                Name = "Income",
            });
            this._repositoryWrapper.InvoiceType.Create(new InvoiceType()
            {
                FactoryId = entityToAdd.Id,
                Name = "SalesReturn",
            });
            this._repositoryWrapper.InvoiceType.Create(new InvoiceType()
            {
                FactoryId = entityToAdd.Id,
                Name = "PurchaseReturn",
            });
            this._repositoryWrapper.InvoiceType.Create(new InvoiceType()
            {
                FactoryId = entityToAdd.Id,
                Name = "StaffProduction",
            });
            this._repositoryWrapper.InvoiceType.Create(new InvoiceType()
            {
                FactoryId = entityToAdd.Id,
                Name = "StaffPayment",
            });
            this._repositoryWrapper.InvoiceType.Create(new InvoiceType()
            {
                FactoryId = entityToAdd.Id,
                Name = "SupplierPayment",
            });
            this._repositoryWrapper.InvoiceType.Create(new InvoiceType()
            {
                FactoryId = entityToAdd.Id,
                Name = "ClientPayment",
            });
            this._repositoryWrapper.InvoiceType.Create(new InvoiceType()
            {
                FactoryId = entityToAdd.Id,
                Name = "Purchase",
            });
            this._repositoryWrapper.InvoiceType.Create(new InvoiceType()
            {
                FactoryId = entityToAdd.Id,
                Name = "Sales",
            });
            #endregion
            #region Income  Type
            this._repositoryWrapper.IncomeType.Create(new IncomeType()
            {
                FactoryId = entityToAdd.Id,
                Name = "PurchaseReturn"
            });
            this._repositoryWrapper.IncomeType.Create(new IncomeType()
            {
                FactoryId = entityToAdd.Id,
                Name = "ClientPaymentRecieved"
            });
            this._repositoryWrapper.IncomeType.Create(new IncomeType()
            {
                FactoryId = entityToAdd.Id,
                Name = "Sales"
            });
            #endregion
            #region Expense Type
            this._repositoryWrapper.ExpenseType.Create(new ExpenseType()
            {
                FactoryId = entityToAdd.Id,
                Name = "SalesReturn"
            });
            this._repositoryWrapper.ExpenseType.Create(new ExpenseType()
            {
                FactoryId = entityToAdd.Id,
                Name = "StaffPayment"
            });
            this._repositoryWrapper.ExpenseType.Create(new ExpenseType()
            {
                FactoryId = entityToAdd.Id,
                Name = "SupplierPayment"
            });
            this._repositoryWrapper.ExpenseType.Create(new ExpenseType()
            {
                FactoryId = entityToAdd.Id,
                Name = "Purchase"
            });
            #endregion
            #region Payment Status
            this._repositoryWrapper.PaymentStatus.Create(new PaymentStatus()
            {
                FactoryId = entityToAdd.Id,
                Status = "CASH_PAID"
            });
            this._repositoryWrapper.PaymentStatus.Create(new PaymentStatus()
            {
                FactoryId = entityToAdd.Id,
                Status = "CASH_Receivable"
            });
            this._repositoryWrapper.PaymentStatus.Create(new PaymentStatus()
            {
                FactoryId = entityToAdd.Id,
                Status = "CASH_PAYABLE"
            });
            this._repositoryWrapper.PaymentStatus.Create(new PaymentStatus()
            {
                FactoryId = entityToAdd.Id,
                Status = "CASH_RECIEVED"
            });
            #endregion
            #region Item    Status
            this._repositoryWrapper.ItemStatus.Create(new ItemStatus()
            {
                FactoryId = entityToAdd.Id,
                Name = "BAD"
            });
            this._repositoryWrapper.ItemStatus.Create(new ItemStatus()
            {
                FactoryId = entityToAdd.Id,
                Name = "GOOD"
            }); 
            #endregion

            Task<int> t1 =  _repositoryWrapper.Factory.SaveChangesAsync();
            Task<int> t2 =  _repositoryWrapper.InvoiceType.SaveChangesAsync();
            Task<int> t3 =  _repositoryWrapper.ExpenseType.SaveChangesAsync();
            Task<int> t4 =  _repositoryWrapper.PaymentStatus.SaveChangesAsync();
            Task<int> t5 =  _repositoryWrapper.ItemStatus.SaveChangesAsync();
            Task<int> t6 =  _repositoryWrapper.IncomeType.SaveChangesAsync();

            await Task.WhenAll(t1,t2,t3,t4,t5,t6);

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperFactoryListVM data = await GetListPaged(dataParam);
            return data;
        }
        public async Task<WrapperFactoryListVM> Update(string id, FactoryVM vm)
        {
            IEnumerable<Factory> ItemDB = await _repositoryWrapper.Factory.FindByConditionAsync(x => x.Id == id);
            var ItemUpdated = _utilService.GetMapper().Map<FactoryVM, Factory>(vm, ItemDB.ToList().FirstOrDefault());
            _repositoryWrapper.Factory.Update(ItemUpdated);
            await _repositoryWrapper.Factory.SaveChangesAsync();

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperFactoryListVM data = await GetListPaged(dataParam);
            return data;
        }

        public async Task<WrapperFactoryListVM> Delete(FactoryVM itemTemp)
        {
            IEnumerable<Factory> itemTask = await _repositoryWrapper.Factory.FindByConditionAsync(x => x.Id == itemTemp.Id);
            var item = itemTask.ToList().FirstOrDefault();
            if (item == null)
            {
                return new WrapperFactoryListVM();
            }
            _repositoryWrapper.Factory.Delete(item);
            await _repositoryWrapper.Factory.SaveChangesAsync();

            var dataParam = new GetDataListVM()
            {
                FactoryId = itemTemp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperFactoryListVM data = await GetListPaged(dataParam);
            return data;
        }
    }
}
