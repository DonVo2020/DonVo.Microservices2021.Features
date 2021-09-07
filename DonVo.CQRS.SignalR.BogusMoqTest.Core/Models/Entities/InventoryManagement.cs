using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models
{
    public class InventoryManagement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string InventoryItemId { get; set; }
        [Range(1, 100000,
            ErrorMessage = "Quantity must be a positive number")]
        public int addQty { get; set; }

        public virtual Employee Employee {   get;  set; }
        public virtual InventoryItem InventoryItem { get; set; }
    }
}
