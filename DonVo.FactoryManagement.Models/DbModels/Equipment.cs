using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.FactoryManagement.Models.DbModels
{
    public  class Equipment : BaseEntity
    {

        public int? MachineNumber { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string EquipmentCategoryId { get; set; }

        [ForeignKey("EquipmentCategoryId")]
        public virtual EquipmentCategory EquipmentCategory { get; set; }
    }
}
