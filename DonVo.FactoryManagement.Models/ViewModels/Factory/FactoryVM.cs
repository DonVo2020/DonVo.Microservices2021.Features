using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Factory
{
    public class FactoryVM
    {
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string Id { get; set; }
        public string FactoryId { get; set; }
        public DateTime? SubscriptionStart { get; set; }
        public DateTime? SubscriptionEnd { get; set; }
        public string LicenseNo { get; set; }
        public string RegNo { get; set; }
        public string VatRegNo { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
