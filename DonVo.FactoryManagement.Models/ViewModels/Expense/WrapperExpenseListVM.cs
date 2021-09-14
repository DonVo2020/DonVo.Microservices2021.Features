using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.Expense
{
    public class WrapperExpenseListVM
    {
        public List<ExpenseVM> ListOfData { get; set; }
        public long TotalRecords { get; set; }

        public WrapperExpenseListVM() 
        { 
        
        }
    }
}
