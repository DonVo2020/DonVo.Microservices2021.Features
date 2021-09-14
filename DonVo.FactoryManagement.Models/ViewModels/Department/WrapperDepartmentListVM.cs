using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.Department
{
    public class WrapperDepartmentListVM
    {
        public long TotalRecords { get; set; }
        public List<DepartmentVM> ListOfData { get; set; }
    }
}
