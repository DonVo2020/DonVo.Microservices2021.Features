using AutoMapper;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Commands
{
    public static class UpdateEmployee
    {
        public record Command(int EmployeeId, string DepartmentId, string Name, string Role, string Email, string Password) : IRequest<EmployeeDetailResponse>;

        public class Handler : IRequestHandler<Command, EmployeeDetailResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger, IEmployeeRepository employeeRepository, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
                _employeeRepository = employeeRepository;
                _mapper = mapper;
            }

            public async Task<EmployeeDetailResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var employee = await _employeeRepository.GetEmployeeAsync(request.EmployeeId, request.DepartmentId, cancellationToken);

                if (employee == null)
                {
                    _logger.LogWarning($"User tried to access employee: {request.EmployeeId}");
                    throw new UnauthorizedAccessException("You don't own this item");
                }

                _logger.LogInformation($"Updating employee {employee.Id} with the incoming request {request}");
                employee.Role = request.Role;
                employee.Email = request.Email;
                employee.Password = request.Password;
                employee.Name = request.Name;

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogWarning($"No changes to employee {employee.Id}");
                }

                return _mapper.Map<EmployeeDetailResponse>(employee);
            }
        }
    }
}
