using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DonVo.SignalR_MediatR.Core.Commands.AddCommands;
using DonVo.SignalR_MediatR.Core.Commands.RemoveCommands;
using DonVo.SignalR_MediatR.Core.Extensions;
using DonVo.SignalR_MediatR.Core.Models.Params;
using DonVo.SignalR_MediatR.Core.Models.Requests;
using DonVo.SignalR_MediatR.Core.Models.Responses;
using DonVo.SignalR_MediatR.Core.Queries;

namespace DonVo.SignalR_MediatR.API.Controllers
{
    public class RemindersController : BaseApiController
    {
        private readonly IMediator _mediator;

        public RemindersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ReminderResponse>> AddReminder(AddReminderRequest addReminderRequest, CancellationToken cancellationToken)
        {
            var reminder = await _mediator.Send(
                new AddReminder.Command(User.GetUserId(), addReminderRequest.Name, addReminderRequest.Description, addReminderRequest.DueDate,
                addReminderRequest.Priority), cancellationToken);
            return reminder;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReminderResponse>>> GetReminders([FromQuery]ReminderParams reminderParams, CancellationToken cancellationToken)
        {
            var reminders = await _mediator.Send(new GetReminders.Query(User.GetUserId(), reminderParams), cancellationToken);
            return Ok(reminders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReminderResponse>> GetReminder(int id, CancellationToken cancellationToken)
        {
            var reminder = await _mediator.Send(new GetReminder.Query(User.GetUserId(), id), cancellationToken);
            return reminder;
        }

        [HttpDelete]
        public async Task<ActionResult<ReminderResponse>> RemoveReminders([FromQuery] IEnumerable<int> ids, CancellationToken cancellationToken)
        {
            await _mediator.Send(new RemoveReminders.Command(User.GetUserId(), ids), cancellationToken);
            return NoContent();
        }
    }
}
