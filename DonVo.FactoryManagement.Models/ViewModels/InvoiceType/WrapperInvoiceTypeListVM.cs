using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.InvoiceType
{
    public class WrapperInvoiceTypeListVM
    {
        public long TotalRecords { get; set; }
        public List<InvoiceTypeVM> ListOfData { get; set; }
    }
}
