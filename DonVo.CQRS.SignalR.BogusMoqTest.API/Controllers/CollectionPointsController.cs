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
    }
}
