using System;

namespace DonVo.FactoryManagement.Models.ViewModels
{
    public class MonthlyReport : GetDataListVM
    {
        public string Month { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
