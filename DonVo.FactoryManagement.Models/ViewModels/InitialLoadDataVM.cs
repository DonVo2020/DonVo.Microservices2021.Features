using DonVo.FactoryManagement.Models.ViewModels.CustomerView;
using DonVo.FactoryManagement.Models.ViewModels.Equipment;
using DonVo.FactoryManagement.Models.ViewModels.ExpenseType;
using DonVo.FactoryManagement.Models.ViewModels.IncomeType;
using DonVo.FactoryManagement.Models.ViewModels.InvoiceType;
using DonVo.FactoryManagement.Models.ViewModels.Item;
using DonVo.FactoryManagement.Models.ViewModels.ItemCategoryView;
using DonVo.FactoryManagement.Models.ViewModels.ItemStatus;
using DonVo.FactoryManagement.Models.ViewModels.Role;
using DonVo.FactoryManagement.Models.ViewModels.Staff;
using DonVo.FactoryManagement.Models.ViewModels.Supplier;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels
{
  public  class InitialLoadDataVM
    {
        public List<ItemVM> ItemVMs { get; set; }
        public List<ItemCategoryVM> ItemCategoryVMs { get; set; }
        public List<CustomerVM> CustomerVMs { get; set; }
        public List<SupplierVM> SupplierVMs { get; set; }
        public List<StaffVM> StaffVMs { get; set; }
        public List<ItemStatusVM> ItemStatusVMs { get; set; }
        public List<ExpenseTypeVM> ExpenseTypeVMs { get; set; }

        public List<IncomeTypeVM> IncomeTypeVMs { get; set; }

        public List<InvoiceTypeVM> InvoiceTypeVMs { get; set; }
        public List<EquipmentVM> EquipmentVMs { get; set; }

        public List<RoleVM> RoleVMs { get; set; }

    }
}
