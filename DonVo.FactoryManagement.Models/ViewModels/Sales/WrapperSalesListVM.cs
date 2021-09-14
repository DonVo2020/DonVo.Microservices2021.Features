using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.Sales
{
    public class WrapperSalesListVM : CommonVM
    {
        public long TotalRecords { get; set; }
        public List<SalesVM> ListOfData { get; set; }
    }
}
