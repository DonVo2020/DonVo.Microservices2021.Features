using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.Production
{
    public class WrapperProductionListVM : CommonVM
    {
        public WrapperProductionListVM()
        {
            ListOfData = new List<AddProductionVM>();
        }
        public long TotalRecords { get; set; }
        public List<AddProductionVM> ListOfData { get; set; }
    }

    //public class WrapperMonthPayableListVM
    //{
    //    public WrapperMonthPayableListVM()
    //    {
    //        ListOfData = new List<MonthlyProduction>();
    //    }

    //    public long TotalRecords { get; set; }
    //    public List<MonthlyProduction> ListOfData { get; set; }
    //}







}
