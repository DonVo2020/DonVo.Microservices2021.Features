using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.Staff
{
    public class WrapperStaffListVM
    {
        public long TotalRecords { get; set; }
        public List<StaffVM> ListOfData { get; set; }
    }
}
