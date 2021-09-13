using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DonVo.SignalR_MediatR.Core.Data;
using DonVo.SignalR_MediatR.Core.Interfaces;
using DonVo.SignalR_MediatR.Core.Models.Entities;
using DonVo.SignalR_MediatR.Core.Models.Params;
using DonVo.SignalR_MediatR.Core.Models.Responses;

namespace DonVo.SignalR_MediatR.Core.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;

        public MessageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddMessageAsync(Message message, CancellationToken cancellationToken)
        {
            await _context.Messages.AddAsync(message, cancellationToken);
        }

        public async Task<Message> GetMessage(int messageId, string userId)
        {
            return await _context.Messages.FirstOrDefaultAsync(x => x.SenderId == userId || x.RecipientId == userId
            && x.Id == messageId);
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(string userId, MessageParams messageParams, CancellationToken cancellationToken = default)
        {
            var query = _context.Messages.OrderByDescending(m => m.DateSent).AsNoTracking().AsQueryable();

            query = messageParams.FilterBy switch
            {
                "Inbox" => query.Where(x => x.RecipientId == userId && !x.RecipientDeleted),
                "Outbox" => query.Where(x => x.SenderId == userId && !x.SenderDeleted),
                _ => query.Where(x => x.RecipientId == userId && !x.RecipientDeleted && x.DateRead == null)
            };

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Message>> GetMessageThreadAsync(string userId, string otherUserId, CancellationToken cancellationToken = default)
        {
            var messages = await _context.Messages
                .Where(x => x.RecipientId == userId && !x.RecipientDeleted && x.SenderId == otherUserId
                || x.SenderId == userId && !x.SenderDeleted && x.RecipientId == otherUserId).ToListAsync(cancellationToken);

            return messages;
        }

        public void RemoveMessage(Message message)
        {
            _context.Remove(message);
        }
    }
}
