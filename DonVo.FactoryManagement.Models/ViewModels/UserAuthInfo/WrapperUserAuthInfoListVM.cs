using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.UserAuthInfo
{
   public class WrapperUserAuthInfoListVM
    {
        public WrapperUserAuthInfoListVM()
        {
            ListOfData = new List<UserAuthInfoVM>();
            TotalRecords = 0;
        }
        public long TotalRecords { get; set; }
        public List<UserAuthInfoVM> ListOfData { get; set; }
    }
}
