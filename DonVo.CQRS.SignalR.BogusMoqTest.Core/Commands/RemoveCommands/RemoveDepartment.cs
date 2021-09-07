using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Commands.RemoveCommands
{
    public static class RemoveDepartment
    {
        public record Command(int DepartmentId) : IRequest;

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;

            public Handler(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork, ILogger<Handler> logger)
            {
                _departmentRepository = departmentRepository;
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var department = await _departmentRepository.GetDepartmentAsync(request.DepartmentId, cancellationToken);

                if (department == null)
                {
                    _logger.LogWarning($"User tried to access Department: {request.DepartmentId}");
                    throw new UnauthorizedAccessException("You dont own this item");
                }

                _logger.LogInformation($"Removing Department {request}");
                _departmentRepository.RemoveDepartment(department);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError("Failed to remove Department");
                    throw new DbUpdateException("Failed to remove Department");
                }
            }
        }
    }
}
