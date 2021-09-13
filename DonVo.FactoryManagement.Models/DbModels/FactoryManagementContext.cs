using System;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.DbModels.EntityWiseConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DonVo.FactoryManagement.Models.DbModels
{
    public partial class FactoryManagementContext : DbContext
    {
        public FactoryManagementContext()
        {
        }
        public FactoryManagementContext(DbContextOptions<FactoryManagementContext> options)
            : base(options)
        {
        }

        #region Db Sets
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<EquipmentCategory> EquipmentCategory { get; set; }
        public virtual DbSet<Expense> Expense { get; set; }
        public virtual DbSet<ExpenseType> ExpenseType { get; set; }
        public virtual DbSet<Factory> Factory { get; set; }
        public virtual DbSet<IncomeType> IncomeType { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceType> InvoiceType { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemCategory> ItemCategory { get; set; }
        public virtual DbSet<ItemStatus> ItemStatus { get; set; }
        public virtual DbSet<Payable> Payable { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }
        public virtual DbSet<Production> Production { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }
        public virtual DbSet<PurchaseType> PurchaseType { get; set; }
        public virtual DbSet<Receivable> Receivable { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<StockIn> StockIn { get; set; }
        public virtual DbSet<StockOut> StockOut { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<TblTransaction> Transaction { get; set; }
        public virtual DbSet<TransactionType> TransactionType { get; set; }
        public virtual DbSet<UserAuthInfo> UserAuthInfo { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        public virtual DbSet<ApiResourceMapping> ApiResourceMapping { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                /*
                #warning To protect potentially sensitive information in your connection string, you should move 
                #it out of source code.
                See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                */
               // optionsBuilder.UseSqlServer("Server=DESKTOP-VBDPK1F\\SQLEXPRESS;Database=FactoryManagementDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Entity Wise Configuration

            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new EquipmentConfiguration());
            modelBuilder.ApplyConfiguration(new EquipmentCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FactoryConfiguration());
            modelBuilder.ApplyConfiguration(new IncomeConfiguration());
            modelBuilder.ApplyConfiguration(new IncomeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new ItemCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ItemStatusConfiguration());
            modelBuilder.ApplyConfiguration(new PayableConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentStatusConfiguration());
            modelBuilder.ApplyConfiguration(new PhoneConfiguration());
            modelBuilder.ApplyConfiguration(new ProductionConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReceivableConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new SalesConfiguration());
            modelBuilder.ApplyConfiguration(new StaffConfiguration());
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new StockInConfiguration());
            modelBuilder.ApplyConfiguration(new StockOutConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new TblTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserAuthInfoConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new ApiResourceMappingConfiguration());
            #endregion
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
