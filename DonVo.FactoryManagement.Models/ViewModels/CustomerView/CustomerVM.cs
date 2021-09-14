namespace DonVo.FactoryManagement.Models.ViewModels.CustomerView
{
    public  class CustomerVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }
        public string CellNo { get; set; }
        public string AlternateCellNo { get; set; }
        public string FactoryId { get; set; }
        public string CustomerId { get; set; }

        public long PaidAmount { get; set; }
        public long RecievedAmount { get; set; }
        public long PayableAmount { get; set; }
        public long ReceivableAmount { get; set; }
    }
}
