using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.PaymentStatus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class PaymentStatusService : IPaymentStatusService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUtilService _utilService;

        public PaymentStatusService(IRepositoryWrapper repositoryWrapper,  IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }

        public async Task<WrapperPaymentStatusListVM> GetListPaged(GetDataListVM dataListVM)
        {
            System.Linq.Expressions.Expression<Func<PaymentStatus, bool>> globalFilterExpression = (x) => true;
            if (string.IsNullOrEmpty(dataListVM.GlobalFilter) || string.IsNullOrWhiteSpace(dataListVM.GlobalFilter))
            {
                globalFilterExpression = (x) => true;
            }
            else
            {
                globalFilterExpression = (x) =>
                x.Status.Contains(dataListVM.GlobalFilter);
            }

            var paymentStatusList = await _repositoryWrapper.PaymentStatus
                .FindAll()
                .Where(x => x.FactoryId == dataListVM.FactoryId)
                .Where(globalFilterExpression)
                .OrderByDescending(x => x.UpdatedDateTime)
                .Skip((dataListVM.PageNumber - 1) * (dataListVM.PageSize))
                .Take(dataListVM.PageSize)
                .ToListAsync();

            var dataRowCount = await _repositoryWrapper.PaymentStatus.NumOfRecord();
            List<PaymentStatusVM> PaymentStatusVMLists = new();
            PaymentStatusVMLists = _utilService.GetMapper().Map<List<PaymentStatus>, List<PaymentStatusVM>>(paymentStatusList);
            var wrapper = new WrapperPaymentStatusListVM()
            {
                ListOfData = PaymentStatusVMLists,
                TotalRecords = dataRowCount
            };
            return wrapper;
        }

        public async Task<WrapperPaymentStatusListVM> Add(PaymentStatusVM vm)
        {
            var entityToAdd = _utilService.GetMapper().Map<PaymentStatusVM, PaymentStatus>(vm);
            //string uniqueIdTask =await _repositoryWrapper.PaymentStatus.GetUniqueId();

            //// Todo  need to aandle unique id from db
            //entityToAdd.UniqueId = uniqueIdTask;
            entityToAdd = _repositoryWrapper.PaymentStatus.Create(entityToAdd);
            await _repositoryWrapper.PaymentStatus.SaveChangesAsync();
            this._utilService.LogInfo("Successful In saving  Item Category");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperPaymentStatusListVM data = await GetListPaged(dataParam);
            return data;
        }

        public async Task<WrapperPaymentStatusListVM> Update(string id, PaymentStatusVM vm)
        {
            IEnumerable<PaymentStatus> ItemDB = await _repositoryWrapper.PaymentStatus.FindByConditionAsync(x => x.Id == id && x.FactoryId == vm.FactoryId);
            var ItemUpdated = _utilService.GetMapper().Map<PaymentStatusVM, PaymentStatus>(vm, ItemDB.ToList().FirstOrDefault());
            _repositoryWrapper.PaymentStatus.Update(ItemUpdated);
            await _repositoryWrapper.PaymentStatus.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Updating Item Cateory");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperPaymentStatusListVM data = await GetListPaged(dataParam);
            return data;
        }

        public async Task<WrapperPaymentStatusListVM> Delete(PaymentStatusVM itemTemp)
        {
            IEnumerable<PaymentStatus> itemTask = await _repositoryWrapper.PaymentStatus.FindByConditionAsync(x => x.Id == itemTemp.Id && x.FactoryId == itemTemp.FactoryId);
            var item = itemTask.ToList().FirstOrDefault();
            if (item == null)
            {
                return new WrapperPaymentStatusListVM();
            }
            _repositoryWrapper.PaymentStatus.Delete(item);
            await _repositoryWrapper.PaymentStatus.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Deleting Item Cateory");

            var dataParam = new GetDataListVM()
            {
                FactoryId = itemTemp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperPaymentStatusListVM data = await GetListPaged(dataParam);
            return data;
        }
    }
}
