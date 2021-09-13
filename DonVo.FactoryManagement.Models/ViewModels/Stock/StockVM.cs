using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Stock
{
  public  class StockVM
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public long? Quantity { get; set; }
        public string ItemStatus { get; set; }
        public string ItemStatusId { get; set; }
        public DateTime ExpiryDate { get; set; }

        public string FactoryId { get; set; }
        public string Id { get; set; }
    }
}
