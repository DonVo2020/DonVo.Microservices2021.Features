using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Commands;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Commands.AddCommands;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Commands.RemoveCommands;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Extensions;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Requests;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Queries;

namespace DonVo.CQRS.SignalR.BogusMoqTest.API.Controllers
{

    public class CollectionPointsController : BaseApiController
    {

        private readonly IMediator _mediator;

        public CollectionPointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CollectionPointListResponse>> AddCollectionPoint(AddCollectionPointRequest addCollectionPointRequest, CancellationToken cancellationToken)
        {
            var collectionPoint = await _mediator.Send(new AddCollectionPoint.Command(null,
                addCollectionPointRequest.Name, addCollectionPointRequest.Time, addCollectionPointRequest.EmployeeId),
                cancellationToken);

            return CreatedAtRoute(nameof(GetCollectionPoint), new { collectionPoint.Id }, collectionPoint);
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<CollectionPointListResponse>>> GetCollectionPoints([FromQuery] int? subjectId, CancellationToken cancellationToken)
        //{
        //    var homeworks = await _mediator.Send(
        //        new GetCollectionPoints.Query(subjectId, User.GetUserId()), cancellationToken);
        //    return Ok(homeworks);
        //}

        //[HttpGet("{id}", Name = nameof(GetHomework))]
        //public async Task<ActionResult<HomeworkDetailResponse>> GetHomework(int id)
        //{
        //    var homework = await _mediator.Send(new GetHomework.Query(User.GetUserId(), id));
        //    return homework;
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> RemoveCollectionPoint(int employeeId, CancellationToken cancellationToken)
        //{
        //    await _mediator.Send(new RemoveCollectionPoint.Command(employeeId, id), cancellationToken);
        //    return NoContent();
        //}

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[HttpPut("{id}")]
        //public async Task<ActionResult<HomeworkListResponse>> UpdateHomework(int id, UpdateHomeworkRequest updateHomeworkRequest, CancellationToken cancellationToken)
        //{
        //    var homework = await _mediator.Send(new UpdateHomework.Command(
        //           id, updateHomeworkRequest.Name, updateHomeworkRequest.Description, updateHomeworkRequest.DueDate, User.GetUserId()), cancellationToken);

        //    return homework;
        //}
    }
}
