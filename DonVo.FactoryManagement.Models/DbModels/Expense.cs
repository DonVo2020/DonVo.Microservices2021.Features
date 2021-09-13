using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.FactoryManagement.Models.DbModels
{
    public  class Expense : BaseEntity
    {
        public string ExpenseTypeId { get; set; }
         public string ClientId { get; set; }

        //public string CustomerId { get; set; }
        //public string SupplierId { get; set; }
        //public string StaffId { get; set; }
        public string InvoiceId { get; set; }
        public string Month { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public DateTime OccurranceDate { get; set; }
        public string EmployeeId { get; set; }

        [ForeignKey("ExpenseTypeId")]
        public virtual ExpenseType ExpenseType { get; set; }

        public string Purpose { get; set; }



        [ForeignKey("ClientId")]
        public Supplier Supplier { get; set; }

        [ForeignKey("ClientId")]
        public Customer Customer { get; set; }

        [ForeignKey("ClientId")]
        public Staff Staff { get; set; }
    }
}
