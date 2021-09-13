using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.FactoryManagement.Models.DbModels
{
    [Table("TblTransaction")]
    public  class TblTransaction : BaseEntity
    {      
        public string InvoiceId { get; set; }

        public decimal Amount { get; set; }
        //  public string Amount { get; set; }
        public string ExecutorId { get; set; }
        public string ClientId { get; set; }

        // public string PaymentStatusId { get; set; }

        public string PaymentStatus { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
        //        public string TransactionTypeId { get; set; }
        public string Month { get; set; }
        public string Purpose { get; set; }
        public string TransactionId { get; set; }


        [ForeignKey("ClientId")]
        public Supplier Supplier { get; set; }

        [ForeignKey("ClientId")]
        public Customer Customer { get; set; }

        [ForeignKey("ClientId")]
        public Staff Staff { get; set; }
    }
}
