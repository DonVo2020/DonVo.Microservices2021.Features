using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class PayableRepository : RepositoryBase<Payable>, IPayableRepository
    {
        public PayableRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
