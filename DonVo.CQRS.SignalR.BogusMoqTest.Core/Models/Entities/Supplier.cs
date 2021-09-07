using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models
{
    public class Supplier
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set;}
        [Required]
        public String Name { get; set;}
        [Required]
        public string ContactName{ get; set;}
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string FaxNo { get; set;}
        [Required]
        public string Address { get; set;}
        [Required]
        public string GSTReg { get; set;}
    }
}
