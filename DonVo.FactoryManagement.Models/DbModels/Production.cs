using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.FactoryManagement.Models.DbModels
{
    public  class Production : BaseEntity
    {
        public string StaffId { get; set; }
        public string ItemId {  get; set; }
        public string ItemStatusId { get; set; }
        public string InvoiceId { get; set; }
        public string ItemCategoryId { get; set; }
        public long Quantity { get; set; }
        public string EquipmentId { get; set; }
        public DateTime EntryDate { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public bool? IsMadeJointly { get; set; }
        public string ProductionId { get; set; }
        public string Month { get; set; }


        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }

        [ForeignKey("ItemCategoryId")]
        public virtual ItemCategory ItemCategory { get; set; }

        [ForeignKey("StaffId")]
        public virtual Staff Staff { get; set; }

        [ForeignKey("EquipmentId")]
        public virtual Equipment Equipment { get; set; }


        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }


        [ForeignKey("ItemStatusId")]
        public virtual ItemStatus ItemStatus { get; set; }

    }
}
