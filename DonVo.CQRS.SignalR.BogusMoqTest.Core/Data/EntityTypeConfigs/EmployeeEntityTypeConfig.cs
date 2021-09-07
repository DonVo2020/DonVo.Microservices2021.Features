using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Data.EntityTypeConfigs
{
    public class EmployeeEntityTypeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasOne<Department>()
             .WithMany()
             .HasForeignKey(x => x.DepartmentId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
