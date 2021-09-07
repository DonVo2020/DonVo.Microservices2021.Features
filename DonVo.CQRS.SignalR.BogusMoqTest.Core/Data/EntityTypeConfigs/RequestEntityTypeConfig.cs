using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Data.EntityTypeConfigs
{
    public class RequestEntityTypeConfig : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasOne<Employee>()
             .WithMany()
             .HasForeignKey(x => x.EmployeeId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
