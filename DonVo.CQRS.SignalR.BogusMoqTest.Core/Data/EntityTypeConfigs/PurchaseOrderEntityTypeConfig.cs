using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Data.EntityTypeConfigs
{
    public class PurchaseOrderEntityTypeConfig : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.HasOne<Supplier>()
             .WithMany()
             .HasForeignKey(x => x.SupplierId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
