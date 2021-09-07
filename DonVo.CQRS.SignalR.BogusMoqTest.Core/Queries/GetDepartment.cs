using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Queries
{
    public static class GetDepartment
    {
        public record Query(int DepartmentId) : IRequest<DepartmentDetailResponse>;

        public class Handler : IRequestHandler<Query, DepartmentDetailResponse>
        {
            private readonly IMapper _mapper;
            private readonly IDepartmentRepository _departmentRepository;

            public Handler(IMapper mapper, IDepartmentRepository departmentRepository)
            {
                _mapper = mapper;
                _departmentRepository = departmentRepository;
            }

            public async Task<DepartmentDetailResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var department = await _departmentRepository.GetDepartmentAsync(request.DepartmentId, cancellationToken);
                return _mapper.Map<DepartmentDetailResponse>(department);
            }
        }
    }
}
