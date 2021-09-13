using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.ItemStatus
{
  public   class WrapperItemStatusListVM
    {
        public long TotalRecords { get; set; }
        public List<ItemStatusVM> ListOfData { get; set; }

    }
}
