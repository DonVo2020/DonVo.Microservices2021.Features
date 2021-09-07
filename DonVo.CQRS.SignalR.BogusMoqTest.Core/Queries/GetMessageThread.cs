using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Mappers;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Queries
{
    public static class GetMessageThread
    {
        public record Query(string UserId, string OtherUserEmail) : IRequest<IEnumerable<MessageResponse>>;

        public class Handler : IRequestHandler<Query, IEnumerable<MessageResponse>>
        {
            private readonly IMessageRepository _messageRepository;
            private readonly IUserRepository _userRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;
            private readonly IMapper _mapper;

            public Handler(IMessageRepository messageRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<Handler> logger,
                IMapper mapper)
            {
                _messageRepository = messageRepository;
                _userRepository = userRepository;
                _unitOfWork = unitOfWork;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<IEnumerable<MessageResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var otherUser = await _userRepository.GetUserByEmailAsync(request.OtherUserEmail);

                if (otherUser == null)
                {
                    throw new KeyNotFoundException("Could not find user with the specified email");
                }

                var messages = await _messageRepository.GetMessageThreadAsync(request.UserId, otherUser.Id, cancellationToken);
                var user = await _userRepository.GetUserAsync(request.UserId);

                var messageResponses = new List<MessageResponse>(messages.Count());

                foreach (var message in messages)
                {
                    if (message.DateRead == null && message.RecipientId == request.UserId)
                    {
                        message.DateRead = DateTime.UtcNow;
                    }

                    var messageResponse = _mapper.Map<MessageResponse>(message);
                    messageResponse.SenderUsername = message.SenderId == request.UserId ? user.UserName : otherUser.UserName;
                    messageResponse.RecipientUsername = message.RecipientId == request.UserId ? user.UserName : otherUser.UserName;

                    messageResponses.Add(messageResponse);
                }

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogWarning($"No messages to mark as read");
                }

                return messageResponses;
            }
        }
    }
}
