using System;
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
    public class AddHomeworkTests
    {
        private readonly Mock<ISubjectRepository> _subjectRepository = new();
        private readonly Mock<IHomeworkRepository> _homeworkRepository = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ILogger<AddHomework.Handler>> _addHomeworkLogger = new();
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));

        private readonly AddHomework.Handler _subject;

        public AddHomeworkTests()
        {
            _unitOfWork.Setup(_ => _.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
            _subject = new AddHomework.Handler(_mapper, _unitOfWork.Object, _subjectRepository.Object, _addHomeworkLogger.Object, _homeworkRepository.Object);
        }

        [Fact]
        public async Task AddHomework_ThrowsUnauthorized_When_UserDoesNotOwnSubject()
        {
            var command = new AddHomework.Command("fluentassertions week 2", "", DateTime.UtcNow, 1, Guid.NewGuid().ToString());

            _subjectRepository.Setup(_ => _.SubjectExistsAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            await _subject.Invoking(_ => _.Handle(command, default))
                .Should().ThrowAsync<UnauthorizedAccessException>();
        }

        [Fact]
        public async Task AddHomework_AddsHomeworkToTheSubject_WhenSubjectExists()
        {
            var command = new AddHomework.Command("Read up on penguins", string.Empty, DateTime.Now, 1, Guid.NewGuid().ToString());
            Homework savedHomework = null;

            _subjectRepository.Setup(_ => _.SubjectExistsAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _homeworkRepository.Setup(_ => _.AddHomeworkAsync(It.IsAny<Homework>(), It.IsAny<CancellationToken>()))
                .Callback<Homework, CancellationToken>((homework, y) => savedHomework = homework);

           var newHomework = await _subject.Handle(command, default);

            newHomework.Id.Should().Be(savedHomework.Id);
            savedHomework.SubjectId.Should().Be(command.SubjectId);
        }

        [Fact]
        public async Task AddHomework_Returns_NewHomework_IfSubject_Exists()
        {
            var homeworkToAdd = new AddHomework.Command("Read up on penguins", string.Empty, DateTime.Now, 1, Guid.NewGuid().ToString());

            _subjectRepository.Setup(_ => _.SubjectExistsAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var newHomework = await _subject.Handle(homeworkToAdd, default);

            newHomework.Should().NotBeNull();
            newHomework.Name.Should().BeSameAs(homeworkToAdd.Name);
        }
    }
}
