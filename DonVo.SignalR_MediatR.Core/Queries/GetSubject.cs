using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DonVo.SignalR_MediatR.Core.Interfaces;
using DonVo.SignalR_MediatR.Core.Models.Responses;

namespace DonVo.SignalR_MediatR.Core.Queries
{
    public static class GetSubject
    {
        public record Query(string UserId, int Subjectid) : IRequest<SubjectDetailResponse>;

        public class Handler : IRequestHandler<Query, SubjectDetailResponse>
        {
            private readonly ISubjectRepository _subjectRepository;
            private readonly IMapper _mapper;

            public Handler(ISubjectRepository subjectRepository, IMapper mapper)
            {
                _subjectRepository = subjectRepository;
                _mapper = mapper;
            }

            public async Task<SubjectDetailResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var subject = await _subjectRepository.GetSubjectByIdAsync(request.Subjectid, request.UserId, cancellationToken);
                return _mapper.Map<SubjectDetailResponse>(subject);
            }
        }
    }
}
