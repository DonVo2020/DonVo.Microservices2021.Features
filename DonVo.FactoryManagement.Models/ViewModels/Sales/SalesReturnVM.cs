using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Sales
{
    public class SalesReturnVM
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; } //--


        public string ItemId { get; set; }
        public string ItemName { get; set; } //--


        public string ItemStatusId { get; set; }
        public string ItemStatusName { get; set; } //--


        public string ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; } //--

        public string ExpenseTypeId { get; set; }

        public long AmountPayable { get; set; } //--
        public long AmountPaid { get; set; } //--
        public long TotalAmount { get; set; } //--
        public long UnitPrice { get; set; } //--
        public long Quantity { get; set; } //--
        public string EmployeeId { get; set; }
        public string FactoryId { get; set; }
        public string InvoiceId { get; set; }
        public string InvoiceTypeId { get; set; }
        public string InvoiceTypeName { get; set; }
        public DateTime OccurranceDate { get; set; } //--

        public bool IsFullyPaid { get; set; }

    
    }
}
