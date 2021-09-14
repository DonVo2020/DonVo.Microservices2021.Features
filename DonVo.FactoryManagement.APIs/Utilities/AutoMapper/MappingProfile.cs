using AutoMapper;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.Enums;
using DonVo.FactoryManagement.Models.ViewModels.ApiResourceMapping;
using DonVo.FactoryManagement.Models.ViewModels.CustomerView;
using DonVo.FactoryManagement.Models.ViewModels.Department;
using DonVo.FactoryManagement.Models.ViewModels.Equipment;
using DonVo.FactoryManagement.Models.ViewModels.EquipmentCategory;
using DonVo.FactoryManagement.Models.ViewModels.Expense;
using DonVo.FactoryManagement.Models.ViewModels.ExpenseType;
using DonVo.FactoryManagement.Models.ViewModels.Factory;
using DonVo.FactoryManagement.Models.ViewModels.Income;
using DonVo.FactoryManagement.Models.ViewModels.IncomeType;
using DonVo.FactoryManagement.Models.ViewModels.InvoiceType;
using DonVo.FactoryManagement.Models.ViewModels.Item;
using DonVo.FactoryManagement.Models.ViewModels.ItemCategoryView;
using DonVo.FactoryManagement.Models.ViewModels.ItemStatus;
using DonVo.FactoryManagement.Models.ViewModels.Payable;
using DonVo.FactoryManagement.Models.ViewModels.Payment;
using DonVo.FactoryManagement.Models.ViewModels.PaymentStatus;
using DonVo.FactoryManagement.Models.ViewModels.Production;
using DonVo.FactoryManagement.Models.ViewModels.Purchase;
using DonVo.FactoryManagement.Models.ViewModels.Receivable;
using DonVo.FactoryManagement.Models.ViewModels.Role;
using DonVo.FactoryManagement.Models.ViewModels.Sales;
using DonVo.FactoryManagement.Models.ViewModels.Staff;
using DonVo.FactoryManagement.Models.ViewModels.Stock;
using DonVo.FactoryManagement.Models.ViewModels.Supplier;
using DonVo.FactoryManagement.Models.ViewModels.Transaction;
using DonVo.FactoryManagement.Models.ViewModels.UserAuthInfo;
using System;

