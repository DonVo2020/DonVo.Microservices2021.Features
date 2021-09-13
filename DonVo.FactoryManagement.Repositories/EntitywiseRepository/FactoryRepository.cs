using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class FactoryRepository : RepositoryBase<Factory>, IFactoryRepository
    {
        public FactoryRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
