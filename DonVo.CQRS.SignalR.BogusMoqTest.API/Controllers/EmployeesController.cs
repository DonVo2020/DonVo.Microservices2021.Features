using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
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

    public class EmployeesController : BaseApiController
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveEmployee(int id)
        {
            await _mediator.Send(new RemoveEmployee.Command(id, User.GetUserId()));
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeDetailResponse>> UpdateEmployee(int id, UpdateEmployeeRequest updateEmployeeRequest, CancellationToken cancellationToken)
        {
            var Employee = await _mediator.Send(new UpdateEmployee.Command(
                   id, updateEmployeeRequest.departmentId, updateEmployeeRequest.Name, updateEmployeeRequest.Role, 
                   updateEmployeeRequest.Email, updateEmployeeRequest.Password), cancellationToken);

            return Employee;
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDetailResponse>> AddEmployee(AddEmployeeRequest addEmployeeRequest, CancellationToken cancellationToken)
        {
            var Employee = await _mediator.Send(new AddEmployee.Command(null,
                    addEmployeeRequest.Name, addEmployeeRequest.Role, addEmployeeRequest.Email, addEmployeeRequest.Password, addEmployeeRequest.DepartmentId), cancellationToken);

            return CreatedAtRoute(nameof(GetEmployee), new { Employee.Id }, Employee);
        }

        [HttpGet("{id}", Name = nameof(GetEmployee))]
        public async Task<ActionResult<EmployeeDetailResponse>> GetEmployee(int id)
        {
            var Employee = await _mediator.Send(new GetEmployee.Query(User.GetUserId(), id));
            return Employee;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<EmployeeListResponse>>> GetEmployees([FromQuery] int? subjectId, CancellationToken cancellationToken)
        //{
        //    var Employees = await _mediator.Send(
        //        new GetEmployees.Query(subjectId, User.GetUserId()), cancellationToken);
        //    return Ok(Employees);
        //}
    }
}
