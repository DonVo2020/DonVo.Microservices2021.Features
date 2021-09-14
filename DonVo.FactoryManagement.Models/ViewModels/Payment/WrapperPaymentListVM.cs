using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.Payment
{
    public class WrapperPaymentListVM
    {
        public long TotalRecords { get; set; }
        public List<PaymentVM> ListOfData { get; set; }
    }
}
