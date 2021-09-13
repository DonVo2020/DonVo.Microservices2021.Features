using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.InvoiceType
{
   public class WrapperInvoiceTypeListVM
    {
        public long TotalRecords { get; set; }
        public List<InvoiceTypeVM> ListOfData { get; set; }
    }
}
