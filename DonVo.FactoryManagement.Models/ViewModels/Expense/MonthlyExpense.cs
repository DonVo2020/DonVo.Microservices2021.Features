﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Expense
{
   public class MonthlyExpense
    {
        public DateTime CreatedDateTime { get; set; }
        public string FactoryId { get; set; }
        public string InvoiceId { get; set; }
        public string ExpenseTypeId { get; set; }
        public string ClientName { get; set; }
        public string Month { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
    }
}
