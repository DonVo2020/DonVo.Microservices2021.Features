using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.CustomerView
{
    public  class WrapperListCustomerVM
    {
        public long TotalRecords { get; set; }
        public List<CustomerVM> ListOfData { get; set; }
    }
}
