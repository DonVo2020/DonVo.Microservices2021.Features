using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models
{
    public class RequestDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string InventoryItemId { get; set; }

        [Range(1, 99, ErrorMessage = "Requested quantity must be below 99")]
        public int QtyRequested { get; set; }

        public virtual Request Request { get; set; }
        public virtual InventoryItem InventoryItem { get; set; }
    }
}
