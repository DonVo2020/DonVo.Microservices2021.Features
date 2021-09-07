using AutoMapper;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Commands.AddCommands
{
    public static class AddDepartment
    {
        public record Command(string Name, int CollectionPointId, int? DepartmentHeadId, int? DepartmentRepId) : IRequest<DepartmentResponse>;

        public class Handler : IRequestHandler<Command, DepartmentResponse>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IDepartmentRepository _departmentRepository;
            private readonly ILogger<Handler> _logger;

            public Handler(IMapper mapper, IUnitOfWork unitOfWork, IDepartmentRepository departmentRepository, ILogger<Handler> logger)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _departmentRepository = departmentRepository;
                _logger = logger;
            }

            public async Task<DepartmentResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var departments = await _departmentRepository.GetDepartmentsAsync(cancellationToken);

                if(departments.Where(x => x.DepartmentHeadId == request.DepartmentHeadId || x.DepartmentRepId == request.DepartmentRepId).Any())
                {
                    _logger.LogWarning($"DepartmentHeadId: {request.DepartmentHeadId} or DepartmentRepId: {request.DepartmentRepId}");
                    throw new UnauthorizedAccessException("DepartmentId is currently existed.");
                }

                var department = new Department
                {
                    Name = request.Name,
                    CollectionPointId = request.CollectionPointId
                };

                _logger.LogInformation($"Adding department {request}");
                await _departmentRepository.AddDepartmentAsync(department, cancellationToken);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError("Failed to add department");
                    throw new DbUpdateException("Failed to add department");
                }

                return _mapper.Map<DepartmentResponse>(department);
            }
        }
    }
}
