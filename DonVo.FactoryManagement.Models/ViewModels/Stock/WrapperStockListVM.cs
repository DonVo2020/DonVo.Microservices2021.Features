using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.Stock
{
    public  class WrapperStockListVM
    {
        public long TotalRecords { get; set; }
        public List<StockVM> ListOfData { get; set; }
    }
}
