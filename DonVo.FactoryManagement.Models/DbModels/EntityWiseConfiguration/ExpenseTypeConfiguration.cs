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
    public class ExpenseTypeConfiguration : IEntityTypeConfiguration<ExpenseType>
    {
       // string FactoryId = "c90a9cdf-ca6b-4f74-b9f6-d00cd37b1b30";
        public void Configure(EntityTypeBuilder<ExpenseType> builder)
        {

            builder.Property(e => e.Id).HasMaxLength(50);

            builder.Property(e => e.FactoryId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.RowStatus)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.UniqueId)
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            //builder.Property(e => e.UniqueId)
            //    .IsRequired();
            builder.HasQueryFilter(s => s.RowStatus != DB_ROW_STATUS.DELETED.ToString());



            //builder.HasData(
            //    new ExpenseType()
            //    {
            //        CreatedDateTime = DateTime.Now,
            //        FactoryId = FactoryId,
            //        Id = Guid.NewGuid().ToString(),
            //        Name = "Mobil",
            //        RowStatus = DB_ROW_STATUS.ADDED.ToString(),
            //        UpdatedDateTime = DateTime.Now
            //    },
            //    new ExpenseType()
            //    {
            //        CreatedDateTime = DateTime.Now,
            //        FactoryId = FactoryId,
            //        Id = Guid.NewGuid().ToString(),
            //        Name = "Diesel",
            //        RowStatus = DB_ROW_STATUS.ADDED.ToString(),
            //        UpdatedDateTime = DateTime.Now
            //    },
            //    new ExpenseType()
            //    {
            //        CreatedDateTime = DateTime.Now,
            //        FactoryId = FactoryId,
            //        Id = Guid.NewGuid().ToString(),
            //        Name = "Electricity",
            //        RowStatus = DB_ROW_STATUS.ADDED.ToString(),
            //        UpdatedDateTime = DateTime.Now
            //    },
            //    new ExpenseType()
            //    {
            //        CreatedDateTime = DateTime.Now,
            //        FactoryId = FactoryId,
            //        Id = Guid.NewGuid().ToString(),
            //        Name = "Thread",
            //        RowStatus = DB_ROW_STATUS.ADDED.ToString(),
            //        UpdatedDateTime = DateTime.Now
            //    }

            //    );
        }
    }
}
