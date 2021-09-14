using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.PaymentStatus
{
    public class WrapperPaymentStatusListVM
    {
        public long TotalRecords { get; set; }
        public List<PaymentStatusVM> ListOfData { get; set; }
    }
}
