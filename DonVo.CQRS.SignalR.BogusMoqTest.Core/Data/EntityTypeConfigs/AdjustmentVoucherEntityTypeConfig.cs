using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Data.EntityTypeConfigs
{
    public class AdjustmentVoucherEntityTypeConfig : IEntityTypeConfiguration<AdjustmentVoucher>
    {
        public void Configure(EntityTypeBuilder<AdjustmentVoucher> builder)
        {
            builder.HasOne<Employee>()
               .WithMany()
               .HasForeignKey(x => x.EmployeeId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<InventoryItem>()
               .WithMany()
               .HasForeignKey(x => x.InventoryItemId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
