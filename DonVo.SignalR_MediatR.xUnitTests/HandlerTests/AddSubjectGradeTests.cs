using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using DonVo.SignalR_MediatR.Core.Commands.AddCommands;
using DonVo.SignalR_MediatR.Core.Interfaces;
using DonVo.SignalR_MediatR.Core.Mappers;
using DonVo.SignalR_MediatR.Core.Models.Entities;
using Xunit;

namespace DonVo.SignalR_MediatR.xUnitTests.HandlerTests
{
    public class AddSubjectGradeTests
    {
        private readonly Mock<ISubjectRepository> _subjectRepository = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ILogger<AddSubjectGrade.Handler>> _logger = new();
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));

        private readonly AddSubjectGrade.Handler _subject;

        public AddSubjectGradeTests()
        {
            _unitOfWork.Setup(_ => _.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
            _subject = new AddSubjectGrade.Handler(_subjectRepository.Object, _unitOfWork.Object, _logger.Object, _mapper);
        }

        [Fact]
        public async Task AddSubjectGrade_AddsGradeToSubject()
        {
            var command = new AddSubjectGrade.Command("userid", 1, "A", DateTime.UtcNow, "Could have done better");
            var mockSubject = new Subject();

            _subjectRepository.Setup(_ => _.GetSubjectByIdAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockSubject);

            // act
            await _subject.Handle(command, default);

            // assert

            mockSubject.Grade.Value.Should().Be(command.Grade);
        }

        [Fact]
        public async Task AddSubjectGrade_ThrowsKeyNotFound_WhenSubjectDoesNotExist()
        {
            var command = new AddSubjectGrade.Command(default, default, default, default, default);

            _subjectRepository.Setup(_ => _.GetSubjectByIdAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(() => null);

            await _subject.Invoking(x => x.Handle(command, default))
                .Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
