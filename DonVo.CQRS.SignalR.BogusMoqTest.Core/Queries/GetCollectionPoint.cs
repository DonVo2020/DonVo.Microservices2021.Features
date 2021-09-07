using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Queries
{
    public static class GetCollectionPoint
    {
        public record Query(int EmployeeId, int CollectionPointId) : IRequest<CollectionPointListResponse>;

        public class Handler : IRequestHandler<Query, CollectionPointListResponse>
        {
            private readonly ICollectionPointRepository _collectionPointRepository;
            private readonly IMapper _mapper;

            public Handler(ICollectionPointRepository collectionPointRepository, IMapper mapper)
            {
                _collectionPointRepository = collectionPointRepository;
                _mapper = mapper;
            }

            public async Task<CollectionPointListResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var collectionPoint = await _collectionPointRepository.GetCollectionPointAsync(request.EmployeeId, request.CollectionPointId, cancellationToken);
                return _mapper.Map<CollectionPointListResponse>(collectionPoint);
            }
        }
    }
}
