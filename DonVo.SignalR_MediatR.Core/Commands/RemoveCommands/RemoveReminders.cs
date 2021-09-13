using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DonVo.SignalR_MediatR.Core.Interfaces;
using DonVo.SignalR_MediatR.Core.Models.Entities;

namespace DonVo.SignalR_MediatR.Core.Commands.RemoveCommands
{
    public static class RemoveReminders
    {
        public record Command(string UserId, IEnumerable<int> ReminderIds) : IRequest;

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IReminderRepository _reminderRepository;
            private readonly ILogger<Handler> _logger;

            public Handler(IUnitOfWork unitOfWork, IReminderRepository reminderRepository, ILogger<Handler> logger)
            {
                _unitOfWork = unitOfWork;
                _reminderRepository = reminderRepository;
                _logger = logger;
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var reminders = await _reminderRepository.GetRemindersAsync(request.UserId, request.ReminderIds);
                ICollection<Reminder> remindersToRemove = new List<Reminder>();

                if (reminders.Count < 1)
                {
                    _logger.LogWarning($"Could not find any reminders from request {request}");
                    throw new KeyNotFoundException("Could not find any reminders");
                }

                foreach (var reminder in reminders)
                {
                    if (reminder.DeletedDate == null)
                    {
                        reminder.DeletedDate = DateTime.UtcNow;
                        _logger.LogInformation($"Reminder {reminder.Name} recieved a soft delete date {reminder.DeletedDate}");
                    }
                    else
                    {
                        remindersToRemove.Add(reminder);
                        _logger.LogInformation($"Adding reminder {reminder.Name}: to remove");
                    }
                }

                if (remindersToRemove.Count > 0)
                {
                    _logger.LogInformation("Removing reminders from database");
                    _reminderRepository.RemoveReminders(remindersToRemove);
                }

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    throw new DbUpdateException("Failed to remove reminders");
                }
            }
        }
    }
}
