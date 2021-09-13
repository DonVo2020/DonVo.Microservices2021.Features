using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Payment
{
    public class WrapperPaymentListVM
    {
        public long TotalRecords { get; set; }
        public List<PaymentVM> ListOfData { get; set; }
    }
}
