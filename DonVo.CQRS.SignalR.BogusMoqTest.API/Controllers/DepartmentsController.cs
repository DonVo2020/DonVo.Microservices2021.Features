using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Commands.AddCommands;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Extensions;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Requests;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Queries;

namespace DonVo.CQRS.SignalR.BogusMoqTest.API.Controllers
{
    public class DepartmentsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentResponse>>> GetDepartments(CancellationToken cancellationToken)
        {
            var departments = await _mediator.Send(new GetDepartments.Query());
            return Ok(departments);
        }

        /// <summary>
        /// Adds a new Department
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Department
        ///     {
        ///         "name":"Math",
        ///         "description:"not so fun"
        ///     }
        ///     
        ///     POST /Department
        ///     {
        ///         "name":"Math"
        ///     }
        /// </remarks>
        /// <param name="addDepartmentRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A newly created Department</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">In case of validation errors or if the database was not able to add the item</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DepartmentResponse>> AddDepartment(AddDepartmentRequest addDepartmentRequest, CancellationToken cancellationToken)
        {
            var department = await _mediator.Send(new AddDepartment.Command(
                addDepartmentRequest.Name, addDepartmentRequest.CollectionPointId, addDepartmentRequest.DepartmentHeadId, addDepartmentRequest.DepartmentRepId), cancellationToken);
            return CreatedAtRoute(nameof(GetDepartment), new { DepartmentId = department.Id }, department);
        }

        ///// <summary>
        ///// Adds a grade to a Department
        ///// </summary>
        ///// <remarks>
        ///// Sample request:  
        /////     POST /Department/grade
        /////     {
        /////         grade:"A"   
        /////     }
        ///// </remarks>
        ///// <param name="addGradeRequest"></param>
        ///// <param name="DepartmentId"></param>
        ///// <param name="cancellationToken"></param>
        ///// <returns></returns>
        ///// <response code="204">Set the Department's grade successfully</response>
        ///// <response code="404">Could not find the specified Department</response>
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[HttpPost("{DepartmentId}/grade")]
        //public async Task<ActionResult<DepartmentResponse>> AddDepartmentGrade(AddGradeRequest addGradeRequest, int DepartmentId, CancellationToken cancellationToken)
        //{
        //    var Department = await _mediator.Send(new AddDepartmentGrade.Command(
        //          User.GetUserId(), DepartmentId, addGradeRequest.Grade, addGradeRequest.DateSet, addGradeRequest.Note), cancellationToken);

        //    return Department;
        //}

        [HttpGet("{DepartmentId}", Name = nameof(GetDepartment))]
        public async Task<ActionResult<DepartmentDetailResponse>> GetDepartment(int departmentId, CancellationToken cancellationToken)
        {
            var department = await _mediator.Send(new GetDepartment.Query(departmentId), cancellationToken);
            return department;
        }
    }
}
