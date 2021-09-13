using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.FactoryManagement.Models.DbModels
{
    public  class Item : BaseEntity
    {

        public string Name { get; set; }
        public string CategoryId { get; set; }
        public decimal? UnitPrice { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ItemCategory ItemCategory { get; set; }
    }
}
