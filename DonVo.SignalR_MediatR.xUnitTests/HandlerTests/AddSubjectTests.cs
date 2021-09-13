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
using Xunit;

namespace DonVo.SignalR_MediatR.xUnitTests.HandlerTests
{
    public class AddSubjectTests
    {
        private readonly Mock<ISubjectRepository> _subjectRepository = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ILogger<AddSubject.Handler>> _addSubjectLogger = new();
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));

        //This is our subject we want to test
        private readonly AddSubject.Handler _subject;

        public AddSubjectTests()
        {
            _unitOfWork.Setup(_ => _.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
            _subject = new AddSubject.Handler(_mapper, _subjectRepository.Object, _unitOfWork.Object, _addSubjectLogger.Object);
        }

        [Fact]
        public async Task AddSubject_ReturnsNewlyCreatedSubject()
        {
            //arrange
            var command = new AddSubject.Command("Math", "Not so fun", Guid.NewGuid().ToString());

            //act
            var newSubject = await _subject.Handle(command, default);

            //assert
            _unitOfWork.Verify(_ => _.SaveChangesAsync(default), Times.Once);

            newSubject.Should().NotBeNull();
            newSubject.Name.Should().BeEquivalentTo(command.Name);
        }
    }
}
