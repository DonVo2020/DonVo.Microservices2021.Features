using System;

namespace DonVo.FactoryManagement.Models.ViewModels.Production
{
    public class AddProductionVM
    {
        public string Id { get; set; }
        public string FactoryId { get; set; }
        public string InvoiceTypeId { get; set; }
        public string InvoiceId { get; set; }
        public string ExecutorId { get; set; }
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public string ItemId { get; set; }
        public string ItemStatusName { get; set; }
   //     public string ItemStatusId { get; set; }
        public string ItemName { get; set; }
        public string ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; }
        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public DateTime EntryDate { get; set; }
        public string ProductionId { get; set; }
        public string ItemStatusId { get; set; }
        public decimal UnitPrice { get; set; }
        public long Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}


// staff
// equipment
// item
// item category