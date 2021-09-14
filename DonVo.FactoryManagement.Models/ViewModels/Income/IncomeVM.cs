using System;

namespace DonVo.FactoryManagement.Models.ViewModels.Income
{
    public class IncomeVM
    {
        public string Id { get; set; }
        public string FactoryId { get; set; }
        public string IncomeTypeId { get; set; } // 
        public string IncomeTypeName { get; set; }
        public string ClientId { get; set; } //
        public string ClientName { get; set; }
        public string InvoiceId { get; set; }
        public string InvoiceTypeId { get; set; }
        public string Month { get; set; }
        public decimal? Amount { get; set; } //
        public string Description { get; set; } // 
        public string EmployeeId { get; set; }
        public DateTime OccurranceDate { get; set; }
        public string Purpose { get; set; } // 
    }
}
