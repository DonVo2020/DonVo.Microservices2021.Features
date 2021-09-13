using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class IncomeTypeRepository : RepositoryBase<IncomeType>, IIncomeTypeRepository
    {
        public IncomeTypeRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
