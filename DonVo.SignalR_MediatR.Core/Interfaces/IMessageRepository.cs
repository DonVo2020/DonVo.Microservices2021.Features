using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DonVo.SignalR_MediatR.Core.Models.Entities;
using DonVo.SignalR_MediatR.Core.Models.Params;
using DonVo.SignalR_MediatR.Core.Models.Responses;

namespace DonVo.SignalR_MediatR.Core.Interfaces
{
    public interface IMessageRepository
    {
        Task AddMessageAsync(Message message, CancellationToken cancellationToken);
        Task<IEnumerable<Message>> GetMessagesAsync(string userId, MessageParams messageParams, CancellationToken cancellationToken = default);
        Task<IEnumerable<Message>> GetMessageThreadAsync(string userId, string otherUserId, CancellationToken cancellationToken = default);
        void RemoveMessage(Message message);

        /// <summary>
        /// Gets a message
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="userId"></param>
        /// <returns>A message if found otherwise null</returns>
        Task<Message> GetMessage(int messageId, string userId);
    }
}
