using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Queries
{
    public static class GetDepartments
    {
        public record Query() : IRequest<IEnumerable<DepartmentResponse>>;

        public class Handler : IRequestHandler<Query, IEnumerable<DepartmentResponse>>
        {
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IMapper _mapper;

            public Handler(IDepartmentRepository departmentRepository, IMapper mapper)
            {
                _departmentRepository = departmentRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<DepartmentResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var departments = await _departmentRepository.GetDepartmentsAsync(cancellationToken);

                return _mapper.Map<IEnumerable<DepartmentResponse>>(departments);
            }
        }
    }
}
