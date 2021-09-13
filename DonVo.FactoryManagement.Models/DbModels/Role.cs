using System;
using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.DbModels
{
    public  class Role : BaseEntity
    {
      
        public string Name { get; set; }


        public bool Item { get; set; }
        public bool ItemCategory { get; set; }
        public bool Equipment { get; set; }
        public bool EquipmentCategory { get; set; }
        public bool Stock { get; set; }
        public bool PurchaseReturn { get; set; }
        public bool SalesReturn { get; set; }
        public bool Staff { get; set; }
        public bool Customer { get; set; }
        public bool Supplier { get; set; }
        public bool StaffHistory { get; set; }
        public bool Production { get; set; }
        public bool CustomerHistory { get; set; }
        public bool SupplierHistory { get; set; }
        public bool MonthlyIncomeReport { get; set; }
        public bool MonthlyExpenseReport { get; set; }
        public bool MonthlyPayableReport { get; set; }
        public bool MonthlyReceivableReport { get; set; }
        public bool MonthlyAccountReport { get; set; }
        public bool MonthlyProductionReport { get; set; }
        public bool StaffPayment { get; set; }
        public bool CustomerPayment { get; set; }
        public bool SupplierPayment { get; set; }
        public bool Department { get; set; }
        public bool Sales  { get; set; }
        public bool Purchase { get; set; }
        public bool Income { get; set; }
        public bool Expense { get; set; }
        public bool IncomeType { get; set; }
        public bool ExpenseType { get; set; }
        public bool InvoiceType { get; set; }
        public bool ItemStatus { get; set; }
        public bool PaymentStatus { get; set; }
        public bool Factory { get; set; }

        public bool Registration { get; set; }

    }
}
