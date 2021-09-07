using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Params;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces
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
