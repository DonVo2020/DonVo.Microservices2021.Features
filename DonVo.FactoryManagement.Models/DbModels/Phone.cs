using System;
using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.DbModels
{
    public  class Phone : BaseEntity
    {
     
        public string RelatedId { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string AlternateNumber_1 { get; set; }
        public string AlternateNumber_2 { get; set; }
        public string AlternateNumber_3 { get; set; }
        public string LandPhone { get; set; }


    }
}
