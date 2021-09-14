using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.Purchase
{
    public class WrapperPurchaseListVM : CommonVM
    {
        public long TotalRecords { get; set; }
        public List<PurchaseVM> ListOfData { get; set; }
    }
}
