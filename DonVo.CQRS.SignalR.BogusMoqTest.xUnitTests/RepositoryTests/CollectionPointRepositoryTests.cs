using DonVo.CQRS.SignalR.BogusMoqTest.Core.Data;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
namespace DonVo.CQRS.SignalR.BogusMoqTest.xUnitTests.RepositoryTests
{
    public class CollectionPointRepositoryTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly int _collectionPointId;
        private readonly List<CollectionPoint> _collectionPoints;

        private readonly ICollectionPointRepository _collectionPointRepository;

        public CollectionPointRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("CollectionPoints");
            _context = new DataContext(contextOptions.Options);
            _collectionPointRepository = new CollectionPointRepository(_context);

            //random CollectionPoints
            _collectionPoints = DataSeed.CreateCollectionPointGenerator()
                .RuleFor(x => x.Id, x => x.Random.Number(1010,5100))
                .RuleFor(x => x.EmployeeId, x => x.Random.Number(1,5))
                .RuleFor(x => x.Name, x => x.Random.Words())
                .RuleFor(x => x.Time, x => x.Date.Future().ToLongTimeString())
                .Generate(5);

            _context.CollectionPoints.AddRange(_collectionPoints);
            _context.SaveChanges();

            _collectionPointId = _collectionPoints.FirstOrDefault().Id;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task GetDepartment_ReturnsDepartment_When_Exists()
        {
            var collectionPoint = await _collectionPointRepository.GetCollectionPointAsync(_collectionPoints.FirstOrDefault().EmployeeId.Value, _collectionPointId);

            collectionPoint.Should().NotBeNull();
            collectionPoint.Id.Should().Be(_collectionPointId);
        }

        [Fact]
        public async Task GetDepartments_ReturnsDepartments_With_TheSameSubjectId()
        {
            var collectionPoints = await _collectionPointRepository.GetCollectionPointsAsync(_collectionPoints.FirstOrDefault().EmployeeId.Value, _collectionPoints.Select(x=>x.Id));

            collectionPoints.Should().NotBeNull();
            collectionPoints.Count.Equals(2);
        }
    }
}
