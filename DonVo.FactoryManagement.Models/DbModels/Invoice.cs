using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.FactoryManagement.Models.DbModels
{
    public  class Invoice : BaseEntity
    {

        public string EmployeeId { get; set; }
        public string ClientId { get; set; }
        public string InvoiceId { get; set; }
        public decimal? Discount { get; set; }
        public bool? IsDiscountInPercentage { get; set; }
        // public string AmountBeforeDiscount { get; set; }
        // public string AmountPaid { get; set; }
        // public string AmountAfterDiscount { get; set; }

        public long AmountBeforeDiscount { get; set; }
        public long AmountPaid { get; set; }
        public long AmountAfterDiscount { get; set; }
        public string InvoiceTypeId { get; set; }
        public bool? IsFullyPaid { get; set; }
        public DateTime? DateOfOcurrance { get; set; }
        public decimal? Due { get; set; }
        public decimal? DeliveryCost { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal? OtherCost { get; set; }
        public DateTime? DeliveryDateTime { get; set; }
        public string Comment { get; set; }

        [ForeignKey("InvoiceTypeId")]
        public virtual InvoiceType InvoiceType { get; set; }

        [ForeignKey("ClientId")]
        public virtual Supplier Supplier { get; set; }


        [ForeignKey("ClientId")]
        public virtual Customer Customer { get; set; }


        [ForeignKey("ClientId")]
        public virtual Staff Staff { get; set; }
    }
}
