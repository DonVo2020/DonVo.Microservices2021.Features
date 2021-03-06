using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class StockRepository : RepositoryBase<Stock>, IStockRepository
    {
        public StockRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
