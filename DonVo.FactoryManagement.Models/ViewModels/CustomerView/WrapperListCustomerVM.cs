using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.CustomerView
{
   public  class WrapperListCustomerVM
    {
        public long TotalRecords { get; set; }
        public List<CustomerVM> ListOfData { get; set; }

    }
}
