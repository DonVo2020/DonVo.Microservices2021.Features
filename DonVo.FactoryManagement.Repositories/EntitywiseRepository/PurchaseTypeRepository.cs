using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class PurchaseTypeRepository : RepositoryBase<PurchaseType>, IPurchaseTypeRepository
    {
        public PurchaseTypeRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
