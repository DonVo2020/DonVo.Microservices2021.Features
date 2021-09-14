using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;

namespace Service.BusinessServices
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUtilService _utilService;

        public InvoiceService(IRepositoryWrapper repositoryWrapper,  IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }
    }
}
