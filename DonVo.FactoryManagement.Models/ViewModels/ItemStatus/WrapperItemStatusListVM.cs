using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.ItemStatus
{
    public   class WrapperItemStatusListVM
    {
        public long TotalRecords { get; set; }
        public List<ItemStatusVM> ListOfData { get; set; }
    }
}
