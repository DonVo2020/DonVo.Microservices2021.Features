using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Commands.RemoveCommands
{
    public static class RemoveEmployee
    {
        public record Command(int EmployeeId, string DepartmentId) : IRequest;

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IEmployeeRepository _employeeRepository;
            private readonly ILogger<Handler> _logger;

            public Handler(IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository, ILogger<Handler> logger)
            {
                _unitOfWork = unitOfWork;
                _employeeRepository = employeeRepository;
                _logger = logger;
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var employee = await _employeeRepository.GetEmployeeAsync(request.EmployeeId, request.DepartmentId, cancellationToken);

                if (employee == null)
                {
                    _logger.LogWarning($"User tried to access employee: {request.EmployeeId}");
                    throw new UnauthorizedAccessException("You dont own this item");
                }

                _logger.LogInformation($"Removing employee {request}");
                _employeeRepository.RemoveEmployee(employee);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError("Failed to remove employee");
                    throw new DbUpdateException("Failed to remove employee");
                }
            }
        }
    }
}
