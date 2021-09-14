using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Equipment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        private readonly IUtilService _utilService;
        public EquipmentService(IRepositoryWrapper repositoryWrapper, IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }
        public async Task<WrapperEquipmentListVM> GetListPaged(GetDataListVM dataListVM)
        {
            System.Linq.Expressions.Expression<Func<Equipment, bool>> globalFilterExpression = (x) => true;
            if (string.IsNullOrEmpty(dataListVM.GlobalFilter) || string.IsNullOrWhiteSpace(dataListVM.GlobalFilter))
            {
                globalFilterExpression = (x) => true;
            }
            else
            {
                globalFilterExpression = (x) =>
                x.Name.Contains(dataListVM.GlobalFilter)
                || x.EquipmentCategory.Name.Contains(dataListVM.GlobalFilter)
                || x.Price.ToString().Contains(dataListVM.GlobalFilter)
                  || x.Description.Contains(dataListVM.GlobalFilter);
            }

            var EquipmentList = await _repositoryWrapper.Equipment
                .FindAll()
                .Where(x => x.FactoryId == dataListVM.FactoryId)
                .Include(x => x.EquipmentCategory)
                .Where(globalFilterExpression)
                .OrderByDescending(x => x.UpdatedDateTime)
                .Skip((dataListVM.PageNumber - 1) * dataListVM.PageSize)
                .Take(dataListVM.PageSize)
                .ToListAsync();

            var dataRowCount = await _repositoryWrapper.Equipment.NumOfRecord();
            List<EquipmentVM> EquipmentVMList = new();
            EquipmentVMList = _utilService.GetMapper().Map<List<Equipment>, List<EquipmentVM>>(EquipmentList);
            var wrapper = new WrapperEquipmentListVM()
            {
                ListOfData = EquipmentVMList,
                TotalRecords = dataRowCount
            };
            return wrapper;
        }

        public async Task<WrapperEquipmentListVM> Add(EquipmentVM vm)
        {
            var entityToAdd = _utilService.GetMapper().Map<EquipmentVM, Equipment>(vm);
            //string uniqueIdTask = await _repositoryWrapper.Equipment.GetUniqueId();
            //entityToAdd.UniqueId = uniqueIdTask;
            entityToAdd = _repositoryWrapper.Equipment.Create(entityToAdd);
            await _repositoryWrapper.Equipment.SaveChangesAsync();
            this._utilService.LogInfo("Successful In saving  Equipment");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperEquipmentListVM data = await GetListPaged(dataParam);
            return data;
        }

        public async Task<WrapperEquipmentListVM> Update(string id, EquipmentVM vm)
        {
            IEnumerable<Equipment> EquipmentDB = await _repositoryWrapper.Equipment.FindByConditionAsync(x => x.Id == id && x.FactoryId == vm.FactoryId);
            var EquipmentUpdated = _utilService.GetMapper().Map<EquipmentVM, Equipment>(vm, EquipmentDB.ToList().FirstOrDefault());
            _repositoryWrapper.Equipment.Update(EquipmentUpdated);
            await _repositoryWrapper.Equipment.SaveChangesAsync();
            this._utilService.LogInfo("Successful In Updating Equipment");

            var dataParam = new GetDataListVM()
            {
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperEquipmentListVM data = await GetListPaged(dataParam);
            return data;
        }

        public async Task<WrapperEquipmentListVM> Delete(EquipmentVM EquipmentTemp)
        {
            var EquipmentTask = await _repositoryWrapper.Equipment.FindByConditionAsync(x => x.Id == EquipmentTemp.Id && x.FactoryId == EquipmentTemp.FactoryId);
            var Equipment = EquipmentTask.ToList().FirstOrDefault();
            if (Equipment == null)
            {
                return new WrapperEquipmentListVM();
            }
            _repositoryWrapper.Equipment.Delete(Equipment);
            await _repositoryWrapper.Equipment.SaveChangesAsync();
            var dataParam = new GetDataListVM()
            {
                FactoryId = EquipmentTemp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperEquipmentListVM data = await GetListPaged(dataParam);
            return data;
        }
    }
}
