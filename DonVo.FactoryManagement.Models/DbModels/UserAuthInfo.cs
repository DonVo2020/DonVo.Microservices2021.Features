using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.FactoryManagement.Models.DbModels
{
    public  class UserAuthInfo : BaseEntity
    {
      
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


        [ForeignKey("FactoryId")]
        public Factory Factory { get; set; }
    }
}
