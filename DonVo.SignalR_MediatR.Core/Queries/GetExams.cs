using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DonVo.SignalR_MediatR.Core.Interfaces;
using DonVo.SignalR_MediatR.Core.Models.Responses;

namespace DonVo.SignalR_MediatR.Core.Queries
{
    public static class GetExams
    {
        public record Query(int? SubjectId, string UserId) : IRequest<IEnumerable<ExamListResponse>>;

        public class Handler : IRequestHandler<Query, IEnumerable<ExamListResponse>>
        {
            private readonly IExamRepository _examRepository;
            private readonly IMapper _mapper;

            public Handler(IExamRepository examRepository, IMapper mapper)
            {
                _examRepository = examRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ExamListResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var homeworks = await _examRepository.GetExamsAsync(request.UserId, request.SubjectId, cancellationToken);

                return _mapper.Map<IEnumerable<ExamListResponse>>(homeworks);
            }
        }
    }
}
