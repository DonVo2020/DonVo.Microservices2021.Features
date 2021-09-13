using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.ExpenseType
{
    public class WrapperExpenseTypeListVM
    {
        public long TotalRecords { get; set; }
        public List<ExpenseTypeVM> ListOfData { get; set; }
    }
}
