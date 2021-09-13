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

namespace DonVo.SignalR_MediatR.Core.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly DataContext _context;

        public ReminderRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddReminderAsync(Reminder reminder, CancellationToken cancellationToken)
        {
            await _context.Reminders.AddAsync(reminder, cancellationToken);
        }

        public async Task<Reminder> GetReminderAsync(string userId, int id, CancellationToken cancellationToken = default)
        {
            return await _context.Reminders.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId, cancellationToken);
        }

        public async Task<IEnumerable<Reminder>> GetRemindersAsync(string userId, ReminderParams reminderParams,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Reminders.Where(x => x.UserId == userId).AsNoTracking().AsQueryable();

            query = reminderParams.FilterBy switch
            {
                "due" => query.Where(x => x.DueDate <= DateTime.UtcNow),
                "removed" => query.IgnoreQueryFilters().Where(x => x.DeletedDate != null),
                _ => query
            };

            query = reminderParams.OrderBy switch
            {
                "due" => query.OrderBy(x => x.DueDate),
                "priority" => query.OrderByDescending(x => x.Priority),
                _ => query.OrderByDescending(x => x.Priority)
            };

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<Reminder>> GetRemindersAsync(string userId, IEnumerable<int> reminderIds, CancellationToken cancellationToken = default)
        {
            return await _context.Reminders.Where(x => x.UserId == userId)
                .Where(reminder => reminderIds.Contains(reminder.Id)).IgnoreQueryFilters().ToListAsync(cancellationToken);
        }

        public void RemoveReminders(IEnumerable<Reminder> reminders)
        {
            _context.RemoveRange(reminders);
        }
    }
}
