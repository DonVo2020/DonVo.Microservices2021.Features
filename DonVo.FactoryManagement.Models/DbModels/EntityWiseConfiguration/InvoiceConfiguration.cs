using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DonVo.FactoryManagement.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DonVo.FactoryManagement.Models.DbModels.EntityWiseConfiguration
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
          
                builder.Property(e => e.Id).HasMaxLength(50);

                //builder.Property(e => e.AmountAfterDiscount)
                //    .IsRequired()
                //    .HasMaxLength(50);

                //builder.Property(e => e.AmountBeforeDiscount)
                //    .IsRequired()
                //    .HasMaxLength(50);

                builder.Property(e => e.ClientId).HasMaxLength(50);

                builder.Property(e => e.Comment).HasMaxLength(50);

                builder.Property(e => e.DeliveryAddress).HasMaxLength(50);

                builder.Property(e => e.DeliveryCost).HasColumnType("decimal(18, 0)");

                builder.Property(e => e.Discount).HasColumnType("decimal(18, 0)");

                builder.Property(e => e.Due).HasColumnType("decimal(18, 0)");

                builder.Property(e => e.FactoryId)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(e => e.InvoiceId)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(e => e.InvoiceTypeId).HasMaxLength(50);

                builder.Property(e => e.OtherCost).HasColumnType("decimal(18, 0)");

                builder.Property(e => e.RowStatus)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(p => p.UniqueId)
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            //builder.Property(e => e.UniqueId)
            //    .IsRequired();
            builder.HasQueryFilter(s => s.RowStatus != DB_ROW_STATUS.DELETED.ToString());
        }
    }
}
