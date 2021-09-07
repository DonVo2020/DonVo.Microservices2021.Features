using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Queries
{
    public static class GetEmployee
    {
        public record Query(string DepartmentId, int EmployeeId) : IRequest<EmployeeDetailResponse>;

        public class Handler : IRequestHandler<Query, EmployeeDetailResponse>
        {
            private readonly IMapper _mapper;
            private readonly IEmployeeRepository _employeeRepository;

            public Handler(IMapper mapper, IEmployeeRepository employeeRepository)
            {
                _mapper = mapper;
                _employeeRepository = employeeRepository;
            }

            public async Task<EmployeeDetailResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var exam = await _employeeRepository.GetEmployeeAsync(request.EmployeeId, request.DepartmentId, cancellationToken);
                return _mapper.Map<EmployeeDetailResponse>(exam);
            }
        }
    }
}
