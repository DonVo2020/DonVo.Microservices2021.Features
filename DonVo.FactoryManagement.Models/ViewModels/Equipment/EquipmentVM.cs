using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Equipment
{
    public class EquipmentVM
    {
        public int MachineNumber { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string EquipmentCategoryId { get; set; }
        public string EquipmentCategoryName { get; set; }
        public string FactoryId { get; set; }
        public string Id { get; set; }

    }
}
