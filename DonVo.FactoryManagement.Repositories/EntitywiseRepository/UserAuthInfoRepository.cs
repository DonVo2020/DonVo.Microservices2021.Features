﻿using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class UserAuthInfoRepository : RepositoryBase<UserAuthInfo>, IUserAuthInfoRepository
    {
        public UserAuthInfoRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
