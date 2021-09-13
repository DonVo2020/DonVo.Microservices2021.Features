using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Staff
{
   public class StaffVM
    {
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }

        public string CellNo { get; set; }
        public string AlternateCellNo { get; set; }


        public string FactoryId { get; set; }
        public string Id { get; set; }


        public bool IsManager { get; set; }
        public string RoleId { get; set; }


        public long PaidAmount { get; set; }
        public long RecievedAmount { get; set; }
        public long PayableAmount { get; set; }
        public long ReceivableAmount { get; set; }


    }
}
