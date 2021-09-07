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
    public static class UpdateDepartment
    {
        public record Command(int DepartmentId, string Name, int CollectionPointId, int? DepartmentHeadId, int? DepartmentRepId) : IRequest<DepartmentDetailResponse>;

        public class Handler : IRequestHandler<Command, DepartmentDetailResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger, IDepartmentRepository departmentRepository, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
                _departmentRepository = departmentRepository;
                _mapper = mapper;
            }

            public async Task<DepartmentDetailResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var department = await _departmentRepository.GetDepartmentAsync(request.DepartmentId, cancellationToken);

                if (department == null)
                {
                    _logger.LogWarning($"User tried to access Department: {request.DepartmentId}");
                    throw new UnauthorizedAccessException("You don't own this item");
                }

                _logger.LogInformation($"Updating department {department.Id} with the incoming request {request}");
                department.CollectionPointId = request.CollectionPointId;
                department.DepartmentHeadId = request.DepartmentHeadId;
                department.DepartmentRepId = request.DepartmentRepId;
                department.Name = request.Name;

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogWarning($"No changes to Department {department.Id}");
                }

                return _mapper.Map<DepartmentDetailResponse>(department);
            }
        }
    }
}
