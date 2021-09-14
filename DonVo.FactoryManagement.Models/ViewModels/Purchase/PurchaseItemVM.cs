using DonVo.FactoryManagement.Models.ViewModels.Item;
using DonVo.FactoryManagement.Models.ViewModels.ItemCategoryView;
using DonVo.FactoryManagement.Models.ViewModels.ItemStatus;
using DonVo.FactoryManagement.Models.ViewModels.Supplier;
using System;

namespace DonVo.FactoryManagement.Models.ViewModels.Purchase
{
    public class PurchaseItemVM
    {
        public ItemVM Item { get; set;}
        public string ItemName { get; set; }
        public string ItemCategoryName { get; set; }
        public string Status { get; set; }
        public string Month { get; set; }
        public ItemStatusVM ItemStatus { get; set;}
        public ItemCategoryVM ItemCategory { get; set;}
        public int Quantity { get; set;}
        public double UnitPrice { get; set;}
        public string FactoryId { get; set;}
        public DateTime ExpiryDate { get; set; }
        public SupplierVM SupplierVM { get; set;}
        public string EmployeeId { get; set; }
        public string InvoiceId { get; set; }
    }
}
