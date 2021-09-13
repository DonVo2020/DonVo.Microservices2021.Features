using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class TransactionRepository : RepositoryBase<TblTransaction>, ITransactionRepository
    {
        public TransactionRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
