using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Sales;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface ISalesService
    {
        Task<WrapperSalesListVM> AddSalesAsync(SalesVM salesVM);
        Task<WrapperSalesReturnVM> AddSalesReturn(SalesReturnVM vm);
        Task<WrapperSalesListVM> DeleteSalesAsync(SalesVM vm);
        Task<WrapperSalesReturnVM> DeleteSalesReturn(SalesReturnVM vm);
        Task<WrapperSalesListVM> GetAllSalesAsync(GetDataListVM getDataListVM);
        Task<WrapperSalesReturnVM> GetAllSalesReturn(GetDataListVM getDataListVM);
    }
}
