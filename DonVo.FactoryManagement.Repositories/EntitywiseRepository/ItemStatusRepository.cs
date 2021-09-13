using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class ItemStatusRepository : RepositoryBase<ItemStatus>, IItemStatusRepository
    {
        public ItemStatusRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
