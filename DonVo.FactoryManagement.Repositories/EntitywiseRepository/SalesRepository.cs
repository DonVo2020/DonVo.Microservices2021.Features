using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class SalesRepository : RepositoryBase<Sales>, ISalesRepository
    {
        public SalesRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
