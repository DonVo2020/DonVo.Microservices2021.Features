using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using DonVo.SignalR_MediatR.Core.Data;
using DonVo.SignalR_MediatR.Core.Interfaces;
using DonVo.SignalR_MediatR.Core.Repositories;
using Xunit;
namespace DonVo.SignalR_MediatR.xUnitTests.RepositoryTests
{
    public class ExamRepositoryTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly string _userId = Guid.NewGuid().ToString();
        private readonly int _subjectId = 1;
        private readonly int _examId;

        private readonly IExamRepository _subject;

        public ExamRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("exams");
            _context = new DataContext(contextOptions.Options);
            _subject = new ExamRepository(_context);

            //random exams
            var exams = DataSeed.CreateExamGenerator()
                .RuleFor(x => x.UserId, Guid.NewGuid().ToString())
                .RuleFor(x => x.SubjectId, x => x.Random.Number())
                .Generate(5);

            //user owned exams
            var userExams = DataSeed.CreateExamGenerator()
                .RuleFor(x => x.UserId, _userId)
                .RuleFor(x => x.SubjectId, _subjectId)
                .Generate(5);

            _context.Exams.AddRange(exams);
            _context.Exams.AddRange(userExams);
            _context.SaveChanges();

            _examId = userExams.FirstOrDefault().Id;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task GetExam_ReturnsExam_When_Exists()
        {
            var exam = await _subject.GetExamAsync(_examId, _userId);

            exam.Should().NotBeNull();
            exam.UserId.Should().Be(_userId);
        }

        [Fact]
        public async Task GetExams_ReturnsExams_With_TheSameSubjectId()
        {
            var exams = await _subject.GetExamsAsync(_userId, _subjectId);

            exams.Should().OnlyContain(x => x.SubjectId == _subjectId);
        }
    }
}
