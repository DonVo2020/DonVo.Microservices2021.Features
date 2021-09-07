using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models
{
    public class Disbursement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime DateRequested { get; set; }
        [Required]
        public DateTime DisbursedDate { get; set; }
        [Required]
        public string DepartmentId { get; set; }
        public string DisbursementStatus { get; set; }
        public virtual Department Department { get; set; }
    }
}
