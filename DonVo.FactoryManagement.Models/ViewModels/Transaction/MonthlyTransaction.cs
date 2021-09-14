using System;

namespace DonVo.FactoryManagement.Models.ViewModels.Transaction
{
    public class MonthlyTransaction
    {
        public string Id { get; set; }
        public string FactoryId { get; set; }
        public string InvoiceId { get; set; }
        public decimal Amount { get; set; }
        //  public string Amount { get; set; }
        public string ExecutorId { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        // public string PaymentStatusId { get; set; }
        public string PaymentStatus { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
        //        public string TransactionTypeId { get; set; }
        public string Month { get; set; }
        public string Purpose { get; set; }
        public string TransactionId { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
