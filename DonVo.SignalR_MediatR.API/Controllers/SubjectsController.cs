using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DonVo.SignalR_MediatR.Core.Commands.AddCommands;
using DonVo.SignalR_MediatR.Core.Extensions;
using DonVo.SignalR_MediatR.Core.Models.Requests;
using DonVo.SignalR_MediatR.Core.Models.Responses;
using DonVo.SignalR_MediatR.Core.Queries;

namespace DonVo.SignalR_MediatR.API.Controllers
{
    public class SubjectsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public SubjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectListResponse>>> GetSubjects(CancellationToken cancellationToken)
        {
            var subjects = await _mediator.Send(new GetSubjects.Query(User.GetUserId()), cancellationToken);
            return Ok(subjects);
        }

        /// <summary>
        /// Adds a new subject
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Subject
        ///     {
        ///         "name":"Math",
        ///         "description:"not so fun"
        ///     }
        ///     
        ///     POST /Subject
        ///     {
        ///         "name":"Math"
        ///     }
        /// </remarks>
        /// <param name="addSubjectRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A newly created subject</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">In case of validation errors or if the database was not able to add the item</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubjectListResponse>> AddSubject(AddSubjectRequest addSubjectRequest, CancellationToken cancellationToken)
        {
            var subject = await _mediator.Send(new AddSubject.Command(
                addSubjectRequest.Name, addSubjectRequest.Description, User.GetUserId()), cancellationToken);
            return CreatedAtRoute(nameof(GetSubject), new { SubjectId = subject.Id }, subject);
        }

        /// <summary>
        /// Adds a grade to a subject
        /// </summary>
        /// <remarks>
        /// Sample request:  
        ///     POST /Subject/grade
        ///     {
        ///         grade:"A"   
        ///     }
        /// </remarks>
        /// <param name="addGradeRequest"></param>
        /// <param name="subjectId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="204">Set the subject's grade successfully</response>
        /// <response code="404">Could not find the specified subject</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("{subjectId}/grade")]
        public async Task<ActionResult<SubjectDetailResponse>> AddSubjectGrade(AddGradeRequest addGradeRequest, int subjectId, CancellationToken cancellationToken)
        {
            var subject = await _mediator.Send(new AddSubjectGrade.Command(
                  User.GetUserId(), subjectId, addGradeRequest.Grade, addGradeRequest.DateSet, addGradeRequest.Note), cancellationToken);

            return subject;
        }

        [HttpGet("{subjectId}", Name = nameof(GetSubject))]
        public async Task<ActionResult<SubjectDetailResponse>> GetSubject(int subjectId, CancellationToken cancellationToken)
        {
            var subject = await _mediator.Send(new GetSubject.Query(User.GetUserId(), subjectId), cancellationToken);
            return subject;
        }
    }
}