namespace DonVo.FactoryManagement.APIs.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapping_CustomerVM();
            Mapping_ItemCategoryVM();
            Mapping_ItemVM();
            Mapping_StockVM();
            Mapping_SupplierVM();
            Mapping_StaffVM();
            Mapping_ItemStatusVM();
            Mapping_DepartmentVM();
            Mapping_EquipmentVM();
            Mapping_EquipmentCategoryVM();
            Mapping_ExpenseTypeVM();
            Mapping_IncomeTypeVM();
            Mapping_InvoiceTypeVM();
            Mapping_Purchase();
            Mapping_Payment();
            Mapping_Sales();
            Mapping_CustomerHistory();
            Mapping_SupplierHistory();
            Mapping_Production();
            Mapping_StaffHistory();
            Mapping_SalesReturn();
            Mapping_PurchaseReturn();
            Mapping_MonthlyReport();
            Mapping_Income();
            Mapping_Expense();
            Mapping_Factory();
            Mapping_ItemStatus();
            Mapping_PaymentStatus();
            Mapping_Role();
            Mapping_UserAuthInfo();
            Mapping_ApiResourceMapping();
        }
        public void Mapping_CustomerVM()
        {
            // GET
            CreateMap<Customer, CustomerVM>()
                   .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                    .ForMember(dest => dest.ImageUrl, act => act.MapFrom(src => src.ImageUrl))
                     .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                     .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                         .ForMember(dest => dest.PresentAddress, act => act.MapFrom(src => src.PresentAddress))
                         .ForMember(dest => dest.PermanentAddress, act => act.MapFrom(src => src.PermanentAddress))
                         .ForMember(dest => dest.CellNo, act => act.MapFrom(src => src.Number))
                         .ForMember(dest => dest.AlternateCellNo, act => act.MapFrom(src => src.AlternateNumber_1))
                          .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.Id))
                     .ForAllOtherMembers(act => act.Ignore());
            // UPDATE + ADDITION
            CreateMap<CustomerVM, Customer>()
                   .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                    .ForMember(dest => dest.ImageUrl, act => act.MapFrom(src => src.ImageUrl))
                     .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                     .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                         .ForMember(dest => dest.PresentAddress, act => act.MapFrom(src => src.PresentAddress))
                         .ForMember(dest => dest.PermanentAddress, act => act.MapFrom(src => src.PermanentAddress))
                         .ForMember(dest => dest.Number, act => act.MapFrom(src => src.CellNo))
                         .ForMember(dest => dest.AlternateNumber_1, act => act.MapFrom(src => src.AlternateCellNo))
                     .ForAllOtherMembers(act => act.Ignore());

            //            CreateMap<UpdateCustomerViewModel, Customer>()
            //.ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
            //.ForMember(dest => dest.ImageUrl, act => act.MapFrom(src => src.ImageUrl))
            //.ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.PresentAddress, act => act.MapFrom(src => src.PresentAddress))
            //    .ForMember(dest => dest.PermanentAddress, act => act.MapFrom(src => src.PermanentAddress))
            //        .ForMember(dest => dest.Number, act => act.MapFrom(src => src.CellNo))
            //    .ForMember(dest => dest.AlternateNumber_1, act => act.MapFrom(src => src.AlternateCellNo))
            //.ForAllOtherMembers(act => act.Ignore());


            //CreateMap<AddCustomerViewModel, Address>()
            //    .ForMember(dest => dest.PresentAddress, act => act.MapFrom(src => src.PresentAddress))
            //    .ForMember(dest => dest.PermanentAddress, act => act.MapFrom(src => src.PermanentAddress))
            //    .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            //    .ForAllOtherMembers(act => act.Ignore());

            //CreateMap<AddCustomerViewModel, Phone>()
            //    .ForMember(dest => dest.Number, act => act.MapFrom(src => src.CellNo))
            //    .ForMember(dest => dest.AlternateNumber_1, act => act.MapFrom(src => src.AlternateCellNo))
            //    .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            //    .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_StaffVM()
        {
            // GET
            CreateMap<Staff, StaffVM>()
                   .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                   .ForMember(dest => dest.ImageUrl, act => act.MapFrom(src => src.ImageUrl))
                   .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                   .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                   .ForMember(dest => dest.PresentAddress, act => act.MapFrom(src => src.PresentAddress))
                   .ForMember(dest => dest.PermanentAddress, act => act.MapFrom(src => src.PermanentAddress))
                   .ForMember(dest => dest.CellNo, act => act.MapFrom(src => src.Number))
                   .ForMember(dest => dest.AlternateCellNo, act => act.MapFrom(src => src.AlternateNumber_1))
                   .ForMember(dest => dest.RoleId, act => act.MapFrom(src => src.RoleId))
                   .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                   .ForAllOtherMembers(act => act.Ignore());
            // UPDATE + ADDITION
            CreateMap<StaffVM, Staff>()
                   .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                   .ForMember(dest => dest.ImageUrl, act => act.MapFrom(src => src.ImageUrl))
                   .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                   .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                   .ForMember(dest => dest.PresentAddress, act => act.MapFrom(src => src.PresentAddress))
                   .ForMember(dest => dest.PermanentAddress, act => act.MapFrom(src => src.PermanentAddress))
                   .ForMember(dest => dest.Number, act => act.MapFrom(src => src.CellNo))
                   .ForMember(dest => dest.AlternateNumber_1, act => act.MapFrom(src => src.AlternateCellNo))
                   .ForMember(dest => dest.RoleId, act => act.MapFrom(src => src.RoleId))
                   .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                   .ForAllOtherMembers(act => act.Ignore());


            CreateMap<StaffVM, UserAuthInfo>()
                  .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.UserName))
                  .ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
                  .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                  .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_SupplierVM()
        {
            // GET
            CreateMap<Supplier, SupplierVM>()
                   .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                    .ForMember(dest => dest.ImageUrl, act => act.MapFrom(src => src.ImageUrl))
                     .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                     .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                         .ForMember(dest => dest.PresentAddress, act => act.MapFrom(src => src.PresentAddress))
                         .ForMember(dest => dest.PermanentAddress, act => act.MapFrom(src => src.PermanentAddress))
                         .ForMember(dest => dest.CellNo, act => act.MapFrom(src => src.Number))
                         .ForMember(dest => dest.AlternateCellNo, act => act.MapFrom(src => src.AlternateNumber_1))
                          .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                     .ForAllOtherMembers(act => act.Ignore());
            // UPDATE + ADDITION
            CreateMap<SupplierVM, Supplier>()
                   .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                    .ForMember(dest => dest.ImageUrl, act => act.MapFrom(src => src.ImageUrl))
                     .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                     .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                         .ForMember(dest => dest.PresentAddress, act => act.MapFrom(src => src.PresentAddress))
                         .ForMember(dest => dest.PermanentAddress, act => act.MapFrom(src => src.PermanentAddress))
                         .ForMember(dest => dest.Number, act => act.MapFrom(src => src.CellNo))
                         .ForMember(dest => dest.AlternateNumber_1, act => act.MapFrom(src => src.AlternateCellNo))
                     .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_StockVM()
        {
            // GET
            CreateMap<Stock, StockVM>()
                   .ForMember(dest => dest.ExpiryDate, act => act.MapFrom(src => src.ExpiryDate))
                    .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.ItemId))
                     .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                     .ForMember(dest => dest.ItemName, act => act.MapFrom(src => src.Item.Name))
                         .ForMember(dest => dest.ItemStatus, act => act.MapFrom(src => src.ItemStatus.Name))
                         .ForMember(dest => dest.ItemStatusId, act => act.MapFrom(src => src.ItemStatus.Id))
                         .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                          .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                     .ForAllOtherMembers(act => act.Ignore());
            // UPDATE + ADDITION
            CreateMap<StockVM, Stock>()
              .ForMember(dest => dest.ExpiryDate, act => act.MapFrom(src => src.ExpiryDate))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                         .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                           .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.ItemId))
                            .ForMember(dest => dest.ItemStatusId, act => act.MapFrom(src => src.ItemStatusId))
                     .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_DepartmentVM()
        {
            // GET
            CreateMap<Department, DepartmentVM>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
             .ForAllOtherMembers(act => act.Ignore());

            // ADDITION + UPDATE 
            CreateMap<DepartmentVM, Department>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            .ForAllOtherMembers(act => act.Ignore());

        }
        public void Mapping_EquipmentVM()
        {
            // GET
            CreateMap<Equipment, EquipmentVM>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
            .ForMember(dest => dest.EquipmentCategoryId, act => act.MapFrom(src => src.EquipmentCategory.Id))
            .ForMember(dest => dest.MachineNumber, act => act.MapFrom(src => src.MachineNumber))
            .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))
            .ForMember(dest => dest.EquipmentCategoryName, act => act.MapFrom(src => src.EquipmentCategory.Name))
            .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
             .ForAllOtherMembers(act => act.Ignore());

            // ADDITION + UPDATE 
            CreateMap<EquipmentVM, Equipment>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            .ForMember(dest => dest.EquipmentCategoryId, act => act.MapFrom(src => src.EquipmentCategoryId))
            .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
            .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))
            .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_EquipmentCategoryVM()
        {
            // GET
            CreateMap<EquipmentCategory, EquipmentCategoryVM>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
             .ForAllOtherMembers(act => act.Ignore());

            // ADDITION + UPDATE 
            CreateMap<EquipmentCategoryVM, EquipmentCategory>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_ExpenseTypeVM()
        {
            // GET
            CreateMap<ExpenseType, ExpenseTypeVM>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
             .ForAllOtherMembers(act => act.Ignore());

            // ADDITION + UPDATE 
            CreateMap<ExpenseTypeVM, ExpenseType>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_IncomeTypeVM()
        {
            // GET
            CreateMap<IncomeType, IncomeTypeVM>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
             .ForAllOtherMembers(act => act.Ignore());

            // ADDITION + UPDATE 
            CreateMap<IncomeTypeVM, IncomeType>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_InvoiceTypeVM()
        {
            // GET
            CreateMap<InvoiceType, InvoiceTypeVM>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
             .ForAllOtherMembers(act => act.Ignore());

            // ADDITION + UPDATE 
            CreateMap<InvoiceTypeVM, InvoiceType>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_UpdateCustomerViewModelmm()
        {
            //CreateMap<UpdateCustomerViewModel, Address>()
            //    .ForMember(dest => dest.PresentAddress, act => act.MapFrom(src => src.PresentAddress))
            //    .ForMember(dest => dest.PermanentAddress, act => act.MapFrom(src => src.PermanentAddress))
            //    .ForAllOtherMembers(act => act.Ignore());


            //CreateMap<UpdateCustomerViewModel, Phone>()
            //    .ForMember(dest => dest.Number, act => act.MapFrom(src => src.CellNo))
            //    .ForMember(dest => dest.AlternateNumber_1, act => act.MapFrom(src => src.AlternateCellNo))
            //    .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_ItemCategoryVM()
        {
            // GET
            CreateMap<ItemCategory, ItemCategoryVM>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
             .ForAllOtherMembers(act => act.Ignore());

            // ADDITION + UPDATE 
            CreateMap<ItemCategoryVM, ItemCategory>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            .ForAllOtherMembers(act => act.Ignore());

            //  CreateMap<List<ItemCategory>, List<ItemCategoryVM>>();
        }
        public void Mapping_ItemVM()
        {
            // GET 
            CreateMap<Item, ItemVM>()
                  .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                  .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                  .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                  .ForMember(dest => dest.UnitPrice, act => act.MapFrom(src => src.UnitPrice))
                  .ForMember(dest => dest.CategoryId, act => act.MapFrom(src => src.CategoryId))
                  .ForMember(dest => dest.CategoryName, act => act.MapFrom(src => src.ItemCategory.Name))
                  .ForAllOtherMembers(act => act.Ignore());

            // ADDITION + UPDATE 
            CreateMap<ItemVM, Item>()
                  .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                  .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                  .ForMember(dest => dest.UnitPrice, act => act.MapFrom(src => src.UnitPrice))
                  .ForMember(dest => dest.CategoryId, act => act.MapFrom(src => src.CategoryId))
                  .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_CustomerListView()
        {
            //           CreateMap<Customer, ListCustomerVM>()
            //                    .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            //                     .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
            //                    .ForMember(dest => dest.ImageUrl, act => act.MapFrom(src => src.ImageUrl))
            //                     .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            //                      .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.Id))
            //                       .ForMember(dest => dest.PermanentAddress, act => act.MapFrom(src => src.PermanentAddress))
            //         .ForMember(dest => dest.PresentAddress, act => act.MapFrom(src => src.PresentAddress))
            //         .ForMember(dest => dest.CellNo, act => act.MapFrom(src => src.Number))
            //.ForMember(dest => dest.AlternateCellNo, act => act.MapFrom(src => src.AlternateNumber_1))
            //                       .ForAllOtherMembers(act => act.Ignore());

            //            CreateMap<Address, ListCustomerVM>()
            //         .ForMember(dest => dest.PermanentAddress, act => act.MapFrom(src => src.PermanentAddress))
            //          .ForMember(dest => dest.PresentAddress, act => act.MapFrom(src => src.PresentAddress))
            //            .ForAllOtherMembers(act => act.Ignore());


            //            CreateMap<Phone, ListCustomerVM>()
            //.ForMember(dest => dest.CellNo, act => act.MapFrom(src => src.Number))
            // .ForMember(dest => dest.AlternateCellNo, act => act.MapFrom(src => src.AlternateNumber_1))
            //   .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_ItemStatusVM()
        {
            // GET
            CreateMap<ItemStatus, ItemStatusVM>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
             .ForAllOtherMembers(act => act.Ignore());

            // ADDITION + UPDATE 
            //CreateMap<ItemCategoryVM, ItemCategory>()
            //.ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            //.ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            //.ForAllOtherMembers(act => act.Ignore());

            //  CreateMap<List<ItemCategory>, List<ItemCategoryVM>>();
        }
        public void Mapping_Purchase()
        {
            //------------------------------Expense-------------------------------------
            // Addition
            // InvoiceId 
            CreateMap<PurchaseVM, Expense>()
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.PaidAmount))
                .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.SupplierVM.Id))
                .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.ExpenseTypeId, act => act.MapFrom(src => src.ExpenseType.Id))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => DateTime.Now))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Purchase"))
                 .ForMember(dest => dest.Description, act => act.MapFrom(src => "Paid to " +
                 src.SupplierVM.Name))
                .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------Invoice-------------------------------------
            // Addition
            // InvoiceId 
            CreateMap<PurchaseVM, Invoice>()
               .ForMember(dest => dest.AmountAfterDiscount, act => act.MapFrom(src => (src.TotalAmount - src.DiscountAmount)))
               .ForMember(dest => dest.AmountBeforeDiscount, act => act.MapFrom(src => (src.TotalAmount)))
               .ForMember(dest => dest.AmountPaid, act => act.MapFrom(src => (src.PaidAmount)))
               .ForMember(dest => dest.ClientId, act => act.MapFrom(src => (src.SupplierVM.Id)))
               .ForMember(dest => dest.Discount, act => act.MapFrom(src => src.DiscountAmount))
               .ForMember(dest => dest.Due, act => act.MapFrom(src => src.DueAmount))
               .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
               .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
               .ForMember(dest => dest.InvoiceTypeId, act => act.MapFrom(src => src.InvoiceType.Id))
               .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.IsFullyPaid, act => act.MapFrom(src => !(src.DueAmount > 0)))
               .ForMember(dest => dest.DateOfOcurrance, act => act.MapFrom(src => DateTime.Now))
               .ForAllOtherMembers(act => act.Ignore());

            //----------------------------------Payable-------------------------------------
            // Addition
            // InvoiceId, 
            CreateMap<PurchaseVM, Payable>()
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.DueAmount))
                .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.SupplierVM.Id))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Purchase Due"))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => " to " + src.SupplierVM.Name))
                .ForAllOtherMembers(act => act.Ignore());

            //-----------------------------------Purchase-------------------------------------
            // Addition
            // InvoiceId
            CreateMap<PurchaseItemVM, Purchase>()
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => (src.UnitPrice * src.Quantity)))
                .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.Item.Id))
                .ForMember(dest => dest.ItemCategoryId, act => act.MapFrom(src => src.ItemCategory.Id))
                .ForMember(dest => dest.UnitPrice, act => act.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.SupplierVM.Id))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => DateTime.Now))
                .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------------Stock-------------------------------------
            // Addition

            CreateMap<PurchaseItemVM, Stock>()
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.Item.Id))
                .ForMember(dest => dest.ItemStatusId, act => act.MapFrom(src => src.ItemStatus.Id))
                .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------------StockIn-------------------------------------
            // Addition
            // InvoiceId
            CreateMap<PurchaseItemVM, StockIn>()
                 .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                  .ForMember(dest => dest.ItemStatusId, act => act.MapFrom(src => src.ItemStatus.Id))
                  .ForMember(dest => dest.BatchNumber, act => act.MapFrom(src => Guid.NewGuid()))
                  .ForMember(dest => dest.ExpiryDate, act => act.MapFrom(src => src.ExpiryDate))
                  .ForMember(dest => dest.AddedDateTime, act => act.MapFrom(src => DateTime.Now))
                  .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.Item.Id))
                  .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                  .ForMember(dest => dest.Unitprice, act => act.MapFrom(src => src.UnitPrice))
                  .ForMember(dest => dest.SupplierId, act => act.MapFrom(src => src.SupplierVM.Id))
                  .ForMember(dest => dest.ExecutorId, act => act.MapFrom(src => src.EmployeeId))
                  .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                  .ForMember(dest => dest.Source, act => act.MapFrom(src => StockInSource.Purchase.ToString()))
                  .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------------Transaction-------------------------------------
            // Addition
            // Amount, InvoiceId , PaymentStatus ,TransactionType
            CreateMap<PurchaseVM, TblTransaction>()
                 .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                 .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.SupplierVM.Id))
                 .ForMember(dest => dest.ExecutorId, act => act.MapFrom(src => src.EmployeeId))
                 .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                 .ForMember(dest => dest.TransactionId, act => act.MapFrom(src => Guid.NewGuid()))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.PaymentStatus, act => act.MapFrom(src => "NA"))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => TransactionPurpose.PurchasePayment.ToString()))
                   .ForMember(dest => dest.Description, act => act.MapFrom(src => " Paid to " + src.SupplierVM.Name))

                 .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------Invoice-------------------------------------
            CreateMap<Invoice, PurchaseVM>()
               .ForMember(dest => dest.TotalAmount, act => act.MapFrom(src => (src.AmountBeforeDiscount)))
               .ForMember(dest => dest.DiscountAmount, act => act.MapFrom(src => (src.Discount)))
               .ForMember(dest => dest.PaidAmount, act => act.MapFrom(src => (src.AmountPaid)))
               .ForMember(dest => dest.SupplierName, act => act.MapFrom(src => (src.Supplier.Name)))
               .ForMember(dest => dest.DiscountAmount, act => act.MapFrom(src => src.Discount))
               .ForMember(dest => dest.DueAmount, act => act.MapFrom(src => src.Due))
               .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
               .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
               //.ForMember(dest => dest.InvoiceTypeId, act => act.MapFrom(src => src.InvoiceTypeId))
               .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.Id))
               //.ForMember(dest => dest.IsFullyPaid, act => act.MapFrom(src => !(src.DueAmount > 0)))
               .ForMember(dest => dest.OcurranceDate, act => act.MapFrom(src => src.DateOfOcurrance))
               .ForAllOtherMembers(act => act.Ignore());

            //-----------------------------------Purchase-------------------------------------
            // Addition
            // InvoiceId
            CreateMap<Purchase, PurchaseItemVM>()
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                // .ForMember(dest => dest., act => act.MapFrom(src => (src.UnitPrice * src.Quantity)))
                .ForMember(dest => dest.ItemName, act => act.MapFrom(src => src.Item.Name))
                .ForMember(dest => dest.ItemCategoryName, act => act.MapFrom(src => src.Item.ItemCategory.Name))
                .ForMember(dest => dest.UnitPrice, act => act.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                /// .ForMember(dest => dest., act => act.MapFrom(src => src.SupplierVM.Id))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Month, act => act.MapFrom(src => src.Month))
                .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_Sales()
        {
            //------------------------------Income-------------------------------------
            // Addition
            // InvoiceId 
            CreateMap<SalesVM, Income>()
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.PaidAmount))
                .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.CustomerVM.CustomerId))
                .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.IncomeTypeId, act => act.MapFrom(src => src.IncomeType.Id))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => " Sales "))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => " from " + src.CustomerVM.Name))
                .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------Invoice-------------------------------------
            // Addition
            // InvoiceId 
            CreateMap<SalesVM, Invoice>()
               .ForMember(dest => dest.AmountAfterDiscount, act => act.MapFrom(src => (src.TotalAmount - src.DiscountAmount)))
               .ForMember(dest => dest.AmountBeforeDiscount, act => act.MapFrom(src => (src.TotalAmount)))
                .ForMember(dest => dest.AmountPaid, act => act.MapFrom(src => (src.PaidAmount)))
               .ForMember(dest => dest.ClientId, act => act.MapFrom(src => (src.CustomerVM.CustomerId)))
               .ForMember(dest => dest.Discount, act => act.MapFrom(src => src.DiscountAmount))
               .ForMember(dest => dest.Due, act => act.MapFrom(src => src.DueAmount))
               .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
               .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
               .ForMember(dest => dest.InvoiceTypeId, act => act.MapFrom(src => src.InvoiceType.Id))
               .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.IsFullyPaid, act => act.MapFrom(src => !(src.DueAmount > 0)))
               .ForMember(dest => dest.DateOfOcurrance, act => act.MapFrom(src => DateTime.Now))
               .ForAllOtherMembers(act => act.Ignore());

            //----------------------------------Receivable-------------------------------------
            // Addition
            // InvoiceId, 
            CreateMap<SalesVM, Receivable>()
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.DueAmount))
                .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.CustomerVM.CustomerId))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Sales"))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => "Sales Due from " + src.CustomerVM.Name))
                .ForAllOtherMembers(act => act.Ignore());

            //-----------------------------------Sales-------------------------------------
            // Addition
            // InvoiceId
            CreateMap<SalesItemVM, Sales>()
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => (src.UnitPrice * src.Quantity)))
                .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.Item.Id))
                .ForMember(dest => dest.ItemCategoryId, act => act.MapFrom(src => src.ItemCategory.Id))
                .ForMember(dest => dest.UnitPrice, act => act.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.CustomerVM.CustomerId))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => DateTime.Now))
                .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------------Stock-------------------------------------
            // Addition

            CreateMap<SalesItemVM, Stock>()
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.Item.Id))
                .ForMember(dest => dest.ItemStatusId, act => act.MapFrom(src => src.ItemStatus.Id))
                .ForAllOtherMembers(act => act.Ignore()); ;

            //-------------------------------------StockIn-------------------------------------
            // Addition
            // InvoiceId
            CreateMap<SalesItemVM, StockOut>()
                 .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                  .ForMember(dest => dest.ItemStatusId, act => act.MapFrom(src => src.ItemStatus.Id))
                  .ForMember(dest => dest.BatchNumber, act => act.MapFrom(src => Guid.NewGuid()))
                  .ForMember(dest => dest.ExpiryDate, act => act.MapFrom(src => src.ExpiryDate))
                  .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.Item.Id))
                  .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                  .ForMember(dest => dest.Unitprice, act => act.MapFrom(src => src.UnitPrice))
                  .ForMember(dest => dest.BuyerId, act => act.MapFrom(src => src.CustomerVM.CustomerId))
                  .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                  .ForMember(dest => dest.ExecutorId, act => act.MapFrom(src => src.EmployeeId))
                  .ForMember(dest => dest.CurrentDateTime, act => act.MapFrom(src => DateTime.Now))
                  .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------------Transaction-------------------------------------
            // Addition
            // Amount, InvoiceId , PaymentStatus ,TransactionType
            CreateMap<SalesVM, TblTransaction>()
                 .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                 .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.CustomerVM.CustomerId))
                 .ForMember(dest => dest.ExecutorId, act => act.MapFrom(src => src.EmployeeId))
                 .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                 .ForMember(dest => dest.TransactionId, act => act.MapFrom(src => Guid.NewGuid()))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Sales Payment"))
                 .ForMember(dest => dest.Description, act => act.MapFrom(src => " Recieved from " + src.CustomerVM.Name))
                 .ForMember(dest => dest.PaymentStatus, act => act.MapFrom(src => "NA"))
                 .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------Invoice-------------------------------------
            CreateMap<Invoice, SalesVM>()
               .ForMember(dest => dest.TotalAmount, act => act.MapFrom(src => (src.AmountBeforeDiscount)))
               .ForMember(dest => dest.DiscountAmount, act => act.MapFrom(src => (src.Discount)))
               .ForMember(dest => dest.PaidAmount, act => act.MapFrom(src => (src.AmountPaid)))
               .ForMember(dest => dest.CustomerName, act => act.MapFrom(src => (src.Customer.Name)))
               .ForMember(dest => dest.DiscountAmount, act => act.MapFrom(src => src.Discount))
               .ForMember(dest => dest.DueAmount, act => act.MapFrom(src => src.Due))
               .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
               .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
               //.ForMember(dest => dest.InvoiceTypeId, act => act.MapFrom(src => src.InvoiceTypeId))
               .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.Id))
               //.ForMember(dest => dest.IsFullyPaid, act => act.MapFrom(src => !(src.DueAmount > 0)))
               .ForMember(dest => dest.OcurranceDate, act => act.MapFrom(src => src.DateOfOcurrance))
               .ForAllOtherMembers(act => act.Ignore());

            //-----------------------------------Purchase-------------------------------------
            // Addition
            // InvoiceId
            CreateMap<Sales, SalesItemVM>()
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                // .ForMember(dest => dest., act => act.MapFrom(src => (src.UnitPrice * src.Quantity)))
                .ForMember(dest => dest.ItemName, act => act.MapFrom(src => src.Item.Name))
                .ForMember(dest => dest.ItemCategoryName, act => act.MapFrom(src => src.Item.ItemCategory.Name))
                .ForMember(dest => dest.UnitPrice, act => act.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                /// .ForMember(dest => dest., act => act.MapFrom(src => src.SupplierVM.Id))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Month, act => act.MapFrom(src => src.Month))
                .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_Payment()
        {
            //------------------------------Income-------------------------------------
            //  
            CreateMap<PaymentVM, Income>()
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount))
                .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.IncomeTypeId, act => act.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => " Cash Payment "))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => " from " + src.ClientName))
                .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------Invoice-------------------------------------
            //
            CreateMap<PaymentVM, Invoice>()
               .ForMember(dest => dest.AmountPaid, act => act.MapFrom(src => src.Amount))
               .ForMember(dest => dest.AmountAfterDiscount, act => act.MapFrom(src => src.Amount))
               .ForMember(dest => dest.AmountBeforeDiscount, act => act.MapFrom(src => src.Amount))
               .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.ClientId))
               //.ForMember(dest => dest.Discount, act => act.MapFrom(src => src.DiscountAmount))
               //.ForMember(dest => dest.Due, act => act.MapFrom(src => src.DueAmount))
               .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
               .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
               .ForMember(dest => dest.InvoiceTypeId, act => act.MapFrom(src => src.InvoiceTypeId))
               .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => Guid.NewGuid()))
               //.ForMember(dest => dest.IsFullyPaid, act => act.MapFrom(src => !(src.DueAmount > 0)))
               .ForMember(dest => dest.DateOfOcurrance, act => act.MapFrom(src => DateTime.Now))
               .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------PaymentVM-------------------------------------
            //
            CreateMap<Invoice, PaymentVM>()
               .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.AmountPaid))
               .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.AmountPaid))
               .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.AmountPaid))
               .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.ClientId))
               //.ForMember(dest => dest.Discount, act => act.MapFrom(src => src.DiscountAmount))
               //.ForMember(dest => dest.Due, act => act.MapFrom(src => src.DueAmount))
               .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
               .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
               .ForMember(dest => dest.InvoiceTypeId, act => act.MapFrom(src => src.InvoiceTypeId))
               .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.Id))
               .ForMember(dest => dest.ClientName, act => act.MapFrom(src => src.Customer.Name))
               //.ForMember(dest => dest.IsFullyPaid, act => act.MapFrom(src => !(src.DueAmount > 0)))
               .ForMember(dest => dest.PaymentDate, act => act.MapFrom(src => src.DateOfOcurrance))
               .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------------Transaction-------------------------------------
            // PaymentStatus ,TransactionType
            CreateMap<PaymentVM, TblTransaction>()
                 .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                 .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.ClientId))
                 .ForMember(dest => dest.ExecutorId, act => act.MapFrom(src => src.EmployeeId))
                 .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                 .ForMember(dest => dest.TransactionId, act => act.MapFrom(src => Guid.NewGuid()))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount))
                 .ForMember(dest => dest.PaymentStatus, act => act.MapFrom(src => "NA"))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => " Cash Payment "))
                 //.ForMember(dest => dest.Description, act => act.MapFrom(src => " for " + src.ClientName))
                 .ForAllOtherMembers(act => act.Ignore());

            //-----------------------------------------Expense-------------------------------------
            CreateMap<PaymentVM, Expense>()
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount))
                .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.ExpenseTypeId, act => act.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => " Cash Payment "))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => " Paid To " + src.ClientName))
                .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_CustomerHistory()
        {
            //-----------------------------------Sales-------------------------------------
            CreateMap<Sales, CustomerHistory>()
                .ForMember(dest => dest.CategoryId, act => act.MapFrom(src => src.ItemCategoryId))
                .ForMember(dest => dest.CategoryName, act => act.MapFrom(src => src.Item.ItemCategory.Name))
                .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.ItemName, act => act.MapFrom(src => src.Item.Name))
                .ForMember(dest => dest.UnitPrice, act => act.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.OccurranceDate))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Type, act => act.MapFrom(src => "SaleItem"))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Sales"))
                .ForAllOtherMembers(act => act.Ignore());

            //-----------------------------------Invoice-------------------------------------
            CreateMap<Invoice, CustomerHistory>()
                .ForMember(dest => dest.RecievedAmount, act => act.MapFrom(src => src.AmountPaid))
                .ForMember(dest => dest.ReceivableAmount, act => act.MapFrom(src => src.Due))
                .ForMember(dest => dest.InvoiceTotalAfterDiscount, act => act.MapFrom(src => src.AmountAfterDiscount))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.DateOfOcurrance))
                .ForMember(dest => dest.Type, act => act.MapFrom(src => "InvoiceItem"))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Sales"))
                .ForAllOtherMembers(act => act.Ignore());

            //-----------------------------------Income-------------------------------------
            CreateMap<Income, CustomerHistory>()
                .ForMember(dest => dest.RecievedAmount, act => act.MapFrom(src => src.Amount))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.OccurranceDate))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.Type, act => act.MapFrom(src => "IncomeItem"))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                .ForAllOtherMembers(act => act.Ignore());

            //-----------------------------------Expense-------------------------------------
            CreateMap<Expense, CustomerHistory>()
                .ForMember(dest => dest.PaidAmount, act => act.MapFrom(src => src.Amount))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.OccurranceDate))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.Type, act => act.MapFrom(src => "ExpenseItem"))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                .ForAllOtherMembers(act => act.Ignore());

            //----------------------------Receivable-------------------------------
            CreateMap<Receivable, CustomerHistory>()
                 .ForMember(dest => dest.ReceivableAmount, act => act.MapFrom(src => src.Amount))
                 .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.CreatedDateTime))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.ClientId))
                 .ForMember(dest => dest.Type, act => act.MapFrom(src => "ReceivableItem"))
                  .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                 .ForAllOtherMembers(act => act.Ignore());

            //----------------------------Payable-------------------------------
            CreateMap<Payable, CustomerHistory>()
                 .ForMember(dest => dest.PayableAmount, act => act.MapFrom(src => src.Amount))
                 .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.CreatedDateTime))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.ClientId))
                 .ForMember(dest => dest.Type, act => act.MapFrom(src => "PayableItem"))
                  .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                 .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_SupplierHistory()
        {
            //-----------------------------------Purchase-------------------------------------
            CreateMap<Purchase, SupplierHistory>()
                .ForMember(dest => dest.CategoryId, act => act.MapFrom(src => src.ItemCategoryId))
                .ForMember(dest => dest.CategoryName, act => act.MapFrom(src => src.Item.ItemCategory.Name))
                .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.ItemName, act => act.MapFrom(src => src.Item.Name))
                .ForMember(dest => dest.UnitPrice, act => act.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.OccurranceDate))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.Type, act => act.MapFrom(src => "PurchaseItem"))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Purchase"))
                .ForAllOtherMembers(act => act.Ignore());

            //-----------------------------------Invoice-------------------------------------
            CreateMap<Invoice, SupplierHistory>()
                .ForMember(dest => dest.PaidAmount, act => act.MapFrom(src => src.AmountPaid))
                .ForMember(dest => dest.PayableAmount, act => act.MapFrom(src => src.Due))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.InvoiceTotalAfterDiscount, act => act.MapFrom(src => src.AmountAfterDiscount))
                .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.DateOfOcurrance))
                .ForMember(dest => dest.Type, act => act.MapFrom(src => "InvoiceItem"))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Purchase"))
                .ForAllOtherMembers(act => act.Ignore());

            //-----------------------------------Expense-------------------------------------
            CreateMap<Expense, SupplierHistory>()
                .ForMember(dest => dest.PaidAmount, act => act.MapFrom(src => src.Amount))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.OccurranceDate))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.Type, act => act.MapFrom(src => "ExpenseItem"))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                .ForAllOtherMembers(act => act.Ignore());

            //-----------------------------------Income-------------------------------------
            CreateMap<Income, SupplierHistory>()
                .ForMember(dest => dest.RecievedAmount, act => act.MapFrom(src => src.Amount))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.OccurranceDate))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.Type, act => act.MapFrom(src => "IncomeItem"))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                .ForAllOtherMembers(act => act.Ignore());

            //----------------------------Receivable-------------------------------
            CreateMap<Receivable, SupplierHistory>()
                 .ForMember(dest => dest.ReceivableAmount, act => act.MapFrom(src => src.Amount))
                 .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.CreatedDateTime))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.ClientId))
                 .ForMember(dest => dest.Type, act => act.MapFrom(src => "ReceivableItem"))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                 .ForAllOtherMembers(act => act.Ignore());

            //----------------------------Payable-------------------------------
            CreateMap<Payable, SupplierHistory>()
                 .ForMember(dest => dest.PayableAmount, act => act.MapFrom(src => src.Amount))
                 .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.CreatedDateTime))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.ClientId))
                 .ForMember(dest => dest.Type, act => act.MapFrom(src => "PayableItem"))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                 .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_Production()
        {
            //-----------------------------------Production-------------------------------------

            CreateMap<AddProductionVM, Production>()
                  .ForMember(dest => dest.EntryDate, act => act.MapFrom(src => DateTime.Now))
                  .ForMember(dest => dest.EquipmentId, act => act.MapFrom(src => src.EquipmentId))
                  .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                  .ForMember(dest => dest.ItemCategoryId, act => act.MapFrom(src => src.ItemCategoryId))
                  .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.ItemId))
                   .ForMember(dest => dest.ItemStatusId, act => act.MapFrom(src => src.ItemStatusId))
                  .ForMember(dest => dest.ProductionId, act => act.MapFrom(src => Guid.NewGuid()))
                  .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                  .ForMember(dest => dest.StaffId, act => act.MapFrom(src => src.StaffId))
                  .ForMember(dest => dest.TotalAmount, act => act.MapFrom(src => (src.Quantity * src.UnitPrice)))
                  .ForMember(dest => dest.UnitPrice, act => act.MapFrom(src => src.UnitPrice))
                  .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                  .ForAllOtherMembers(act => act.Ignore());

            //-----------------------------------Production-------------------------------------

            CreateMap<EditProductionVM, Production>()
                  .ForMember(dest => dest.EquipmentId, act => act.MapFrom(src => src.EquipmentId))
                  .ForMember(dest => dest.ItemCategoryId, act => act.MapFrom(src => src.ItemCategoryId))
                  .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.ItemId))
                  .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                  .ForMember(dest => dest.StaffId, act => act.MapFrom(src => src.StaffId))
                  .ForMember(dest => dest.TotalAmount, act => act.MapFrom(src => (src.Quantity * src.UnitPrice)))
                  .ForMember(dest => dest.UnitPrice, act => act.MapFrom(src => src.UnitPrice))
                  .ForAllOtherMembers(act => act.Ignore());

            //------------------------------GET-------------------------------------------------
            CreateMap<Production, AddProductionVM>()
                    .ForMember(dest => dest.EntryDate, act => act.MapFrom(src => src.EntryDate))
                    .ForMember(dest => dest.EquipmentId, act => act.MapFrom(src => src.EquipmentId))
                    .ForMember(dest => dest.EquipmentName, act => act.MapFrom(src => src.Equipment.Name))
                    .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                    .ForMember(dest => dest.ItemCategoryId, act => act.MapFrom(src => src.ItemCategoryId))
                    .ForMember(dest => dest.ItemCategoryName, act => act.MapFrom(src => src.ItemCategory.Name))
                    .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.ItemId))
                    .ForMember(dest => dest.ItemName, act => act.MapFrom(src => src.Item.Name))
                    .ForMember(dest => dest.ItemStatusName, act => act.MapFrom(src => src.ItemStatus.Name))
                    .ForMember(dest => dest.ItemStatusId, act => act.MapFrom(src => src.ItemStatus.Id))
                    .ForMember(dest => dest.ProductionId, act => act.MapFrom(src => src.ProductionId))
                    .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                    .ForMember(dest => dest.StaffId, act => act.MapFrom(src => src.StaffId))
                    .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                    .ForMember(dest => dest.StaffName, act => act.MapFrom(src => src.Staff.Name))
                    .ForMember(dest => dest.TotalAmount, act => act.MapFrom(src => (src.Quantity * src.UnitPrice)))
                    .ForMember(dest => dest.UnitPrice, act => act.MapFrom(src => src.UnitPrice))
                    .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                    .ForMember(dest => dest.UpdatedDateTime, act => act.MapFrom(src => src.UpdatedDateTime))
                    .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------------StockIn-------------------------------------
            // Addition
            // InvoiceId
            CreateMap<AddProductionVM, StockIn>()
                 .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                  .ForMember(dest => dest.ItemStatusId, act => act.MapFrom(src => src.ItemStatusId))
                  .ForMember(dest => dest.BatchNumber, act => act.MapFrom(src => Guid.NewGuid()))
                  .ForMember(dest => dest.ExpiryDate, act => act.MapFrom(src => DateTime.Now.AddYears(1)))
                  .ForMember(dest => dest.AddedDateTime, act => act.MapFrom(src => DateTime.Now))
                  .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.ItemId))
                  .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                  .ForMember(dest => dest.Unitprice, act => act.MapFrom(src => src.UnitPrice))
                  .ForMember(dest => dest.SupplierId, act => act.MapFrom(src => src.StaffId))
                  .ForMember(dest => dest.Source, act => act.MapFrom(src => StockInSource.Production.ToString()))
                  .ForMember(dest => dest.ExecutorId, act => act.MapFrom(src => src.ExecutorId))
                  .ForAllOtherMembers(act => act.Ignore());

            //----------------------------------Payable-------------------------------------
            // Addition
            // InvoiceId, 
            CreateMap<AddProductionVM, Payable>()
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => (src.UnitPrice * src.Quantity)))
                .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.StaffId))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Staff Payment Due"))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => " to " + src.StaffName))
                //.ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------------Stock-------------------------------------
            // Addition

            CreateMap<AddProductionVM, Stock>()
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.ItemStatusId, act => act.MapFrom(src => src.ItemStatusId))
                .ForMember(dest => dest.ExpiryDate, act => act.MapFrom(src => DateTime.Now.AddYears(1)))
                .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------Invoice-------------------------------------
            // Addition
            // InvoiceId 
            CreateMap<AddProductionVM, Invoice>()
               .ForMember(dest => dest.AmountAfterDiscount, act => act.MapFrom(src => (src.UnitPrice * src.Quantity)))
               .ForMember(dest => dest.AmountBeforeDiscount, act => act.MapFrom(src => (src.UnitPrice * src.Quantity)))
               .ForMember(dest => dest.AmountPaid, act => act.MapFrom(src => 0))
               .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.StaffId))
               .ForMember(dest => dest.Discount, act => act.MapFrom(src => 0))
               .ForMember(dest => dest.Due, act => act.MapFrom(src => (src.UnitPrice * src.Quantity)))
               //.ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.))
               .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
               .ForMember(dest => dest.InvoiceTypeId, act => act.MapFrom(src => src.InvoiceTypeId))
               .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.IsFullyPaid, act => act.MapFrom(src => false))
               .ForMember(dest => dest.DateOfOcurrance, act => act.MapFrom(src => DateTime.Now))
               .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_StaffHistory()
        {
            //-----------------------------------Purchase-------------------------------------
            CreateMap<Production, StaffHistory>()
                .ForMember(dest => dest.CategoryId, act => act.MapFrom(src => src.ItemCategoryId))
                .ForMember(dest => dest.CategoryName, act => act.MapFrom(src => src.Item.ItemCategory.Name))
                .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.ItemId))

                .ForMember(dest => dest.ItemName, act => act.MapFrom(src => src.Item.Name))
                .ForMember(dest => dest.UnitPrice, act => act.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.EntryDate))
                //.ForMember(dest => dest., act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.StaffId, act => act.MapFrom(src => src.StaffId))
                .ForMember(dest => dest.Type, act => act.MapFrom(src => "ProductionItem"))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Production"))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForAllOtherMembers(act => act.Ignore());

            //-----------------------------------Invoice-------------------------------------
            CreateMap<Invoice, StaffHistory>()
                .ForMember(dest => dest.PaidAmount, act => act.MapFrom(src => src.AmountPaid))
                .ForMember(dest => dest.PayableAmount, act => act.MapFrom(src => src.Due))
                .ForMember(dest => dest.InvoiceTotalAfterDiscount, act => act.MapFrom(src => src.AmountAfterDiscount))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.StaffId, act => act.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.DateOfOcurrance))
                .ForMember(dest => dest.Type, act => act.MapFrom(src => "InvoiceItem"))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Production"))
                .ForAllOtherMembers(act => act.Ignore());

            //-----------------------------------Expense-------------------------------------
            CreateMap<Expense, StaffHistory>()
                .ForMember(dest => dest.PaidAmount, act => act.MapFrom(src => src.Amount))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.OccurranceDate))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.StaffId, act => act.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.Type, act => act.MapFrom(src => "ExpenseItem"))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                .ForAllOtherMembers(act => act.Ignore());

            //-----------------------------------Income-------------------------------------
            CreateMap<Income, StaffHistory>()
                .ForMember(dest => dest.RecievedAmount, act => act.MapFrom(src => src.Amount))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.OccurranceDate))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.StaffId, act => act.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.Type, act => act.MapFrom(src => "IncomeItem"))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                .ForAllOtherMembers(act => act.Ignore());

            //----------------------------Receivable-------------------------------
            CreateMap<Receivable, StaffHistory>()
                 .ForMember(dest => dest.ReceivableAmount, act => act.MapFrom(src => src.Amount))
                 .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.CreatedDateTime))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.StaffId, act => act.MapFrom(src => src.ClientId))
                 .ForMember(dest => dest.Type, act => act.MapFrom(src => "ReceivableItem"))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                 .ForAllOtherMembers(act => act.Ignore());

            //----------------------------Payable-------------------------------
            CreateMap<Payable, StaffHistory>()
                 .ForMember(dest => dest.PayableAmount, act => act.MapFrom(src => src.Amount))
                 .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.CreatedDateTime))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.StaffId, act => act.MapFrom(src => src.ClientId))
                 .ForMember(dest => dest.Type, act => act.MapFrom(src => "PayableItem"))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                 .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_SalesReturn()
        {
            // StockIn
            //-------------------------------------StockIn-------------------------------------
            // Addition
            // InvoiceId
            CreateMap<SalesReturnVM, StockIn>()
                 .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                  .ForMember(dest => dest.ItemStatusId, act => act.MapFrom(src => src.ItemStatusId))
                  .ForMember(dest => dest.BatchNumber, act => act.MapFrom(src => Guid.NewGuid()))
                  .ForMember(dest => dest.ExpiryDate, act => act.MapFrom(src => DateTime.Now.AddYears(1)))
                  .ForMember(dest => dest.AddedDateTime, act => act.MapFrom(src => DateTime.Now))
                  .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.ItemId))
                  .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                  .ForMember(dest => dest.Unitprice, act => act.MapFrom(src => src.UnitPrice))
                  .ForMember(dest => dest.SupplierId, act => act.MapFrom(src => src.CustomerId))
                  .ForMember(dest => dest.Source, act => act.MapFrom(src => StockInSource.Production.ToString()))
                  .ForMember(dest => dest.ExecutorId, act => act.MapFrom(src => src.EmployeeId))
                  .ForAllOtherMembers(act => act.Ignore());

            // Invoice
            //-------------------------------Invoice-------------------------------------
            // Addition
            // InvoiceId 
            CreateMap<SalesReturnVM, Invoice>()
               .ForMember(dest => dest.AmountAfterDiscount, act => act.MapFrom(src => src.TotalAmount))
               .ForMember(dest => dest.AmountBeforeDiscount, act => act.MapFrom(src => src.TotalAmount))
               .ForMember(dest => dest.AmountPaid, act => act.MapFrom(src => src.AmountPaid))
               .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.CustomerId))
               .ForMember(dest => dest.Discount, act => act.MapFrom(src => 0))
               .ForMember(dest => dest.Due, act => act.MapFrom(src => (src.TotalAmount - src.AmountPaid)))
               .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
               .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
               .ForMember(dest => dest.InvoiceTypeId, act => act.MapFrom(src => src.InvoiceTypeId))
               .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.IsFullyPaid, act => act.MapFrom(src => false))
               .ForMember(dest => dest.DateOfOcurrance, act => act.MapFrom(src => DateTime.Now))
               .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------Invoice-------------------------------------
            // Addition
            // InvoiceId 
            CreateMap<Invoice, SalesReturnVM>()
               .ForMember(dest => dest.TotalAmount, act => act.MapFrom(src => src.AmountAfterDiscount))
               .ForMember(dest => dest.TotalAmount, act => act.MapFrom(src => src.AmountBeforeDiscount))
               .ForMember(dest => dest.AmountPaid, act => act.MapFrom(src => src.AmountPaid))
               .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.ClientId))
               .ForMember(dest => dest.CustomerName, act => act.MapFrom(src => src.Customer.Name))
               .ForMember(dest => dest.InvoiceTypeName, act => act.MapFrom(src => src.InvoiceType.Name))
               //.ForMember(dest => dest.Discount, act => act.MapFrom(src => 0))
               //.ForMember(dest => dest.Due, act => act.MapFrom(src => (src.TotalAmount - src.AmountPaid)))
               .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
               .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
               .ForMember(dest => dest.InvoiceTypeId, act => act.MapFrom(src => src.InvoiceTypeId))
               .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.Id))
               //.ForMember(dest => dest.IsFullyPaid, act => act.MapFrom(src => false))
               .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.DateOfOcurrance))
               .ForAllOtherMembers(act => act.Ignore());
            // StockIn
            //-------------------------------------StockIn-------------------------------------
            CreateMap<StockIn, SalesReturnVM>()
                  //.ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                  //.ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                  .ForMember(dest => dest.ItemStatusId, act => act.MapFrom(src => src.ItemStatusId))
                  //.ForMember(dest => dest.BatchNumber, act => act.MapFrom(src => Guid.NewGuid()))
                  //.ForMember(dest => dest.ExpiryDate, act => act.MapFrom(src => DateTime.Now.AddYears(1)))
                  //.ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.AddedDateTime))
                  .ForMember(dest => dest.ItemCategoryName, act => act.MapFrom(src => src.Item.ItemCategory.Name))
                  .ForMember(dest => dest.ItemName, act => act.MapFrom(src => src.Item.Name))
                  .ForMember(dest => dest.ItemCategoryId, act => act.MapFrom(src => src.Item.ItemCategory.Id))
                  .ForMember(dest => dest.ItemStatusName, act => act.MapFrom(src => src.ItemStatus.Name))
                  .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.ItemId))
                  .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                  .ForMember(dest => dest.UnitPrice, act => act.MapFrom(src => src.Unitprice))
                  //.ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.SupplierId))
                  //.ForMember(dest => dest.Source, act => act.MapFrom(src => StockInSource.Production.ToString()))
                  //.ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.ExecutorId))
                  .ForAllOtherMembers(act => act.Ignore());

            //------------------------------Expense-------------------------------------
            // Addition
            // InvoiceId 
            CreateMap<SalesReturnVM, Expense>()
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.AmountPaid))
                .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.ExpenseTypeId, act => act.MapFrom(src => src.ExpenseTypeId))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Sales Return"))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => " Paid to " + src.CustomerName))

                .ForAllOtherMembers(act => act.Ignore());

            //----------------------------------Payable-------------------------------------
            CreateMap<Payable, SalesReturnVM>()
                  .ForMember(dest => dest.AmountPayable, act => act.MapFrom(src => src.Amount))
                  .ForAllOtherMembers(act => act.Ignore());

            // Payable
            //----------------------------------Payable-------------------------------------
            // Addition
            // InvoiceId, 
            CreateMap<SalesReturnVM, Payable>()
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.TotalAmount - src.AmountPaid))
                .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Sales Return Due"))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => " to " + src.CustomerName))

                .ForAllOtherMembers(act => act.Ignore());

            // Stock
            //-------------------------------------Stock-------------------------------------
            // Addition

            CreateMap<SalesReturnVM, Stock>()
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.ItemStatusId, act => act.MapFrom(src => src.ItemStatusId))
                .ForMember(dest => dest.ExpiryDate, act => act.MapFrom(src => DateTime.Now.AddYears(1)))
                .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------------Transaction-------------------------------------
            // PaymentStatus ,TransactionType
            CreateMap<SalesReturnVM, TblTransaction>()
                 .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                 .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.CustomerId))
                 .ForMember(dest => dest.ExecutorId, act => act.MapFrom(src => src.EmployeeId))
                 .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                 .ForMember(dest => dest.TransactionId, act => act.MapFrom(src => Guid.NewGuid()))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.AmountPaid))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Sales Return"))
                 .ForMember(dest => dest.TransactionType, act => act.MapFrom(src => TRANSACTION_TYPE.DEBIT))
                 .ForMember(dest => dest.Description, act => act.MapFrom(src => "Paid to " + src.CustomerName))
                 .ForMember(dest => dest.PaymentStatus, act => act.MapFrom(src => "NA"))
                 .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_PurchaseReturn()
        {
            // Invoice
            //-------------------------------Invoice-------------------------------------
            CreateMap<PurchaseReturnVM, Invoice>()
               .ForMember(dest => dest.AmountAfterDiscount, act => act.MapFrom(src => src.TotalAmount))
               .ForMember(dest => dest.AmountBeforeDiscount, act => act.MapFrom(src => src.TotalAmount))
               .ForMember(dest => dest.AmountPaid, act => act.MapFrom(src => src.AmountRecieved))
               .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.SupplierId))
               .ForMember(dest => dest.Discount, act => act.MapFrom(src => 0))
               .ForMember(dest => dest.Due, act => act.MapFrom(src => (src.TotalAmount - src.AmountRecieved)))
               .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
               .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
               .ForMember(dest => dest.InvoiceTypeId, act => act.MapFrom(src => src.InvoiceTypeId))
               .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.IsFullyPaid, act => act.MapFrom(src => false))
               .ForMember(dest => dest.DateOfOcurrance, act => act.MapFrom(src => DateTime.Now))
               .ForAllOtherMembers(act => act.Ignore());

            // StockOut
            //-------------------------------------StockOut-------------------------------------
            // Addition
            // InvoiceId
            CreateMap<PurchaseReturnVM, StockOut>()
                 .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                  .ForMember(dest => dest.ItemStatusId, act => act.MapFrom(src => src.ItemStatusId))
                  .ForMember(dest => dest.BatchNumber, act => act.MapFrom(src => Guid.NewGuid()))
                  .ForMember(dest => dest.ExpiryDate, act => act.MapFrom(src => DateTime.Now.AddYears(1)))
                  .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.ItemId))
                  .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                  .ForMember(dest => dest.Unitprice, act => act.MapFrom(src => src.UnitPrice))
                  .ForMember(dest => dest.BuyerId, act => act.MapFrom(src => src.SupplierId))
                  .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                  .ForMember(dest => dest.ExecutorId, act => act.MapFrom(src => src.EmployeeId))
                  .ForMember(dest => dest.CurrentDateTime, act => act.MapFrom(src => DateTime.Now))
                  .ForAllOtherMembers(act => act.Ignore());

            //------------------------------Income-------------------------------------
            // Addition
            // InvoiceId 
            CreateMap<PurchaseReturnVM, Income>()
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.AmountRecieved))
                .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.SupplierId))
                .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.IncomeTypeId, act => act.MapFrom(src => src.IncomeTypeId))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Purchase Return"))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => " from  " + src.SupplierName))
                .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------REVERSE Invoice-------------------------------------
            // Addition
            // InvoiceId 
            CreateMap<Invoice, PurchaseReturnVM>()
               .ForMember(dest => dest.TotalAmount, act => act.MapFrom(src => src.AmountAfterDiscount))
               .ForMember(dest => dest.TotalAmount, act => act.MapFrom(src => src.AmountBeforeDiscount))
               .ForMember(dest => dest.AmountRecieved, act => act.MapFrom(src => src.AmountPaid))
               .ForMember(dest => dest.SupplierId, act => act.MapFrom(src => src.ClientId))
               .ForMember(dest => dest.SupplierName, act => act.MapFrom(src => src.Supplier.Name))
               .ForMember(dest => dest.InvoiceTypeName, act => act.MapFrom(src => src.InvoiceType.Name))
               //.ForMember(dest => dest.Discount, act => act.MapFrom(src => 0))
               //.ForMember(dest => dest.Due, act => act.MapFrom(src => (src.TotalAmount - src.AmountPaid)))
               .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
               .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
               .ForMember(dest => dest.InvoiceTypeId, act => act.MapFrom(src => src.InvoiceTypeId))
               .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.Id))
               //.ForMember(dest => dest.IsFullyPaid, act => act.MapFrom(src => false))
               .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.DateOfOcurrance))
               .ForAllOtherMembers(act => act.Ignore());
            // StockIn
            //-------------------------------------StockOut-------------------------------------
            CreateMap<StockOut, PurchaseReturnVM>()
                  //.ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                  //.ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                  .ForMember(dest => dest.ItemStatusId, act => act.MapFrom(src => src.ItemStatusId))
                  //.ForMember(dest => dest.BatchNumber, act => act.MapFrom(src => Guid.NewGuid()))
                  //.ForMember(dest => dest.ExpiryDate, act => act.MapFrom(src => DateTime.Now.AddYears(1)))
                  //.ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.AddedDateTime))
                  .ForMember(dest => dest.ItemCategoryName, act => act.MapFrom(src => src.Item.ItemCategory.Name))
                  .ForMember(dest => dest.ItemName, act => act.MapFrom(src => src.Item.Name))
                  .ForMember(dest => dest.ItemCategoryId, act => act.MapFrom(src => src.Item.ItemCategory.Id))
                  .ForMember(dest => dest.ItemStatusName, act => act.MapFrom(src => src.ItemStatus.Name))
                  .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.ItemId))
                  .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                  .ForMember(dest => dest.UnitPrice, act => act.MapFrom(src => src.Unitprice))
                  //.ForMember(dest => dest.SupplierId, act => act.MapFrom(src => src.BuyerId))
                  //.ForMember(dest => dest.Source, act => act.MapFrom(src => StockInSource.Production.ToString()))
                  //.ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.ExecutorId))
                  .ForAllOtherMembers(act => act.Ignore());

            //--------------------------------Receivable-------------------------------------
            CreateMap<Receivable, PurchaseReturnVM>()
                  .ForMember(dest => dest.AmountReceivable, act => act.MapFrom(src => src.Amount))
                  .ForAllOtherMembers(act => act.Ignore());

            // Receivable
            //----------------------------------Receivable-------------------------------------
            CreateMap<PurchaseReturnVM, Receivable>()
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.TotalAmount - src.AmountRecieved))
                .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.SupplierId))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
               .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Purchase Return"))
               .ForMember(dest => dest.Description, act => act.MapFrom(src => " from " + src.SupplierName))
                .ForAllOtherMembers(act => act.Ignore());

            // Stock
            //-------------------------------------Stock-------------------------------------
            CreateMap<PurchaseReturnVM, Stock>()
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.ItemId, act => act.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.ItemStatusId, act => act.MapFrom(src => src.ItemStatusId))
                .ForMember(dest => dest.ExpiryDate, act => act.MapFrom(src => DateTime.Now.AddYears(1)))
                .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------------Transaction-------------------------------------
            // PaymentStatus ,TransactionType
            CreateMap<PurchaseReturnVM, TblTransaction>()
                 .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                 .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.SupplierId))
                 .ForMember(dest => dest.ExecutorId, act => act.MapFrom(src => src.EmployeeId))
                 .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                 .ForMember(dest => dest.TransactionId, act => act.MapFrom(src => Guid.NewGuid()))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.AmountRecieved))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => "Purchase Return"))
                 .ForMember(dest => dest.TransactionType, act => act.MapFrom(src => TRANSACTION_TYPE.CREDIT))
                 .ForMember(dest => dest.Description, act => act.MapFrom(src => "Recieved from " + src.SupplierName))
                 .ForMember(dest => dest.PaymentStatus, act => act.MapFrom(src => "NA"))
                 .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_MonthlyReport()
        {
            //----------------------------MonthlyProduction-------------------------------
            CreateMap<Production, MonthlyProduction>()
               .ForMember(dest => dest.CreatedDateTime, act => act.MapFrom(src => src.EntryDate))
               .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
               .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
               .ForMember(dest => dest.ItemCategoryName, act => act.MapFrom(src => src.ItemCategory.Name))
               .ForMember(dest => dest.ItemName, act => act.MapFrom(src => src.Item.Name))
               .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
               .ForMember(dest => dest.StaffName, act => act.MapFrom(src => src.Staff.Name))
               .ForMember(dest => dest.TotalAmount, act => act.MapFrom(src => src.TotalAmount))
               .ForMember(dest => dest.Unitprice, act => act.MapFrom(src => src.UnitPrice))
               .ForMember(dest => dest.Month, act => act.MapFrom(src => src.EntryDate.ToString(MonthFormat.MMMM.ToString())))
               .ForAllOtherMembers(act => act.Ignore());

            //----------------------------MonthlyReceivable-------------------------------
            CreateMap<Receivable, MonthlyReceivable>()
                 .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount))
                  .ForMember(dest => dest.ClientName, act => act.MapFrom(src => src.Supplier == null ? src.Customer == null ? src.Staff == null ? "" : src.Staff.Name : src.Customer.Name : src.Supplier.Name))
                 .ForMember(dest => dest.CreatedDateTime, act => act.MapFrom(src => src.CreatedDateTime))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.Month, act => act.MapFrom(src => src.CreatedDateTime.ToString(MonthFormat.MMMM.ToString())))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                 .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                 .ForAllOtherMembers(act => act.Ignore());

            //----------------------------MonthlyPayable-------------------------------
            CreateMap<Payable, MonthlyPayable>()
                 .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount))
                 .ForMember(dest => dest.ClientName, act => act.MapFrom(src => src.Supplier == null ? src.Customer == null ? src.Staff == null ? "" : src.Staff.Name : src.Customer.Name : src.Supplier.Name))
                 .ForMember(dest => dest.CreatedDateTime, act => act.MapFrom(src => src.CreatedDateTime))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.Month, act => act.MapFrom(src => src.CreatedDateTime.ToString(MonthFormat.MMMM.ToString())))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                 .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                 .ForAllOtherMembers(act => act.Ignore());

            //-------------------------MonthlyExpense----------------------------------
            CreateMap<Expense, MonthlyExpense>()
                 .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount))
                 .ForMember(dest => dest.ClientName, act => act.MapFrom(src => src.Supplier == null ? src.Customer == null ? src.Staff == null ? "" : src.Staff.Name : src.Customer.Name : src.Supplier.Name))
                 .ForMember(dest => dest.CreatedDateTime, act => act.MapFrom(src => src.OccurranceDate))
                 .ForMember(dest => dest.ExpenseTypeId, act => act.MapFrom(src => src.ExpenseTypeId))
                 .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.Month, act => act.MapFrom(src => src.Month))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                 .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                 .ForAllOtherMembers(act => act.Ignore());

            //-------------------------MonthlyIncome----------------------------------
            CreateMap<Income, MonthlyIncome>()
                 .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount))
                .ForMember(dest => dest.ClientName, act => act.MapFrom(src => src.Supplier == null ? src.Customer == null ? src.Staff == null ? "" : src.Staff.Name : src.Customer.Name : src.Supplier.Name))
                 .ForMember(dest => dest.CreatedDateTime, act => act.MapFrom(src => src.OccurranceDate))
                 .ForMember(dest => dest.IncomeTypeId, act => act.MapFrom(src => src.IncomeTypeId))
                 .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.Month, act => act.MapFrom(src => src.Month))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                 .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                 .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------------Transaction-------------------------------------
            // PaymentStatus ,TransactionType
            CreateMap<TblTransaction, MonthlyTransaction>()
                .ForMember(dest => dest.TransactionId, act => act.MapFrom(src => src.TransactionId))
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                 .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.ClientId))
                 .ForMember(dest => dest.ClientName, act => act.MapFrom(src => src.Supplier == null ? src.Customer == null ? src.Staff == null ? "" : src.Staff.Name : src.Customer.Name : src.Supplier.Name))
                 .ForMember(dest => dest.ExecutorId, act => act.MapFrom(src => src.ExecutorId))
                 .ForMember(dest => dest.Month, act => act.MapFrom(src => src.CreatedDateTime.ToString(MonthFormat.MMMM.ToString())))
                 .ForMember(dest => dest.TransactionId, act => act.MapFrom(src => src.TransactionId))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                 .ForMember(dest => dest.TransactionType, act => act.MapFrom(src => src.TransactionType))
                 .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                 .ForMember(dest => dest.CreatedDateTime, act => act.MapFrom(src => src.CreatedDateTime))
                 .ForAllOtherMembers(act => act.Ignore());

            //----------------------------------Payable-------------------------------------
            //// Addition
            //// InvoiceId, 
            //CreateMap<Payable, MonthlyPayable>()
            //    .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
            //    .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount))
            //    .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
            //    .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.SupplierVM.Id))
            //    .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
            //    .ForMember(dest => dest.Purpose, act => act.MapFrom(src => PayablePurpose.PurchaseDue.ToString() + " to " + src.SupplierVM.Name))
            //    .ForAllOtherMembers(act => act.Ignore());

            //----------------------------MonthlyReceivable-------------------------------
            CreateMap<Payable, MonthlyPayable>()
                 .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount))
                 .ForMember(dest => dest.ClientName, act => act.MapFrom(src => src.Supplier == null ? src.Customer == null ? src.Staff == null ? "" : src.Staff.Name : src.Customer.Name : src.Supplier.Name))
                 .ForMember(dest => dest.CreatedDateTime, act => act.MapFrom(src => src.CreatedDateTime))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.Month, act => act.MapFrom(src => src.CreatedDateTime.ToString(MonthFormat.MMMM.ToString())))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                 .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                 .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_Income()
        {
            //------------------------------Income-------------------------------------
            // Addition
            // InvoiceId 
            CreateMap<IncomeVM, Income>()
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount))
                .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.IncomeTypeId, act => act.MapFrom(src => src.IncomeTypeId))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                .ForAllOtherMembers(act => act.Ignore());

            //------------------------------Income-------------------------------------
            // Addition
            // InvoiceId 
            CreateMap<Income, IncomeVM>()
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount))
                .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.IncomeTypeId, act => act.MapFrom(src => src.IncomeTypeId))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.Month, act => act.MapFrom(src => src.OccurranceDate.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.OccurranceDate))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.ClientName, act => act.MapFrom(src => src.Supplier == null ? src.Customer == null ? src.Staff == null ? "" : src.Staff.Name : src.Customer.Name : src.Supplier.Name))
                .ForMember(dest => dest.IncomeTypeName, act => act.MapFrom(src => src.IncomeType.Name))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------Invoice-------------------------------------
            // Addition
            // InvoiceId 
            CreateMap<IncomeVM, Invoice>()
               .ForMember(dest => dest.AmountAfterDiscount, act => act.MapFrom(src => src.Amount))
               .ForMember(dest => dest.AmountBeforeDiscount, act => act.MapFrom(src => src.Amount))
                .ForMember(dest => dest.AmountPaid, act => act.MapFrom(src => src.Amount))
               .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.ClientId))
               .ForMember(dest => dest.Discount, act => act.MapFrom(src => 0))
               .ForMember(dest => dest.Due, act => act.MapFrom(src => 0))
               .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
               .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
               .ForMember(dest => dest.InvoiceTypeId, act => act.MapFrom(src => src.InvoiceTypeId))
               .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => Guid.NewGuid()))
               //.ForMember(dest => dest.IsFullyPaid, act => act.MapFrom(src => !(src.DueAmount > 0)))
               .ForMember(dest => dest.DateOfOcurrance, act => act.MapFrom(src => DateTime.Now))
               .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------------Transaction-------------------------------------
            // Addition
            // Amount, InvoiceId , PaymentStatus ,TransactionType
            CreateMap<IncomeVM, TblTransaction>()
                 .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                 .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.ClientId))
                 .ForMember(dest => dest.ExecutorId, act => act.MapFrom(src => src.EmployeeId))
                 .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                 .ForMember(dest => dest.TransactionId, act => act.MapFrom(src => Guid.NewGuid()))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Description))
                 .ForMember(dest => dest.PaymentStatus, act => act.MapFrom(src => "NA"))
                 .ForMember(dest => dest.Description, act => act.MapFrom(src => "Recieved from " + src.ClientName))
                 .ForMember(dest => dest.TransactionType, act => act.MapFrom(src => TRANSACTION_TYPE.DEBIT))
                 .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount))
                 .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_Expense()
        {
            //------------------------------Expense-------------------------------------
            // Addition
            // InvoiceId 
            CreateMap<ExpenseVM, Expense>()
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount))
                .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.ExpenseTypeId, act => act.MapFrom(src => src.ExpenseTypeId))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                .ForAllOtherMembers(act => act.Ignore());

            //------------------------------Expense-------------------------------------
            // Addition
            // InvoiceId 
            CreateMap<Expense, ExpenseVM>()
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount))
                 .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.ExpenseTypeId, act => act.MapFrom(src => src.ExpenseTypeId))
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.OccurranceDate.ToString(MonthFormat.MMMM.ToString())))
                .ForMember(dest => dest.OccurranceDate, act => act.MapFrom(src => src.OccurranceDate))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.ClientName, act => act.MapFrom(src => src.Supplier == null ? src.Customer == null ? src.Staff == null ? "" : src.Staff.Name : src.Customer.Name : src.Supplier.Name))
                .ForMember(dest => dest.ExpenseTypeName, act => act.MapFrom(src => src.ExpenseType.Name))
                .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Purpose))
                .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------Invoice-------------------------------------
            // Addition
            // InvoiceId 
            CreateMap<ExpenseVM, Invoice>()
               .ForMember(dest => dest.AmountAfterDiscount, act => act.MapFrom(src => src.Amount))
               .ForMember(dest => dest.AmountBeforeDiscount, act => act.MapFrom(src => src.Amount))
                .ForMember(dest => dest.AmountPaid, act => act.MapFrom(src => src.Amount))
               .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.ClientId))
               .ForMember(dest => dest.Discount, act => act.MapFrom(src => 0))
               .ForMember(dest => dest.Due, act => act.MapFrom(src => 0))
               .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
               .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
               .ForMember(dest => dest.InvoiceTypeId, act => act.MapFrom(src => src.InvoiceTypeId))
               .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => Guid.NewGuid()))
               //.ForMember(dest => dest.IsFullyPaid, act => act.MapFrom(src => !(src.DueAmount > 0)))
               .ForMember(dest => dest.DateOfOcurrance, act => act.MapFrom(src => DateTime.Now))
               .ForAllOtherMembers(act => act.Ignore());

            //-------------------------------------Transaction-------------------------------------
            // Addition
            // Amount, InvoiceId , PaymentStatus ,TransactionType
            CreateMap<ExpenseVM, TblTransaction>()
                 .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                 .ForMember(dest => dest.ClientId, act => act.MapFrom(src => src.ClientId))
                 .ForMember(dest => dest.ExecutorId, act => act.MapFrom(src => src.EmployeeId))
                 .ForMember(dest => dest.Month, act => act.MapFrom(src => DateTime.Now.ToString(MonthFormat.MMMM.ToString())))
                 .ForMember(dest => dest.TransactionId, act => act.MapFrom(src => Guid.NewGuid()))
                 .ForMember(dest => dest.InvoiceId, act => act.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.TransactionType, act => act.MapFrom(src => TRANSACTION_TYPE.DEBIT))
                 .ForMember(dest => dest.Purpose, act => act.MapFrom(src => src.Description))
                 .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount))
                 .ForMember(dest => dest.PaymentStatus, act => act.MapFrom(src => "NA"))
                 .ForMember(dest => dest.Description, act => act.MapFrom(src => "Paid to " + src.ClientName))
                 .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_Factory()
        {
            CreateMap<Factory, FactoryVM>()
              .ForMember(dest => dest.SubscriptionStart, act => act.MapFrom(src => src.SubscriptionStart))
              .ForMember(dest => dest.SubscriptionEnd, act => act.MapFrom(src => src.SubscriptionEnd))
              .ForMember(dest => dest.LicenseNo, act => act.MapFrom(src => src.LicenseNo))
              .ForMember(dest => dest.RegNo, act => act.MapFrom(src => src.RegNo))
              .ForMember(dest => dest.VatRegNo, act => act.MapFrom(src => src.VatRegNo))
              .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
              .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
              .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
              .ForMember(dest => dest.ImageUrl, act => act.MapFrom(src => src.ImageUrl))
              .ForAllOtherMembers(act => act.Ignore());
            CreateMap<FactoryVM, Factory>()
              .ForMember(dest => dest.SubscriptionStart, act => act.MapFrom(src => src.SubscriptionStart))
              .ForMember(dest => dest.SubscriptionEnd, act => act.MapFrom(src => src.SubscriptionEnd))
              .ForMember(dest => dest.LicenseNo, act => act.MapFrom(src => src.LicenseNo))
              .ForMember(dest => dest.RegNo, act => act.MapFrom(src => src.RegNo))
              .ForMember(dest => dest.VatRegNo, act => act.MapFrom(src => src.VatRegNo))
              .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
              .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
              .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
              .ForMember(dest => dest.ImageUrl, act => act.MapFrom(src => src.ImageUrl))
              .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_PaymentStatus()
        {
            CreateMap<PaymentStatus, PaymentStatusVM>()
                  .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Status))
                  .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                  .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                  .ForAllOtherMembers(act => act.Ignore());
            CreateMap<PaymentStatusVM, PaymentStatus>()
                  .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Name))
                  .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                  .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                  .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_ItemStatus()
        {
            CreateMap<ItemStatus, ItemStatusVM>()
                  .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                  .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                  .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                  .ForAllOtherMembers(act => act.Ignore());
            CreateMap<ItemStatusVM, ItemStatus>()
                  .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                  .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                  .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                  .ForAllOtherMembers(act => act.Ignore());

        }
        public void Mapping_UserAuthInfo()
        {
            CreateMap<UserAuthInfo, UserAuthInfoVM>()
                     .ForMember(dest => dest.UserId, act => act.MapFrom(src => src.UserId))
                     .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.UserName))
                     .ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
                     .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                     .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                     .ForAllOtherMembers(act => act.Ignore());

            CreateMap<UserAuthInfoVM, UserAuthInfo>()
                     .ForMember(dest => dest.UserId, act => act.MapFrom(src => src.UserId))
                     .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.UserName))
                     .ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
                     .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                     .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                     .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_Role()
        {
            CreateMap<RoleVM, Role>()
                     .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                     .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                     .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                     .ForMember(dest => dest.Item, act => act.MapFrom(src => src.Item))
                     .ForMember(dest => dest.ItemCategory, act => act.MapFrom(src => src.ItemCategory))
                     .ForMember(dest => dest.Equipment, act => act.MapFrom(src => src.Equipment))
                     .ForMember(dest => dest.EquipmentCategory, act => act.MapFrom(src => src.EquipmentCategory))
                     .ForMember(dest => dest.Stock, act => act.MapFrom(src => src.Stock))
                     .ForMember(dest => dest.PurchaseReturn, act => act.MapFrom(src => src.PurchaseReturn))
                     .ForMember(dest => dest.SalesReturn, act => act.MapFrom(src => src.SalesReturn))
                     .ForMember(dest => dest.Staff, act => act.MapFrom(src => src.Staff))
                     .ForMember(dest => dest.Customer, act => act.MapFrom(src => src.Customer))
                     .ForMember(dest => dest.Supplier, act => act.MapFrom(src => src.Supplier))
                     .ForMember(dest => dest.StaffHistory, act => act.MapFrom(src => src.StaffHistory))
                     .ForMember(dest => dest.Production, act => act.MapFrom(src => src.Production))
                     .ForMember(dest => dest.CustomerHistory, act => act.MapFrom(src => src.CustomerHistory))
                     .ForMember(dest => dest.SupplierHistory, act => act.MapFrom(src => src.SupplierHistory))
                     .ForMember(dest => dest.MonthlyIncomeReport, act => act.MapFrom(src => src.MonthlyIncomeReport))
                     .ForMember(dest => dest.MonthlyExpenseReport, act => act.MapFrom(src => src.MonthlyExpenseReport))
                     .ForMember(dest => dest.MonthlyPayableReport, act => act.MapFrom(src => src.MonthlyPayableReport))
                     .ForMember(dest => dest.MonthlyReceivableReport, act => act.MapFrom(src => src.MonthlyReceivableReport))
                     .ForMember(dest => dest.MonthlyAccountReport, act => act.MapFrom(src => src.MonthlyAccountReport))
                     .ForMember(dest => dest.MonthlyProductionReport, act => act.MapFrom(src => src.MonthlyProductionReport))
                     .ForMember(dest => dest.StaffPayment, act => act.MapFrom(src => src.StaffPayment))
                     .ForMember(dest => dest.CustomerPayment, act => act.MapFrom(src => src.CustomerPayment))
                     .ForMember(dest => dest.SupplierPayment, act => act.MapFrom(src => src.SupplierPayment))
                     .ForMember(dest => dest.Department, act => act.MapFrom(src => src.Department))
                     .ForMember(dest => dest.Sales, act => act.MapFrom(src => src.Sales))
                     .ForMember(dest => dest.Purchase, act => act.MapFrom(src => src.Purchase))
                     .ForMember(dest => dest.Income, act => act.MapFrom(src => src.Income))
                     .ForMember(dest => dest.Expense, act => act.MapFrom(src => src.Expense))
                     .ForMember(dest => dest.IncomeType, act => act.MapFrom(src => src.IncomeType))
                     .ForMember(dest => dest.ExpenseType, act => act.MapFrom(src => src.ExpenseType))
                     .ForMember(dest => dest.InvoiceType, act => act.MapFrom(src => src.InvoiceType))
                     .ForMember(dest => dest.PaymentStatus, act => act.MapFrom(src => src.PaymentStatus))
                     .ForMember(dest => dest.ItemStatus, act => act.MapFrom(src => src.ItemStatus))
                     .ForMember(dest => dest.Factory, act => act.MapFrom(src => src.Factory))
                     .ForMember(dest => dest.Registration, act => act.MapFrom(src => src.Registration))
                     .ForAllOtherMembers(act => act.Ignore());

            CreateMap<Role, RoleVM>()
                     .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                     .ForMember(dest => dest.FactoryId, act => act.MapFrom(src => src.FactoryId))
                     .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                     .ForMember(dest => dest.Item, act => act.MapFrom(src => src.Item))
                     .ForMember(dest => dest.ItemCategory, act => act.MapFrom(src => src.ItemCategory))
                     .ForMember(dest => dest.Equipment, act => act.MapFrom(src => src.Equipment))
                     .ForMember(dest => dest.EquipmentCategory, act => act.MapFrom(src => src.EquipmentCategory))
                     .ForMember(dest => dest.Stock, act => act.MapFrom(src => src.Stock))
                     .ForMember(dest => dest.PurchaseReturn, act => act.MapFrom(src => src.PurchaseReturn))
                     .ForMember(dest => dest.SalesReturn, act => act.MapFrom(src => src.SalesReturn))
                     .ForMember(dest => dest.Staff, act => act.MapFrom(src => src.Staff))
                     .ForMember(dest => dest.Customer, act => act.MapFrom(src => src.Customer))
                     .ForMember(dest => dest.Supplier, act => act.MapFrom(src => src.Supplier))
                     .ForMember(dest => dest.StaffHistory, act => act.MapFrom(src => src.StaffHistory))
                     .ForMember(dest => dest.Production, act => act.MapFrom(src => src.Production))
                     .ForMember(dest => dest.CustomerHistory, act => act.MapFrom(src => src.CustomerHistory))
                     .ForMember(dest => dest.SupplierHistory, act => act.MapFrom(src => src.SupplierHistory))
                     .ForMember(dest => dest.MonthlyIncomeReport, act => act.MapFrom(src => src.MonthlyIncomeReport))
                     .ForMember(dest => dest.MonthlyExpenseReport, act => act.MapFrom(src => src.MonthlyExpenseReport))
                     .ForMember(dest => dest.MonthlyPayableReport, act => act.MapFrom(src => src.MonthlyPayableReport))
                     .ForMember(dest => dest.MonthlyReceivableReport, act => act.MapFrom(src => src.MonthlyReceivableReport))
                     .ForMember(dest => dest.MonthlyAccountReport, act => act.MapFrom(src => src.MonthlyAccountReport))
                     .ForMember(dest => dest.MonthlyProductionReport, act => act.MapFrom(src => src.MonthlyProductionReport))
                     .ForMember(dest => dest.StaffPayment, act => act.MapFrom(src => src.StaffPayment))
                     .ForMember(dest => dest.CustomerPayment, act => act.MapFrom(src => src.CustomerPayment))
                     .ForMember(dest => dest.SupplierPayment, act => act.MapFrom(src => src.SupplierPayment))
                     .ForMember(dest => dest.Department, act => act.MapFrom(src => src.Department))
                     .ForMember(dest => dest.Sales, act => act.MapFrom(src => src.Sales))
                     .ForMember(dest => dest.Purchase, act => act.MapFrom(src => src.Purchase))
                     .ForMember(dest => dest.Income, act => act.MapFrom(src => src.Income))
                     .ForMember(dest => dest.Expense, act => act.MapFrom(src => src.Expense))
                     .ForMember(dest => dest.IncomeType, act => act.MapFrom(src => src.IncomeType))
                     .ForMember(dest => dest.ExpenseType, act => act.MapFrom(src => src.ExpenseType))
                     .ForMember(dest => dest.InvoiceType, act => act.MapFrom(src => src.InvoiceType))
                     .ForMember(dest => dest.PaymentStatus, act => act.MapFrom(src => src.PaymentStatus))
                     .ForMember(dest => dest.ItemStatus, act => act.MapFrom(src => src.ItemStatus))
                     .ForMember(dest => dest.Factory, act => act.MapFrom(src => src.Factory))
                     .ForMember(dest => dest.Registration, act => act.MapFrom(src => src.Registration))
                     .ForAllOtherMembers(act => act.Ignore());
        }
        public void Mapping_ApiResourceMapping()
        {
            CreateMap<ApiResourceMapping, ApiResourceMappingVM>()
                        .ForMember(dest => dest.Controller, act => act.MapFrom(src => src.Controller))
                        .ForMember(dest => dest.Resource, act => act.MapFrom(src => src.Resource))
                        .ForMember(dest => dest.Action, act => act.MapFrom(src => src.Action))
                        .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                        .ForAllOtherMembers(act => act.Ignore());
            CreateMap<ApiResourceMappingVM, ApiResourceMapping>()
                        .ForMember(dest => dest.Controller, act => act.MapFrom(src => src.Controller))
                        .ForMember(dest => dest.Resource, act => act.MapFrom(src => src.Resource))
                        .ForMember(dest => dest.Action, act => act.MapFrom(src => src.Action))
                        .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                        .ForAllOtherMembers(act => act.Ignore());
        }
    }
}
