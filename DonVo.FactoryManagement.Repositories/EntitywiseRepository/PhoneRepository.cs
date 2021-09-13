using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class PhoneRepository : RepositoryBase<Phone>, IPhoneRepository
    {
        public PhoneRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
