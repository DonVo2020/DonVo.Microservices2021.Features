using System;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses
{
    public class InventoryItemDetailResponse
    {
        public string Id { get; set; }
        public int ItemCategoryId { get; set; }
        public string Description { get; set; }
        public string Bin { get; set; }
        public int RequestQty { get; set; }
        public int QtyInStock { get; set; }
        public int ReorderLevel { get; set; }
        public int ReorderQty { get; set; }
        public string UOM { get; set; }

        public ItemCategory ItemCategory { get; set; }
    }
}