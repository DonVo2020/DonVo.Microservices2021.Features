using DonVo.FactoryManagement.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Models.DbModels.EntityWiseConfiguration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(e => e.Id).HasMaxLength(50);

            builder.Property(e => e.FactoryId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.PermanentAddress).HasMaxLength(100);

            builder.Property(e => e.PresentAddress).HasMaxLength(150);

            builder.Property(e => e.RelatedId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.RowStatus)
                .IsRequired()
                .HasMaxLength(50);

            //builder.Property(e => e.UniqueId)
            //    .IsRequired();
            builder.Property(p => p.UniqueId)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            //builder.Property(p => p.Id)
            //    .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.HasQueryFilter(s => s.RowStatus != DB_ROW_STATUS.DELETED.ToString());
        }
    }
}
