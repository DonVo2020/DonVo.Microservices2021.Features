using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Income
{
    public class MonthlyIncome
    {

        public DateTime CreatedDateTime { get; set; }
        public string  FactoryId { get; set; }
        public string InvoiceId { get; set; }
        public string IncomeTypeId { get; set; }
        public string ClientName { get; set; }
        public string Month { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

    }
}
