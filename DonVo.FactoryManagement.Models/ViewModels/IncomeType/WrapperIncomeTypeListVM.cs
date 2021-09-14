using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.IncomeType
{
    public class WrapperIncomeTypeListVM
    {
        public long TotalRecords { get; set; }
        public List<IncomeTypeVM> ListOfData { get; set; }
    }
}
