using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.ExpenseType;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IExpenseTypeService
    {
        Task<WrapperExpenseTypeListVM> GetListPaged(GetDataListVM dataListVM);
        Task<WrapperExpenseTypeListVM> Add(ExpenseTypeVM vm);
        Task<WrapperExpenseTypeListVM> Update(string id, ExpenseTypeVM vm);
        Task<WrapperExpenseTypeListVM> Delete(ExpenseTypeVM itemTemp);
    }
}
