using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string DepartmentId { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public virtual Department Department { get; set;  }
        public virtual List<ActingDepartmentHead> ActingDepartmentHeads { get; set; }
        public bool IsActingDepartmentHead()
        {
            bool result = false;
            if (ActingDepartmentHeads.Count > 0)
            {
                foreach (ActingDepartmentHead head in ActingDepartmentHeads)
                {
                    if (head.EndDate > new DateTime())
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
