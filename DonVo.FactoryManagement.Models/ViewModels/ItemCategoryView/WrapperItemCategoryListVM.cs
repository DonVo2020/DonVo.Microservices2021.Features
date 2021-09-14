using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.ItemCategoryView
{
    public  class WrapperItemCategoryListVM
    {
        public long TotalRecords { get; set; }
        public List<ItemCategoryVM> ListOfData { get; set; }        
    }
}
