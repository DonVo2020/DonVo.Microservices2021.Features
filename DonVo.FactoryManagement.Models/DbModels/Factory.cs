using System;
using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.DbModels
{
    public  class Factory : BaseEntity
    {
        public Factory()
        {
           
        }


        public DateTime? SubscriptionStart { get; set; }
        public DateTime? SubscriptionEnd { get; set; }
        public string LicenseNo { get; set; }
        public string RegNo { get; set; }
        public string VatRegNo { get; set; }
        public string Name { get; set; }

        public string ImageUrl { get; set; }

       
    }
}
