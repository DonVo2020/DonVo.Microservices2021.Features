using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models
{
    public class RetrievalDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int QtyNeeded { get; set; }
        public int QtyRetrieved { get; set; }
        public int RetrievalId { get;  set;  }
        public string InventoryItemId { get; set;   }
        public virtual Retrieval Retrieval { get; set; }
        public virtual InventoryItem InventoryItem { get; set; }
    }
}
