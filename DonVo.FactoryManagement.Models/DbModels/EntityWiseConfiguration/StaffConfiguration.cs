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
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
           
                builder.Property(e => e.Id).HasMaxLength(50);

                builder.Property(e => e.BirthRegistrationNo).HasMaxLength(50);

                builder.Property(e => e.Code).HasMaxLength(50);

                builder.Property(e => e.DepartmentId).HasMaxLength(50);

                builder.Property(e => e.Email).HasMaxLength(50);

                builder.Property(e => e.FactoryId)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(e => e.ImageUrl).HasMaxLength(250);

                builder.Property(e => e.Name).HasMaxLength(150);

                builder.Property(e => e.NationalIdNo).HasMaxLength(50);

                builder.Property(e => e.RowStatus)
                    .IsRequired();
            builder.Property(p => p.UniqueId)
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            //builder.Property(e => e.UniqueId).HasMaxLength(50);
            builder.HasQueryFilter(s => s.RowStatus != DB_ROW_STATUS.DELETED.ToString());
        }
    }
}
