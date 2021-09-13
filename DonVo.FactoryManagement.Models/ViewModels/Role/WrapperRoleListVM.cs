using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Role
{
   public  class WrapperRoleListVM
    {
        public long TotalRecords { get; set; }
        public List<RoleVM> ListOfData { get; set; }
    }
}
