using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DonVo.SignalR_MediatR.Core.Interfaces;
using DonVo.SignalR_MediatR.Core.Models.Responses;

namespace DonVo.SignalR_MediatR.Core.Queries
{
    public static class GetExam
    {
        public record Query(string UserId, int ExamId) : IRequest<ExamDetailResponse>;

        public class Handler : IRequestHandler<Query, ExamDetailResponse>
        {
            private readonly IMapper _mapper;
            private readonly IExamRepository _examRepository;

            public Handler(IMapper mapper, IExamRepository examRepository)
            {
                _mapper = mapper;
                _examRepository = examRepository;
            }

            public async Task<ExamDetailResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var exam = await _examRepository.GetExamAsync(request.ExamId, request.UserId, cancellationToken);
                return _mapper.Map<ExamDetailResponse>(exam);
            }
        }
    }
}
