using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.IBusinessServiceWrapper;

namespace Service.BusinessWrapper
{
    public class BusinessServices : IBusinessService
    {

        public IPurchaseWrapperService _PurchaseWrapperService;
        public IBusinessWrapperService _BusinessWrapperService;
        public BusinessServices(IPurchaseWrapperService PurchaseWrapperService,
            IBusinessWrapperService BusinessWrapperService)
        {
            this._PurchaseWrapperService = PurchaseWrapperService;
            this._BusinessWrapperService = BusinessWrapperService;
        }

        public IPurchaseWrapperService PurchaseServiceWrapper
        {
            get
            {
                return _PurchaseWrapperService;
            }
        }
        public IBusinessWrapperService BusinessWrapperService
        {
            get
            {
                return _BusinessWrapperService;
            }
        }
    }
}
