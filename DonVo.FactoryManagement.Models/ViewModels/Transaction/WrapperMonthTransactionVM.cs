using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Transaction
{
  public  class WrapperMonthTransactionVM
    {
        public WrapperMonthTransactionVM()
        {
            ListOfData = new List<MonthlyTransaction>();
        }

        public List<MonthlyTransaction> ListOfData { get; set; }

        public decimal TotalTillNow_Debit{ get; set; }
        public decimal TotalTillNow_Credit { get; set; }

        public decimal TotalMonthly_Debit { get; set; }
        public decimal TotalMonthly_Credit { get; set; }


        public long TotalRecords { get; set; }
    }
}
