using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DonVo.SignalR_MediatR.Core.Models.Entities;

namespace DonVo.SignalR_MediatR.Core.Data.EntityTypeConfigs
{
    public class MessageEntityTypeConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(x => x.Content)
                .IsRequired();

            builder.HasOne<AppUser>()
                .WithMany(x => x.MessagesRecieved)
                .HasForeignKey(x => x.RecipientId)
                .IsRequired().OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<AppUser>()
                .WithMany(x => x.MessagesSent)
                .HasForeignKey(x => x.SenderId)
                .IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
