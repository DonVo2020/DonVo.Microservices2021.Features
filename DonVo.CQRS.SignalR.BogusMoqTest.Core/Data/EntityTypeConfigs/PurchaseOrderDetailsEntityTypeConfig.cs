using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Data.EntityTypeConfigs
{
    public class PurchaseOrderDetailsEntityTypeConfig : IEntityTypeConfiguration<PurchaseOrderDetails>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderDetails> builder)
        {
            builder.HasOne<PurchaseOrder>()
               .WithMany()
               .HasForeignKey(x => x.PurchaseOrder)
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
