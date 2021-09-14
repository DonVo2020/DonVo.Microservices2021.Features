using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.Production
{
    public class WrapperMonthProductionListVM
    {
        public WrapperMonthProductionListVM()
        {
            ListOfData = new List<MonthlyProduction>();
        }
        public long TotalRecords { get; set; }
        public long Total_TillNow { get; set; }
        public long Total_Monthly { get; set; }
        public List<MonthlyProduction> ListOfData { get; set; }
    }
}
