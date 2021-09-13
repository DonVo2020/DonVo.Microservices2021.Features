using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.UserAuthInfo
{
   public class UserAuthInfoVM
    {

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Id { get; set; }
        public string FactoryId { get; set; }

    }
}
