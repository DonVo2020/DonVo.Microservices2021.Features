using DonVo.FactoryManagement.Models.ViewModels.Receivable;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Payable
{
   public class WrapperMonthPayableListVM
    {
        public WrapperMonthPayableListVM()
        {
            ListOfData = new List<MonthlyPayable>();
        }

        public decimal Total_TillNow { get; set; }
        public decimal Total_Monthly { get; set; }
        public List<MonthlyPayable> ListOfData { get; set; }
        public long TotalRecords { get; set; }
    }
}
