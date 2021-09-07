using AutoMapper;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Queries
{
    public static class GetSuppliers
    {
        public record Query() : IRequest<IEnumerable<SupplierDetailResponse>>;


        public class Handler : IRequestHandler<Query, IEnumerable<SupplierDetailResponse>>
        {
            private readonly ISupplierRepository _supplierRepository;
            private readonly IMapper _mapper;

            public Handler(ISupplierRepository supplierRepository, IMapper mapper)
            {
                _supplierRepository = supplierRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<SupplierDetailResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var suppliers = await _supplierRepository.GetSuppliersAsync(cancellationToken);
                return _mapper.Map<IEnumerable<SupplierDetailResponse>>(suppliers);
            }
        }
    }
}
