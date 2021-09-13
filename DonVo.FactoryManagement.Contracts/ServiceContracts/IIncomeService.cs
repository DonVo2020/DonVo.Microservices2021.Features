using DonVo.FactoryManagement.Models.ViewModels.Income;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IIncomeService
    {
        Task<WrapperIncomeListVM> Add(IncomeVM vm);
        Task<WrapperIncomeListVM> Delete(IncomeVM itemTemp);
        Task<WrapperIncomeListVM> GetListPaged(DonVo.FactoryManagement.Models.ViewModels.GetDataListVM dataListVM);
        Task<WrapperIncomeListVM> Update(string id, IncomeVM vm);
    }
}
