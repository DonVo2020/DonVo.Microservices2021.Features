using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Stock;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IStockService
    {
        Task<WrapperStockListVM> Add(StockVM ViewModel);
        Task<WrapperStockListVM> Update(string id, StockVM ViewModel);
        Task<WrapperStockListVM> GetListPaged(GetDataListVM dataListVM);
        Task<WrapperStockListVM> Delete(StockVM customerTemp);
        Task<WrapperStockListVM> ChangeItemStatus(StockVM ViewModel);
    }
}
