using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.Expense
{
    public class WrapperMonthExpenseVM
    {
        public WrapperMonthExpenseVM()
        {
            ListOfData = new List<MonthlyExpense>();
        }

        public decimal Total_TillNow { get; set; }
        public decimal Total_Monthly { get; set; }
        public List<MonthlyExpense> ListOfData { get; set; }
        public long TotalRecords { get; set; }
    }
}
