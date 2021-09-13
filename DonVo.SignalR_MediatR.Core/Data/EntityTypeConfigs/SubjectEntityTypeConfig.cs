using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DonVo.SignalR_MediatR.Core.Models.Entities;

namespace DonVo.SignalR_MediatR.Core.Data.EntityTypeConfigs
{
    public class SubjectEntityTypeConfig : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasOne<AppUser>()
             .WithMany()
             .HasForeignKey(x => x.UserId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.OwnsOne(x => x.Grade)
                .Property(x => x.Value).IsRequired().HasMaxLength(3);

            builder.OwnsOne(x => x.Grade)
                .Property(x => x.Note).HasMaxLength(255);

            builder.OwnsOne(x => x.Grade)
            .Property(x => x.DateSet).IsRequired();
        }
    }
}
