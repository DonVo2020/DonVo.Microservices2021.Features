using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class StockOutRepository : RepositoryBase<StockOut>, IStockOutRepository
    {
        public StockOutRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
