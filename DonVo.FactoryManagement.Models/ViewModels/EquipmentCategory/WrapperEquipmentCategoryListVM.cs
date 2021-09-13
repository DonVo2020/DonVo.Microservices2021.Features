using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.EquipmentCategory
{
    public class WrapperEquipmentCategoryListVM
    {
        public long TotalRecords { get; set; }
        public List<EquipmentCategoryVM> ListOfData { get; set; }
    }
}
