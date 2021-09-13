using DonVo.FactoryManagement.APIs.Utilities.ExceptionHandler;
using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.EntitywiseContracts;
using DonVo.FactoryManagement.Contracts.IBusinessServiceWrapper;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Repositories;
using DonVo.FactoryManagement.Repositories.EntitywiseRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service;
using Service.BusinessServices;
using Service.BusinessServiceWrapper;
using Service.BusinessWrapper;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.APIs.Utilities.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }
        public static void ConfigureDBContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<FactoryManagementContext>(opts =>
            opts.UseSqlServer(config.GetConnectionString("SqlConnection"), optionss => optionss.MigrationsAssembly("DonVo.FactoryManagement.APIs")), ServiceLifetime.Transient);
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IEquipmentService, EquipmentService>();
            services.AddTransient<IEquipmentCategoryService, EquipmentCategoryService>();
            services.AddTransient<IExpenseService, ExpenseService>();
            services.AddTransient<IExpenseTypeService, ExpenseTypeService>();
            services.AddTransient<IFactoryService, FactoryService>();
            services.AddTransient<IIncomeService, IncomeService>();
            services.AddTransient<IIncomeTypeService, IncomeTypeService>();
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IInvoiceTypeService, InvoiceTypeService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IItemCategoryService, ItemCategoryService>();
            services.AddTransient<IItemStatusService, ItemStatusService>();
            services.AddTransient<IPayableService, PayableService>();
            services.AddTransient<IPaymentStatusService, PaymentStatusService>();
            services.AddTransient<IPhoneService, PhoneService>();
            services.AddTransient<IProductionService, ProductionService>();
            services.AddTransient<IPurchaseService, PurchaseService>();
            services.AddTransient<IPurchaseTypeService, PurchaseTypeService>();
            services.AddTransient<IReceivableService, ReceivableService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ISalesService, SalesService>();
            services.AddTransient<IStaffService, StaffService>();
            services.AddTransient<IStockService, StockService>();
            services.AddTransient<IStockInService, StockInService>();
            services.AddTransient<IStockOutService, StockOutService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ITransactionTypeService, TransactionTypeService>();
            services.AddTransient<IUserAuthInfoService, UserAuthInfoService>();
            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddTransient<IUtilService, UtilService>();
            services.AddTransient<IApiResourceMappingService, ApiResourceMappingService>();















            //services.AddScoped<IAddressService, AddressService>();
            //services.AddScoped<ICustomerService, CustomerService>();
            //services.AddScoped<IDepartmentService, DepartmentService>();
            //services.AddScoped<IEquipmentService, EquipmentService>();
            //services.AddScoped<IEquipmentCategoryService, EquipmentCategoryService>();
            //services.AddScoped<IExpenseService, ExpenseService>();
            //services.AddScoped<IExpenseTypeService, ExpenseTypeService>();
            //services.AddScoped<IFactoryService, FactoryService>();
            //services.AddScoped<IIncomeService, IncomeService>();
            //services.AddScoped<IIncomeTypeService, IncomeTypeService>();
            //services.AddScoped<IInvoiceService, InvoiceService>();
            //services.AddScoped<IInvoiceTypeService, InvoiceTypeService>();
            //services.AddScoped<IItemService, ItemService>();
            //services.AddScoped<IItemCategoryService, ItemCategoryService>();
            //services.AddScoped<IItemStatusService, ItemStatusService>();
            //services.AddScoped<IPayableService, PayableService>();
            //services.AddScoped<IPaymentStatusService, PaymentStatusService>();
            //services.AddScoped<IPhoneService, PhoneService>();
            //services.AddScoped<IProductionService, ProductionService>();
            //services.AddScoped<IPurchaseService, PurchaseService>();
            //services.AddScoped<IPurchaseTypeService, PurchaseTypeService>();
            //services.AddScoped<IReceivableService, ReceivableService>();
            //services.AddScoped<IRoleService, RoleService>();
            //services.AddScoped<ISalesService, SalesService>();
            //services.AddScoped<IStaffService, StaffService>();
            //services.AddScoped<IStockService, StockService>();
            //services.AddScoped<IStockInService, StockInService>();
            //services.AddScoped<IStockOutService, StockOutService>();
            //services.AddScoped<ISupplierService, SupplierService>();
            //services.AddScoped<ITransactionService, TransactionService>();
            //services.AddScoped<ITransactionTypeService, TransactionTypeService>();
            //services.AddScoped<IUserAuthInfoService, UserAuthInfoService>();
            //services.AddScoped<IUserRoleService, UserRoleService>();
            //services.AddScoped<IUtilService, UtilService>();
        }
        public static void ConfigureRepository(this IServiceCollection services)
        {


            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IEquipmentRepository, EquipmentRepository>();
            services.AddTransient<IEquipmentCategoryRepository, EquipmentCategoryRepository>();
            services.AddTransient<IExpenseRepository, ExpenseRepository>();
            services.AddTransient<IExpenseTypeRepository, ExpenseTypeRepository>();
            services.AddTransient<IFactoryRepository, FactoryRepository>();
            services.AddTransient<IIncomeRepository, IncomeRepository>();
            services.AddTransient<IIncomeTypeRepository, IncomeTypeRepository>();
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            services.AddTransient<IInvoiceTypeRepository, InvoiceTypeRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IItemCategoryRepository, ItemCategoryRepository>();
            services.AddTransient<IItemStatusRepository, ItemStatusRepository>();
            services.AddTransient<IPayableRepository, PayableRepository>();
            services.AddTransient<IPaymentStatusRepository, PaymentStatusRepository>();
            services.AddTransient<IPhoneRepository, PhoneRepository>();
            services.AddTransient<IProductionRepository, ProductionRepository>();
            services.AddTransient<IPurchaseRepository, PurchaseRepository>();
            services.AddTransient<IPurchaseTypeRepository, PurchaseTypeRepository>();
            services.AddTransient<IReceivableRepository, ReceivableRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ISalesRepository, SalesRepository>();
            services.AddTransient<IStaffRepository, StaffRepository>();
            services.AddTransient<IStockRepository, StockRepository>();
            services.AddTransient<IStockInRepository, StockInRepository>();
            services.AddTransient<IStockOutRepository, StockOutRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ITransactionTypeRepository, TransactionTypeRepository>();
            services.AddTransient<IUserAuthInfoRepository, UserAuthInfoRepository>();
            services.AddTransient<IUserRoleRepository, UserRoleRepository>();
            services.AddTransient<IApiResourceMappingRepository, ApiResourceMappingRepository>();

            //services.AddScoped<IAddressRepository, AddressRepository>();
            //services.AddScoped<ICustomerRepository,CustomerRepository>();
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            //services.AddScoped<IEquipmentCategoryRepository, EquipmentCategoryRepository>();
            //services.AddScoped<IExpenseRepository, ExpenseRepository>();
            //services.AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>();
            //services.AddScoped<IFactoryRepository, FactoryRepository>();
            //services.AddScoped<IIncomeRepository, IncomeRepository>();
            //services.AddScoped<IIncomeTypeRepository, IncomeTypeRepository>();
            //services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            //services.AddScoped<IInvoiceTypeRepository, InvoiceTypeRepository>();
            //services.AddScoped<IItemRepository, ItemRepository>();
            //services.AddScoped<IItemCategoryRepository, ItemCategoryRepository>();
            //services.AddScoped<IItemStatusRepository, ItemStatusRepository>();
            //services.AddScoped<IPayableRepository, PayableRepository>();
            //services.AddScoped<IPaymentStatusRepository, PaymentStatusRepository>();
            //services.AddScoped<IPhoneRepository, PhoneRepository>();
            //services.AddScoped<IProductionRepository, ProductionRepository>();
            //services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            //services.AddScoped<IPurchaseTypeRepository, PurchaseTypeRepository>();
            //services.AddScoped<IReceivableRepository, ReceivableRepository>();
            //services.AddScoped<IRoleRepository, RoleRepository>();
            //services.AddScoped<ISalesRepository, SalesRepository>();
            //services.AddScoped<IStaffRepository, StaffRepository>();
            //services.AddScoped<IStockRepository, StockRepository>();
            //services.AddScoped<IStockInRepository, StockInRepository>();
            //services.AddScoped<IStockOutRepository, StockOutRepository>();
            //services.AddScoped<ISupplierRepository, SupplierRepository>();
            //services.AddScoped<ITransactionRepository, TransactionRepository>();
            //services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
            //services.AddScoped<IUserAuthInfoRepository, UserAuthInfoRepository>();
            //services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            // services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddTransient<IRepositoryWrapper, RepositoryWrapper>();
        }
        public static void ConfigureServiceWrapper(this IServiceCollection services)
        {
            //services.AddScoped<IServiceWrapper, ServiceWrapper>();
            //services.AddScoped<IPurchaseWrapperService, PurchaseWrapperService>();
            //services.AddScoped<IBusinessWrapperService, BusinessWrapperService>();
            //services.AddScoped<IBusinessService, BusinessServices>();


            services.AddTransient<IServiceWrapper, ServiceWrapper>();
            services.AddTransient<IPurchaseWrapperService, PurchaseWrapperService>();
            services.AddTransient<IBusinessWrapperService, BusinessWrapperService>();
            services.AddTransient<IBusinessService, BusinessServices>();
        }
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
        public static void ConfigureCustomSwaggerGenerator(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Employee API",
                    Version = "v1",
                    Description = "An API to perform Employee operations",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "John Walkner",
                        Email = "John.Walkner@gmail.com",
                        Url = new Uri("https://twitter.com/jwalkner"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Employee API LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });

        }








        public static void ConfigureJwtAuth(this IServiceCollection services)
        {

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "http://localhost:63048",
            ValidAudience = "http://localhost:4200",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                }
                return Task.CompletedTask;
            }
        };
    });

        }



    }
    public static class MiddlewareExtension
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
