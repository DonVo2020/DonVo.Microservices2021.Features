using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Data.EntityTypeConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ActingDepartmentHead> ActingDepartmentHeads { get; set; }
        public DbSet<AdjustmentVoucher> AdjustmentVouchers { get; set; }
        public DbSet<CollectionPoint> CollectionPoints { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Disbursement> Disbursements { get; set; }
        public DbSet<DisbursementDetails> DisbursementDetails { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<InventoryManagement> InventoryManagements { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestDetails> RequestDetails { get; set; }
        public DbSet<Retrieval> Retrievals { get; set; }
        public DbSet<RetrievalDetails> RetrievalDetails { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierStationery> SupplierStationeries { get; set; }
        //public DbSet<ADProj.Models.CustomEmployeeMobile> CustomEmployeeMobile { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new ActingDepartmentHeadEntityTypeConfig().Configure(builder.Entity<ActingDepartmentHead>());
            new AdjustmentVoucherEntityTypeConfig().Configure(builder.Entity<AdjustmentVoucher>());
            new DisbursementDetailsEntityTypeConfig().Configure(builder.Entity<DisbursementDetails>());
            new EmployeeEntityTypeConfig().Configure(builder.Entity<Employee>());
            new InventoryManagementEntityTypeConfig().Configure(builder.Entity<InventoryManagement>());
            //new PurchaseOrderEntityTypeConfig().Configure(builder.Entity<PurchaseOrder>());
            //new PurchaseOrderDetailsEntityTypeConfig().Configure(builder.Entity<PurchaseOrderDetails>());
            new RequestEntityTypeConfig().Configure(builder.Entity<Request>());
            new RetrievalEntityTypeConfig().Configure(builder.Entity<Retrieval>());
            new SupplierStationeryEntityTypeConfig().Configure(builder.Entity<SupplierStationery>());

            builder.Entity<SupplierStationery>().HasAlternateKey(model =>
            new { model.SupplierId, model.InventoryItemId });
        }
    }
}
