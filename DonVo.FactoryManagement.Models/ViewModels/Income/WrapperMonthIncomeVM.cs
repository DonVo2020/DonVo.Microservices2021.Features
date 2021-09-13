using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Income
{
    public class WrapperMonthIncomeVM
    {
        public WrapperMonthIncomeVM()
        {
            ListOfData = new List<MonthlyIncome>();
        }

        public decimal Total_TillNow { get; set; }
        public decimal Total_Monthly { get; set; }
        public List<MonthlyIncome> ListOfData { get; set; }
        public long TotalRecords { get; set; }
    }
}
