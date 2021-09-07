using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models
{
    public class SupplierStationery
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string SupplierId { get; set; }
        [Required]
        public string InventoryItemId { get; set; }
        [Required]
        public string UOM { get; set; }
        [Required]
        public float TenderPrice { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual InventoryItem InventoryItem { get; set; }
    }
}
