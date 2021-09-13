using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        private readonly FactoryManagementContext _context;
        public CustomerRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        {
            _context = context;
        }
    }
}
