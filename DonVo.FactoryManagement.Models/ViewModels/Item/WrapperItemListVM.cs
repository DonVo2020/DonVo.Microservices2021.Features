using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.Item
{
    public class WrapperItemListVM
    {
        public long TotalRecords { get; set; }
        public List<ItemVM> ListOfData { get; set; }
    }
}
