using AutoMapper;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Commands.AddCommands
{
    public static class AddCollectionPoint
    {
        public record Command(int? Id, string Name, string Time, int? EmployeeId) : IRequest<CollectionPointListResponse>;

        public class Handler : IRequestHandler<Command, CollectionPointListResponse>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICollectionPointRepository _collectionPointRepository;
            private readonly ILogger<Handler> _logger;

            public Handler(IMapper mapper, IUnitOfWork unitOfWork, ICollectionPointRepository collectionPointRepository, ILogger<Handler> logger)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _collectionPointRepository = collectionPointRepository;
                _logger = logger;

            }

            public async Task<CollectionPointListResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var collectionPoint = new CollectionPoint
                {
                    Name = request.Name,
                    Time = request.Time,
                    EmployeeId = request.EmployeeId
                };

                _logger.LogInformation($"Adding CollectionPoint {request}");
                await _collectionPointRepository.AddCollectionPointAsync(collectionPoint, cancellationToken);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError("Failed to add CollectionPoint");
                    throw new DbUpdateException("Failed to add CollectionPoint");
                }

                return _mapper.Map<CollectionPointListResponse>(collectionPoint);
            }
        }
    }
}
