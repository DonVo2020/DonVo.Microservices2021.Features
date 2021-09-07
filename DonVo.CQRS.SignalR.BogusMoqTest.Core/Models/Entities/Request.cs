using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models
{
    public class Request
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        [Required]
        public DateTime DateRequested { get; set; }
        [Required]
        public Status Status{ get; set; }

        public string Comments { get; set; }
        public int? RetrievalId { get; set; }
        public int? DisbursementId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Retrieval Retrieval { get; set; }
        public virtual Disbursement Disbursement { get; set; }
        
    }
}
