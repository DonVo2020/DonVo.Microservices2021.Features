using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Queries
{
    public static class GetCollectionPoints
    {
        public record Query(int EmployeeId, List<int> collectionPointIds) : IRequest<IEnumerable<CollectionPointListResponse>>;

        public class Handler : IRequestHandler<Query, IEnumerable<CollectionPointListResponse>>
        {
            private readonly ICollectionPointRepository _collectionPointRepository;
            private readonly ILogger<Handler> _logger;
            private readonly IMapper _mapper;

            public Handler(ICollectionPointRepository collectionPointRepository, ILogger<Handler> logger, IMapper mapper)
            {
                _collectionPointRepository = collectionPointRepository;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<IEnumerable<CollectionPointListResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"Getting CollectionPoints");
                var collectionPoints = await _collectionPointRepository.GetCollectionPointsAsync(request.EmployeeId,request.collectionPointIds, cancellationToken);
                return _mapper.Map<IEnumerable<CollectionPointListResponse>>(collectionPoints);
            }
        }
    }
}
