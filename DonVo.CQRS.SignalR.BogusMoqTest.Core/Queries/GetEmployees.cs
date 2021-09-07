using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Queries
{
    public static class GetEmployees
    {
        public record Query(int? DepartmentId) : IRequest<IEnumerable<EmployeeListResponse>>;

        public class Handler : IRequestHandler<Query, IEnumerable<EmployeeListResponse>>
        {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMapper _mapper;

            public Handler(IEmployeeRepository employeeRepository, IMapper mapper)
            {
                _employeeRepository = employeeRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<EmployeeListResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var homeworks = await _employeeRepository.GetEmployeesAsync(request.DepartmentId, cancellationToken);

                return _mapper.Map<IEnumerable<EmployeeListResponse>>(homeworks);
            }
        }
    }
}
