using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.PaymentStatus
{
   public class WrapperPaymentStatusListVM
    {
        public long TotalRecords { get; set; }
        public List<PaymentStatusVM> ListOfData { get; set; }

    }
}
