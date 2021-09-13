using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class StockInRepository : RepositoryBase<StockIn>, IStockInRepository
    {
        public StockInRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
