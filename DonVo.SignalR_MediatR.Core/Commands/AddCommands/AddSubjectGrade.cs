using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DonVo.SignalR_MediatR.Core.Interfaces;
using DonVo.SignalR_MediatR.Core.Models.Entities;
using DonVo.SignalR_MediatR.Core.Models.Responses;

namespace DonVo.SignalR_MediatR.Core.Commands.AddCommands
{
    public static class AddSubjectGrade
    {
        public record Command(string UserId, int SubjectId, string Grade, DateTime DateSet, string Note) : IRequest<SubjectDetailResponse>;

        public class Handler : IRequestHandler<Command, SubjectDetailResponse>
        {
            private readonly ISubjectRepository _subjectRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;
            private readonly IMapper _mapper;

            public Handler(ISubjectRepository subjectRepository, IUnitOfWork unitOfWork, ILogger<Handler> logger, IMapper mapper)
            {
                _subjectRepository = subjectRepository;
                _unitOfWork = unitOfWork;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<SubjectDetailResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var subject = await _subjectRepository.GetSubjectByIdAsync(request.SubjectId, request.UserId, cancellationToken);
                if (subject == null)
                {
                    throw new KeyNotFoundException("Could not find subject");
                }

                subject.Grade = new Grade(request.Grade, request.DateSet, request.Note);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError($"Failed to apply grade in request: {request}");
                    throw new DbUpdateException("Failed to add grade");
                }

                return _mapper.Map<SubjectDetailResponse>(subject);
            }
        }

    }
}
