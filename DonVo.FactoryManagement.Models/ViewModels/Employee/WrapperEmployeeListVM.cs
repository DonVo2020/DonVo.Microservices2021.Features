using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.Employee
{
    public class WrapperEmployeeListVM
    {
        public long TotalRecords { get; set; }
        public List<EmployeeVM> ListOfData { get; set; }
    }
}
