using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Data.EntityTypeConfigs
{
    public class DisbursementDetailsEntityTypeConfig : IEntityTypeConfiguration<DisbursementDetails>
    {
        public void Configure(EntityTypeBuilder<DisbursementDetails> builder)
        {
            builder.HasOne<Disbursement>()
               .WithMany()
               .HasForeignKey(x => x.DisbursementId)
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
