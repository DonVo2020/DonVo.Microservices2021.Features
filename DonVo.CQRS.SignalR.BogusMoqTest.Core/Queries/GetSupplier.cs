using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Queries
{
    public static class GetSupplier
    {
        public record Query(int SupplierId) : IRequest<SupplierDetailResponse>;

        public class Handler : IRequestHandler<Query, SupplierDetailResponse>
        {
            private readonly ISupplierRepository _supplierRepository;
            private readonly IMapper _mapper;

            public Handler(ISupplierRepository supplierRepository, IMapper mapper)
            {
                _supplierRepository = supplierRepository;
                _mapper = mapper;
            }
            public async Task<SupplierDetailResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var supplier = await _supplierRepository.GetSupplierByIdAsync(request.SupplierId, cancellationToken);

                return _mapper.Map<SupplierDetailResponse>(supplier);
            }
        }
    }
}
