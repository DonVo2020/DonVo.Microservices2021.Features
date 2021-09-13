using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Item
{
   public class WrapperItemListVM
    {
        public long TotalRecords { get; set; }
        public List<ItemVM> ListOfData { get; set; }

    }
}
