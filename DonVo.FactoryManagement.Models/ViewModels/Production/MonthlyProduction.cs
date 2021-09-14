using System;

namespace DonVo.FactoryManagement.Models.ViewModels.Production
{
    public class MonthlyProduction
    {
        public string StaffName { get; set; }
        public string ItemName { get; set; }
        public string ItemCategoryName { get; set; }
        public long Quantity { get; set; }
        public decimal Unitprice { get; set; }
        public long TotalAmount { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Month { get; set; }
        public string Id { get; set; }
        public string FactoryId { get; set; }
    }
}
