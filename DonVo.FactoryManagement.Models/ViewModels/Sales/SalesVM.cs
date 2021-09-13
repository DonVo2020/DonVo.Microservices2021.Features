using DonVo.FactoryManagement.Models.ViewModels.CustomerView;
using DonVo.FactoryManagement.Models.ViewModels.IncomeType;
using DonVo.FactoryManagement.Models.ViewModels.InvoiceType;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Sales
{
   public class SalesVM
    {
        public string CustomerName { get; set; }

        public CustomerVM CustomerVM { get; set; }
        public InvoiceTypeVM InvoiceType { get; set; }
        public IncomeTypeVM IncomeType { get; set; }
        public DateTime OcurranceDate { get; set; }
        public long TotalAmount { get; set; }
        public long PaidAmount { get; set; }
        public long DueAmount { get; set; }
        public long DiscountAmount { get; set; }
        public List<SalesItemVM> ItemList { get; set; }
        
        
        public string EmployeeId { get; set; }
        public string FactoryId { get; set; }
        public string InvoiceId { get; set; }
        //    public DateTime DeliveryDate { get; set; }
        //    public DateTime OrderIssued { get; set; }
        //    public long DiscountPercentage { get; set; }
        //    public long TaxAmount { get; set; }
        //    public long DeliveryCost { get; set; }
    }
}
