using System;
using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.DbModels
{
    public  class Address : BaseEntity
    {

        public string RelatedId { get; set; }
        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }
    }
}
