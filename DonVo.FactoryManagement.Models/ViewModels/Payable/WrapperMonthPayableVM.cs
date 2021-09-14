using System.Collections.Generic;

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
