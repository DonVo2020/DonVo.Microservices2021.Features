using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Employee
{
   public class EmployeeVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }

        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }

        public string CellNo { get; set; }
        public string AlternateCellNo { get; set; }


        public string FactoryId { get; set; }
        public string CustomerId { get; set; }
    }
}
