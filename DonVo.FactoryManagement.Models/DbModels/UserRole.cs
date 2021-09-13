using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.FactoryManagement.Models.DbModels
{
    public  class UserRole : BaseEntity
    {
 
        public string UserId { get; set; }
        public string RoleId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserAuthInfo UserAuthInfo { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
