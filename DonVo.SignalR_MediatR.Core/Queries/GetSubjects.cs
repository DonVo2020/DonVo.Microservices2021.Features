using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using DonVo.SignalR_MediatR.Core.Interfaces;
using DonVo.SignalR_MediatR.Core.Models.Responses;

namespace DonVo.SignalR_MediatR.Core.Queries
{
    public static class GetSubjects
    {
        public record Query(string UserId) : IRequest<IEnumerable<SubjectListResponse>>;

        public class Handler : IRequestHandler<Query, IEnumerable<SubjectListResponse>>
        {
            private readonly ISubjectRepository _subjectRepository;
            private readonly ILogger<Handler> _logger;
            private readonly IMapper _mapper;

            public Handler(ISubjectRepository subjectRepository, ILogger<Handler> logger, IMapper mapper)
            {
                _subjectRepository = subjectRepository;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<IEnumerable<SubjectListResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"Getting subjects");
                var subjects = await _subjectRepository.GetSubjectsAsync(request.UserId, cancellationToken);
                return _mapper.Map<IEnumerable<SubjectListResponse>>(subjects);
            }
        }
    }
}
