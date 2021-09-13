using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using DonVo.SignalR_MediatR.Core.Commands;
using DonVo.SignalR_MediatR.Core.Interfaces;
using DonVo.SignalR_MediatR.Core.Mappers;
using DonVo.SignalR_MediatR.Core.Models.Entities;
using Xunit;

namespace DonVo.SignalR_MediatR.xUnitTests.HandlerTests
{
    public class UpdateExamTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<IExamRepository> _examRepository = new();
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));
        private readonly Mock<ILogger<UpdateExam.Handler>> _logger = new();

        private readonly UpdateExam.Handler _subject;


        public UpdateExamTests()
        {
            _unitOfWork.Setup(_ => _.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
            _subject = new UpdateExam.Handler(_unitOfWork.Object, _logger.Object, _examRepository.Object, _mapper);
        }

        [Fact]
        public async Task UpdateExam_ThrowsUnauthorized_When_ExamDoesNotExist()
        {
            var command = new UpdateExam.Command(default, string.Empty, string.Empty, DateTime.Now, string.Empty);

            await _subject.Invoking(_ => _.Handle(command, default))
                .Should().ThrowExactlyAsync<UnauthorizedAccessException>();
        }


        [Fact]
        public async Task UpdateExam_Returns_UpdatedExam()
        {
            //arrange
            var command = new UpdateExam.Command(1, "updated", "updated", DateTime.UtcNow, Guid.NewGuid().ToString());
            var exam = new Exam { Id = command.ExamId, Name = "old", Description = "old", DueDate = command.DueDate.AddDays(-1) };

            _examRepository.Setup(_ => _.GetExamAsync(command.ExamId, command.UserId, default))
                .ReturnsAsync(exam);

            //act
            var updatedExam = await _subject.Handle(command, default);

            //assert
            updatedExam.Name.Should().BeEquivalentTo(command.Name);
            updatedExam.Description.Should().BeEquivalentTo(command.Description);
            updatedExam.DueDate.Should().Be(command.DueDate);

            _examRepository.Verify(_ => _.GetExamAsync(command.ExamId, command.UserId, default), Times.Once);
        }
    }
}
