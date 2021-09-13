using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DonVo.SignalR_MediatR.Core.Interfaces;

namespace DonVo.SignalR_MediatR.Core.Commands.RemoveCommands
{
    public static class RemoveHomework
    {
        public record Command(string UserId, int HomeworkId) : IRequest;

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly IHomeworkRepository _homeworkRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;

            public Handler(IHomeworkRepository homeworkRepository, IUnitOfWork unitOfWork, ILogger<Handler> logger)
            {
                _homeworkRepository = homeworkRepository;
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var homework = await _homeworkRepository.GetHomeworkAsync(request.HomeworkId, request.UserId, cancellationToken);

                if (homework == null)
                {
                    _logger.LogWarning($"User tried to access homework: {request.HomeworkId}");
                    throw new UnauthorizedAccessException("You dont own this item");
                }

                _logger.LogInformation($"Removing homework {request}");
                _homeworkRepository.RemoveHomework(homework);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError("Failed to remove homework");
                    throw new DbUpdateException("Failed to remove homework");
                }
            }
        }
    }
}
