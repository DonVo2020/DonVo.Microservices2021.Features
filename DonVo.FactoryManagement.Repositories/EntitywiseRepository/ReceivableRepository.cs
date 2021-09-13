using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class ReceivableRepository : RepositoryBase<Receivable>, IReceivableRepository
    {
        public ReceivableRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
