using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models
{
    public class Retrieval
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime DateRetrieved { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public RetrievalStatus RetrievalStatus { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
