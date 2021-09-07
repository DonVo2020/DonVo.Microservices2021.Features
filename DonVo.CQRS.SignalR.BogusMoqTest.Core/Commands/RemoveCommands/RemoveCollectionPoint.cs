using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Commands.RemoveCommands
{
    public static class RemoveCollectionPoint
    {
        public record Command(int EmployeeId, IEnumerable<int> CollectionPointIds) : IRequest;

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICollectionPointRepository _collectionPointRepository;
            private readonly ILogger<Handler> _logger;

            public Handler(IUnitOfWork unitOfWork, ICollectionPointRepository collectionPointRepository, ILogger<Handler> logger)
            {
                _unitOfWork = unitOfWork;
                _collectionPointRepository = collectionPointRepository;
                _logger = logger;
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var collectionPoints = await _collectionPointRepository.GetCollectionPointsAsync(request.EmployeeId, request.CollectionPointIds, cancellationToken);

                ICollection<CollectionPoint> collectionPointsToRemove = new List<CollectionPoint>();

                if (collectionPoints.Count < 1)
                {
                    _logger.LogWarning($"Could not find any CollectionPoints from request {request}");
                    throw new KeyNotFoundException("Could not find any CollectionPoints");
                }

                foreach (var collectionPoint in collectionPoints)
                {
                    if (string.IsNullOrWhiteSpace(collectionPoint.Time))
                    {
                        collectionPoint.Time = DateTime.UtcNow.ToLongTimeString();
                        _logger.LogInformation($"Reminder {collectionPoint.Name} recieved a soft delete time {collectionPoint.Time}");
                    }
                    else
                    {
                        collectionPointsToRemove.Add(collectionPoint);
                        _logger.LogInformation($"Adding CollectionPoint {collectionPoint.Name}: to remove");
                    }
                }

                if (collectionPointsToRemove.Count > 0)
                {
                    _logger.LogInformation("Removing CollectionPoints from database");
                    _collectionPointRepository.RemoveCollectionPoints(collectionPointsToRemove);
                }

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    throw new DbUpdateException("Failed to remove CollectionPoints");
                }
            }
        }
    }
}
