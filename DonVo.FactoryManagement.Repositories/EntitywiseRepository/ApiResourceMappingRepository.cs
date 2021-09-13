using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class ApiResourceMappingRepository : RepositoryBase<ApiResourceMapping>, IApiResourceMappingRepository
    {
        private readonly FactoryManagementContext _context;
        public ApiResourceMappingRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        {
            _context = context;
        }
    }
}
