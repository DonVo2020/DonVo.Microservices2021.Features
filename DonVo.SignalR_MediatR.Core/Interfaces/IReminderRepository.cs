using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DonVo.SignalR_MediatR.Core.Models.Entities;
using DonVo.SignalR_MediatR.Core.Models.Params;

namespace DonVo.SignalR_MediatR.Core.Interfaces
{
    public interface IReminderRepository
    {
        Task<Reminder> GetReminderAsync(string userId, int reminderId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Reminder>> GetRemindersAsync(string userId, ReminderParams reminderParams, CancellationToken cancellationToken = default);
        Task AddReminderAsync(Reminder reminder, CancellationToken cancellationToken);

        /// <summary>
        /// Gets reminders that matches the passed in list of reminder ids while ignoring query filters
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="reminderIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Reminders with the specified ids if found</returns>
        Task<IReadOnlyCollection<Reminder>> GetRemindersAsync(string userId, IEnumerable<int> reminderIds, CancellationToken cancellationToken = default);
        void RemoveReminders(IEnumerable<Reminder> reminders);
    }
}
