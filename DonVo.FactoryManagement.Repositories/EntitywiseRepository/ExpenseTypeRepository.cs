using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class ExpenseTypeRepository : RepositoryBase<ExpenseType>, IExpenseTypeRepository
    {
        public ExpenseTypeRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
