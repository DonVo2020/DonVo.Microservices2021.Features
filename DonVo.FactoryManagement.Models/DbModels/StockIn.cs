using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.FactoryManagement.Models.DbModels
{
    public  class StockIn : BaseEntity
    {
      
        public string ItemId { get; set; }
        public string SupplierId { get; set; }
        public string ExecutorId { get; set; }
        public string InvoiceId { get; set; }
        public string BatchNumber { get; set; }
        public long? Quantity { get; set; }
        public decimal? Unitprice { get; set; }
        public string ItemStatusId { get; set; }
        public string Source { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string ProductionId { get; set; }

        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }

        [ForeignKey("ItemStatusId")]
        public virtual ItemStatus ItemStatus { get; set; }

    }
}
