using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models
{
    public class DisbursementDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int QtyNeeded { get; set; }
        [Required]
        public int QtyReceived { get; set; }
        [Required]
        public int DisbursementId { get; set;  }
        [Required]
        public string InventoryItemId { get; set; }

        public virtual Disbursement Disbursement { get; set; }      
        public virtual InventoryItem InventoryItem { get; set; }
    }
}
