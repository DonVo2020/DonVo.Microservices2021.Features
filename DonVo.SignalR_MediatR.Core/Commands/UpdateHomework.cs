using AutoMapper;
using DonVo.SignalR_MediatR.Core.Interfaces;
using DonVo.SignalR_MediatR.Core.Models.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.SignalR_MediatR.Core.Commands
{
    public static class UpdateHomework
    {
        public record Command(int HomeworkId, string Name, string Description, DateTime DueDate, string UserId) : IRequest<HomeworkListResponse>;

        public class Handler : IRequestHandler<Command, HomeworkListResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;
            private readonly IHomeworkRepository _homeworkRepository;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger, IHomeworkRepository homeworkRepository, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
                _homeworkRepository = homeworkRepository;
                _mapper = mapper;
            }

            public async Task<HomeworkListResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var homework = await _homeworkRepository.GetHomeworkAsync(request.HomeworkId, request.UserId, cancellationToken);

                if (homework == null)
                {
                    _logger.LogWarning($"User tried to access homework: {request.HomeworkId}");
                    throw new UnauthorizedAccessException("You dont own this item");
                }

                _logger.LogInformation($"Updating homework {homework.Id} with the incoming request {request}");
                homework.Description = request.Description;
                homework.DueDate = request.DueDate;
                homework.Name = request.Name;

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogWarning($"No changes to homework {homework.Id}");
                }

                return _mapper.Map<HomeworkListResponse>(homework);
            }
        }
    }
}
