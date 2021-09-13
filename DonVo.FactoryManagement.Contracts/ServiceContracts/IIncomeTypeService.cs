using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.IncomeType;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IIncomeTypeService
    {
        Task<WrapperIncomeTypeListVM> GetListPaged(GetDataListVM dataListVM);
        Task<WrapperIncomeTypeListVM> Add(IncomeTypeVM vm);
        Task<WrapperIncomeTypeListVM> Update(string id, IncomeTypeVM vm);
        Task<WrapperIncomeTypeListVM> Delete(IncomeTypeVM itemTemp);
    }
}
