using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.Factory
{
    public class WrapperFactoryListVM
    {
        public long TotalRecords { get; set; }
        public List<FactoryVM> ListOfData { get; set; }
    }
}
