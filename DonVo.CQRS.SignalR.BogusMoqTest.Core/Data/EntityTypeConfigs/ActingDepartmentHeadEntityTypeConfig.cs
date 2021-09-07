using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Data.EntityTypeConfigs
{
    public class ActingDepartmentHeadEntityTypeConfig : IEntityTypeConfiguration<ActingDepartmentHead>
    {
        public void Configure(EntityTypeBuilder<ActingDepartmentHead> builder)
        {
            builder.HasOne<Employee>()
             .WithMany()
             .HasForeignKey(x => x.EmployeeId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
