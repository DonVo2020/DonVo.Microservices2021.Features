namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IExpenseService
    {
        System.Threading.Tasks.Task<DonVo.FactoryManagement.Models.ViewModels.Expense.WrapperExpenseListVM> Add(DonVo.FactoryManagement.Models.ViewModels.Expense.ExpenseVM vm);
        System.Threading.Tasks.Task<DonVo.FactoryManagement.Models.ViewModels.Expense.WrapperExpenseListVM> Delete(DonVo.FactoryManagement.Models.ViewModels.Expense.ExpenseVM itemTemp);
        System.Threading.Tasks.Task<DonVo.FactoryManagement.Models.ViewModels.Expense.WrapperExpenseListVM> GetListPaged(DonVo.FactoryManagement.Models.ViewModels.GetDataListVM dataListVM);
        System.Threading.Tasks.Task<DonVo.FactoryManagement.Models.ViewModels.Expense.WrapperExpenseListVM> Update(string id, DonVo.FactoryManagement.Models.ViewModels.Expense.ExpenseVM vm);
    }
}
