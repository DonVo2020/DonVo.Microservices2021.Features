using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Sales
{
    public class WrapperSalesReturnVM
    {
        public WrapperSalesReturnVM()
        {
            this.ListOfData = new List<SalesReturnVM>();

        }
        public List<SalesReturnVM> ListOfData { get; set; }
        public long TotalRecords { get; set; }
    }
}
