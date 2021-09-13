﻿using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Models.DbModels;

namespace DonVo.FactoryManagement.Repositories.EntitywiseRepository
{
    public class PaymentStatusRepository : RepositoryBase<PaymentStatus>, IPaymentStatusRepository
    {
        public PaymentStatusRepository(FactoryManagementContext context, IUtilService util) : base(context, util)
        { }
    }
}
