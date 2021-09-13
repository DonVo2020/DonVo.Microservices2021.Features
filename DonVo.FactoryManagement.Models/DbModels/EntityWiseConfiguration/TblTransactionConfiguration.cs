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
    public class TblTransactionConfiguration : IEntityTypeConfiguration<TblTransaction>
    {
        public void Configure(EntityTypeBuilder<TblTransaction> builder)
        {
          
                builder.Property(e => e.Id).HasMaxLength(50);

                builder.Property(e => e.Amount)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(e => e.Description).HasMaxLength(50);

                builder.Property(e => e.ExecutorId)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(e => e.FactoryId)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(e => e.InvoiceId).HasMaxLength(50);

                builder.Property(e => e.Month)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(e => e.PaymentStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(e => e.Purpose).HasMaxLength(50);

                builder.Property(e => e.RowStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(e => e.TransactionId)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(e => e.TransactionType)
                    .HasMaxLength(50);
            builder.Property(p => p.UniqueId)
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            //builder.Property(e => e.UniqueId)
            //    .IsRequired();
            builder.HasQueryFilter(s => s.RowStatus != DB_ROW_STATUS.DELETED.ToString());
        }
    }
}
