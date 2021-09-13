using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DonVo.SignalR_MediatR.Core.Interfaces;
using DonVo.SignalR_MediatR.Core.Models.Responses;

namespace DonVo.SignalR_MediatR.Core.Queries
{
    public static class GetHomework
    {
        public record Query(string UserId, int HomeworkId) : IRequest<HomeworkDetailResponse>;

        public class Handler : IRequestHandler<Query, HomeworkDetailResponse>
        {
            private readonly IMapper _mapper;
            private readonly IHomeworkRepository _homeworkRepository;

            public Handler(IMapper mapper, IHomeworkRepository homeworkRepository)
            {
                _mapper = mapper;
                _homeworkRepository = homeworkRepository;
            }

            public async Task<HomeworkDetailResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var homework = await _homeworkRepository.GetHomeworkAsync(request.HomeworkId, request.UserId, cancellationToken);
                return _mapper.Map<HomeworkDetailResponse>(homework);
            }
        }
    }
}
