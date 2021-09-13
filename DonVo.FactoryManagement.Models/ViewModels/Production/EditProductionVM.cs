using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Production
{
  public  class EditProductionVM
    {
        public string Id { get; set; }
        public string FactoryId { get; set; }

        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; }
        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public DateTime EntryDate { get; set; }
        public string ProductionId { get; set; }
        public decimal UnitPrice { get; set; }
        public long Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
