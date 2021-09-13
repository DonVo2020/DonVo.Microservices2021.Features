using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Sales
{
    public class WrapperSalesListVM : CommonVM
    {
        public long TotalRecords { get; set; }
        public List<SalesVM> ListOfData { get; set; }
    }
}
