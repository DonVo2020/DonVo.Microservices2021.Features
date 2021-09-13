using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DonVo.FactoryManagement.Models.DbModels
{
   public  class Income : BaseEntity
    {
        public string IncomeTypeId { get; set; }
        public string ClientId { get; set; }
        public string InvoiceId { get; set; }
        public string Month { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public DateTime OccurranceDate { get; set; }
        public string EmployeeId { get; set; }

        public string Purpose { get; set; }

        [ForeignKey("IncomeTypeId")]
        public virtual IncomeType IncomeType { get; set; }


        [ForeignKey("ClientId")]
        public Supplier Supplier { get; set; }

        [ForeignKey("ClientId")]
        public Customer Customer { get; set; }


        [ForeignKey("ClientId")]
        public Staff Staff { get; set; }

    }
}
