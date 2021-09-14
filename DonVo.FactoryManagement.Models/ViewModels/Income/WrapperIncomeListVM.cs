using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.Income
{
    public  class WrapperIncomeListVM
    {
        public long TotalRecords { get; set; }
        public List<IncomeVM> ListOfData { get; set; }
    }
}
