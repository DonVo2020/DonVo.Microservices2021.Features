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
    public static class AddMessage
    {
        public record Command(string SenderId, string RecipientEmail, string Content) : IRequest<MessageResponse>;

        public class Handler : IRequestHandler<Command, MessageResponse>
        {
            private readonly IMessageRepository _messageRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;

            public Handler(IMessageRepository messageRepository, IUnitOfWork unitOfWork, ILogger<Handler> logger, IMapper mapper, IUserRepository userRepository)
            {
                _messageRepository = messageRepository;
                _unitOfWork = unitOfWork;
                _logger = logger;
                _mapper = mapper;
                _userRepository = userRepository;
            }

            public async Task<MessageResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var recipient = await _userRepository.GetUserByEmailAsync(request.RecipientEmail);

                var message = new Message
                {
                    RecipientId = recipient.Id,
                    SenderId = request.SenderId,
                    Content = request.Content
                };

                _logger.LogInformation($"Sending messge to {recipient.Id}");
                await _messageRepository.AddMessageAsync(message, cancellationToken);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError("Failed to send message");
                    throw new DbUpdateException("Failed to send message");
                }

                var messageResponse = _mapper.Map<MessageResponse>(message);
                var sender = await _userRepository.GetUserAsync(request.SenderId);
                messageResponse.RecipientUsername = recipient.UserName;
                messageResponse.SenderUsername = sender.UserName;
                return messageResponse;
            }
        }
    }
}
