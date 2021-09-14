using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.EquipmentCategory
{
    public class WrapperEquipmentCategoryListVM
    {
        public long TotalRecords { get; set; }
        public List<EquipmentCategoryVM> ListOfData { get; set; }
    }
}
