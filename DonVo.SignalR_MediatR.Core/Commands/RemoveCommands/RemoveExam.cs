using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DonVo.SignalR_MediatR.Core.Interfaces;

namespace DonVo.SignalR_MediatR.Core.Commands.RemoveCommands
{
    public static class RemoveExam
    {
        public record Command(int ExamId, string UserId) : IRequest;

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IExamRepository _examRepository;
            private readonly ILogger<Handler> _logger;

            public Handler(IUnitOfWork unitOfWork, IExamRepository examRepository, ILogger<Handler> logger)
            {
                _unitOfWork = unitOfWork;
                _examRepository = examRepository;
                _logger = logger;
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var exam = await _examRepository.GetExamAsync(request.ExamId, request.UserId, cancellationToken);

                if (exam == null)
                {
                    _logger.LogWarning($"User tried to access exam: {request.ExamId}");
                    throw new UnauthorizedAccessException("You dont own this item");
                }

                _logger.LogInformation($"Removing exam {request}");
                _examRepository.RemoveExam(exam);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError("Failed to remove exam");
                    throw new DbUpdateException("Failed to remove exam");
                }
            }
        }
    }
}
