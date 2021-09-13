using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Production;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IProductionService
    {
         Task<WrapperProductionListVM> GetListPaged(GetDataListVM dataListVM);
         Task<WrapperProductionListVM> Add(AddProductionVM vm);
         Task<WrapperProductionListVM> Update(string id, EditProductionVM vm);
         Task<WrapperProductionListVM> Delete(AddProductionVM itemTemp);
        Task<WrapperProductionListVM> AddProductions(List<AddProductionVM> vmList);
    }
}
