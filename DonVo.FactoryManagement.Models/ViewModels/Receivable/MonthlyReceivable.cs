using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Receivable
{
    public class MonthlyReceivable
    {
        public string ClientName { get; set; }
        public string InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public string Month { get; set; }
        public string Purpose { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
