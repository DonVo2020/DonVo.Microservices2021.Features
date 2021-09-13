using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Department
{
    public class WrapperDepartmentListVM
    {
        public long TotalRecords { get; set; }
        public List<DepartmentVM> ListOfData { get; set; }
    }
}
