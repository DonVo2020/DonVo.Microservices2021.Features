using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class PurchaseRepository : RepositoryBase<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
