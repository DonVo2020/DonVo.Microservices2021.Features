using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Equipment
{
    public class WrapperEquipmentListVM
    {
        public long TotalRecords { get; set; }
        public List<EquipmentVM> ListOfData { get; set; }
    }
}
