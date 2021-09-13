using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.InvoiceType;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IInvoiceTypeService
    {
        Task<WrapperInvoiceTypeListVM> GetListPaged(GetDataListVM dataListVM);
        Task<WrapperInvoiceTypeListVM> Add(InvoiceTypeVM vm);
        Task<WrapperInvoiceTypeListVM> Update(string id, InvoiceTypeVM vm);
        Task<WrapperInvoiceTypeListVM> Delete(InvoiceTypeVM itemTemp);
    }   
}
