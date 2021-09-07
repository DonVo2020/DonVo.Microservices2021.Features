using AutoMapper;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Commands.AddCommands
{
    public static class AddEmployee
    {
        public record Command(int? Id, string Name, string Role, string Email, string Password, int DepartmentId) : IRequest<EmployeeListResponse>;

        public class Handler : IRequestHandler<Command, EmployeeListResponse>
        {
            private readonly IMapper _mapper;
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;

            public Handler(IMapper mapper, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, ILogger<Handler> logger)
            {
                _mapper = mapper;
                _employeeRepository = employeeRepository;
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            public async Task<EmployeeListResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"Creating new employee: {request}");

                var employee = new Employee()
                {
                    Name = request.Name,
                    Role = request.Role,
                    Email = request.Email,
                    Password = request.Password,
                    DepartmentId = request.DepartmentId.ToString()
                };

                await _employeeRepository.AddEmployeeAsync(employee, cancellationToken);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError("Failed to add employee");
                    throw new DbUpdateException("Failed to create employee");
                }

                return _mapper.Map<EmployeeListResponse>(employee);
            }
        }
    }
}
