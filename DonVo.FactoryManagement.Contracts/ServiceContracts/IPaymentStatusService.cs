using DonVo.FactoryManagement.Models.ViewModels.PaymentStatus;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IPaymentStatusService
    {
        Task<WrapperPaymentStatusListVM> Add(PaymentStatusVM vm);
        Task<WrapperPaymentStatusListVM> Delete(PaymentStatusVM itemTemp);
        Task<WrapperPaymentStatusListVM> GetListPaged(DonVo.FactoryManagement.Models.ViewModels.GetDataListVM dataListVM);
        Task<WrapperPaymentStatusListVM> Update(string id, PaymentStatusVM vm);
    }
}
