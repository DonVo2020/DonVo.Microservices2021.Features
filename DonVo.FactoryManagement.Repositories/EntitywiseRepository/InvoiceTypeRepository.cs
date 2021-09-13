using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class InvoiceTypeRepository : RepositoryBase<InvoiceType>, IInvoiceTypeRepository
    {
        public InvoiceTypeRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
