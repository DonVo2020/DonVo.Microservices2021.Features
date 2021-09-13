using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.FactoryManagement.Models.DbModels
{
    public  class Stock : BaseEntity
    {
      
        public string ItemId { get; set; }
        public long? Quantity { get; set; }
        public string ItemStatusId { get; set; }
        public DateTime ExpiryDate { get; set; }


        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }

        [ForeignKey("ItemStatusId")]
        public virtual ItemStatus ItemStatus { get; set; }

    }
}
