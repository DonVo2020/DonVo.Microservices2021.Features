using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.FactoryManagement.Models.DbModels
{
    public class Staff : BaseEntity
    {

        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string DepartmentId { get; set; }
        public string Code { get; set; }
        public double? BasicSalary { get; set; }
        public string BirthRegistrationNo { get; set; }
        public string NationalIdNo { get; set; }

        public string RoleId { get; set; }

        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }
        public string Number { get; set; }
        public string AlternateNumber_1 { get; set; }
        public string AlternateNumber_2 { get; set; }


        [ForeignKey("RoleId")]
        public Role Role { get; set; }

    }
}
