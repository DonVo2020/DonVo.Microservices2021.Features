using DonVo.FactoryManagement.Models.ViewModels.Factory;
using DonVo.FactoryManagement.Models.ViewModels.Role;
using DonVo.FactoryManagement.Models.ViewModels.UserAuthInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Auth
{
    public class LoginResponseVM
    {

        public LoginResponseVM() {
            FactoryVM = null;
            UserAuthInfoVM = null;
            RoleVM = null;
            LoginSuccess = false;
            ResponseMessage = "Login Failed";
        }
        public UserAuthInfoVM UserAuthInfoVM { get; set; }
        public FactoryVM FactoryVM { get; set; }
        public RoleVM RoleVM { get; set; }
        public bool LoginSuccess { get; set; }
        public string ResponseMessage { get; set; }
        public string AuthToken { get; set; }

        public bool Leader { get; set; }


        //public string UserId { get; set; }
        //public string UserId { get; set; }
        //public string UserId { get; set; }

    }
}
