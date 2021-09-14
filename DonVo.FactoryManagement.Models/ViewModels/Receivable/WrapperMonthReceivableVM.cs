using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.Receivable
{
    public class WrapperMonthReceivableVM
    {
        public WrapperMonthReceivableVM()
        {
            ListOfData = new List<MonthlyReceivable>();
        }

        public decimal Total_TillNow { get; set; }
        public decimal Total_Monthly { get; set; }
        public List<MonthlyReceivable> ListOfData { get; set; }
        public long TotalRecords { get; set; }
    }
}
