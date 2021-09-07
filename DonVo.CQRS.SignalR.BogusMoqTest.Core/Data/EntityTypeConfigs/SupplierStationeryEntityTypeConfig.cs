using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Data.EntityTypeConfigs
{
    public class SupplierStationeryEntityTypeConfig : IEntityTypeConfiguration<SupplierStationery>
    {
        public void Configure(EntityTypeBuilder<SupplierStationery> builder)
        {
            builder.HasOne<Supplier>()
               .WithMany()
               .HasForeignKey(x => x.SupplierId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<InventoryItem>()
               .WithMany()
               .HasForeignKey(x => x.InventoryItemId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
