using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Purchase
{
   public class PurchaseReturnVM
    {
        public string SupplierId { get; set; }
        public string SupplierName { get; set; } //--
        public string ItemId { get; set; }
        public string ItemName { get; set; } //
        public string ItemStatusId { get; set; }
        public string ItemStatusName { get; set; } // 
        public string ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; } //
        public long AmountRecieved { get; set; } // --
        public long AmountReceivable { get; set; } // --
        public long UnitPrice { get; set; } //
        public long TotalAmount { get; set; } //--
        public long Quantity { get; set; } //
        public string EmployeeId { get; set; }
        public string FactoryId { get; set; }
        public string InvoiceId { get; set; }
        public string InvoiceTypeId { get; set; }
        public string InvoiceTypeName { get; set; }// --
        public DateTime OccurranceDate { get; set; }
        public string IncomeTypeId { get; set; }
        public bool IsFullyPaid { get; set; }
    }
}
