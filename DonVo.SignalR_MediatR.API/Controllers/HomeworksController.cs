using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DonVo.SignalR_MediatR.Core.Commands;
using DonVo.SignalR_MediatR.Core.Commands.AddCommands;
using DonVo.SignalR_MediatR.Core.Commands.RemoveCommands;
using DonVo.SignalR_MediatR.Core.Extensions;
using DonVo.SignalR_MediatR.Core.Models.Requests;
using DonVo.SignalR_MediatR.Core.Models.Responses;
using DonVo.SignalR_MediatR.Core.Queries;

namespace DonVo.SignalR_MediatR.API.Controllers
{

    public class HomeworksController : BaseApiController
    {

        private readonly IMediator _mediator;

        public HomeworksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<HomeworkListResponse>> AddHomework(AddHomeworkRequest addHomeworkRequest, CancellationToken cancellationToken)
        {
            var homework = await _mediator.Send(new AddHomework.Command(
                addHomeworkRequest.Name, addHomeworkRequest.Description, addHomeworkRequest.DueDate, addHomeworkRequest.SubjectId, User.GetUserId()),
                cancellationToken);

            return CreatedAtRoute(nameof(GetHomework), new { homework.Id }, homework);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HomeworkListResponse>>> GetHomeworks([FromQuery] int? subjectId, CancellationToken cancellationToken)
        {
            var homeworks = await _mediator.Send(
                new GetHomeworks.Query(subjectId, User.GetUserId()), cancellationToken);
            return Ok(homeworks);
        }

        [HttpGet("{id}", Name = nameof(GetHomework))]
        public async Task<ActionResult<HomeworkDetailResponse>> GetHomework(int id)
        {
            var homework = await _mediator.Send(new GetHomework.Query(User.GetUserId(), id));
            return homework;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveHomework(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new RemoveHomework.Command(User.GetUserId(), id), cancellationToken);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<ActionResult<HomeworkListResponse>> UpdateHomework(int id, UpdateHomeworkRequest updateHomeworkRequest, CancellationToken cancellationToken)
        {
            var homework = await _mediator.Send(new UpdateHomework.Command(
                   id, updateHomeworkRequest.Name, updateHomeworkRequest.Description, updateHomeworkRequest.DueDate, User.GetUserId()), cancellationToken);

            return homework;
        }
    }
}
