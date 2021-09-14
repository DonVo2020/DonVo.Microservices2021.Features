using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.Role
{
    public  class WrapperRoleListVM
    {
        public long TotalRecords { get; set; }
        public List<RoleVM> ListOfData { get; set; }
    }
}
