using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DonVo.FactoryManagement.Models.Configuration;
using DonVo.FactoryManagement.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DonVo.FactoryManagement.Models.DbModels.EntityWiseConfiguration
{
    public class PaymentStatusConfiguration : IEntityTypeConfiguration<PaymentStatus>
    {
        public void Configure(EntityTypeBuilder<PaymentStatus> builder)
        {

            builder.Property(e => e.Id).HasMaxLength(50);

            builder.Property(e => e.FactoryId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.RowStatus)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.UniqueId)
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(e => e.Status).HasMaxLength(50);

            //builder.Property(e => e.UniqueId)
            //    .IsRequired();
            builder.HasQueryFilter(s => s.RowStatus != DB_ROW_STATUS.DELETED.ToString());


            builder.HasData(
                new PaymentStatus
                {
                    CreatedDateTime = DateTime.UtcNow,
                    FactoryId = Config.FactoryId,
                    Id = Guid.NewGuid().ToString(),
                    RowStatus = DB_ROW_STATUS.ADDED.ToString(),
                    Status = "CASH_RECIEVED",
                    UpdatedDateTime = DateTime.Now
                },
                                new PaymentStatus
                                {
                                    CreatedDateTime = DateTime.UtcNow,
                                    FactoryId = Config.FactoryId,
                                    Id = Guid.NewGuid().ToString(),
                                    RowStatus = DB_ROW_STATUS.ADDED.ToString(),
                                    Status = "CASH_PAYABLE",
                                    UpdatedDateTime = DateTime.Now
                                },

                                new PaymentStatus
                                {
                                    CreatedDateTime = DateTime.UtcNow,
                                    FactoryId = Config.FactoryId,
                                    Id = Guid.NewGuid().ToString(),
                                    RowStatus = DB_ROW_STATUS.ADDED.ToString(),
                                    Status = "CASH_Receivable",
                                    UpdatedDateTime = DateTime.Now
                                },
                                new PaymentStatus
                                {
                                    CreatedDateTime = DateTime.UtcNow,
                                    FactoryId = Config.FactoryId,
                                    Id = Guid.NewGuid().ToString(),
                                    RowStatus = DB_ROW_STATUS.ADDED.ToString(),
                                    Status = "CASH_PAID",
                                    UpdatedDateTime = DateTime.Now
                                }


                );



        }
    }
}
