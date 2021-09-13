using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;
using System;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(FactoryManagementContext context,IUtilService util) : base(context, util) 
        { 
        }
        public void Print()
        {
            throw new NotImplementedException();
        }
    }
}
