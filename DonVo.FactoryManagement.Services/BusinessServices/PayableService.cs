using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;

namespace Service.BusinessServices
{
    public class PayableService : IPayableService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUtilService _utilService;

        public PayableService(IRepositoryWrapper repositoryWrapper, IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }
    }
}
