using DonVo.FactoryManagement.Contracts.IBusinessServiceWrapper;

namespace DonVo.FactoryManagement.Contracts
{
    public interface IBusinessService
    {
        IPurchaseWrapperService PurchaseServiceWrapper { get; }
        IBusinessWrapperService BusinessWrapperService { get; }
    }
}
