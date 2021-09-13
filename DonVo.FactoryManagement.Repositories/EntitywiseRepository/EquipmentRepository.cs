﻿using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class EquipmentRepository : RepositoryBase<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
