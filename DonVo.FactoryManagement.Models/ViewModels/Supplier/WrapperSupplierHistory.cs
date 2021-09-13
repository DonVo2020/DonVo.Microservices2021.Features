using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Supplier
{
    public class WrapperSupplierHistory
    {
        public WrapperSupplierHistory() {
            ListOfData = new List<SupplierHistory>();
            TotalRecords = 0;
        }
        public long TotalRecords { get; set; }
        public List<SupplierHistory> ListOfData { get; set; }
    }
}
