using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Supplier
{
   public  class WrapperSupplierListVM
    {
        public long TotalRecords { get; set; }
        public List<SupplierVM> ListOfData { get; set; }
    }
}
