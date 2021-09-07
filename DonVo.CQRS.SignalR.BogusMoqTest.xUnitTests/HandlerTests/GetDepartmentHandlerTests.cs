using AutoMapper;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Mappers;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Queries;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DonVo.CQRS.SignalR.BogusMoqTest.xUnitTests.HandlerTests
{
    public class GetDepartmentHandlerTests
    {
        private readonly Mock<IDepartmentRepository> _mockDepartmentRepository = new();
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));
        private readonly GetDepartment.Handler _department;

        public GetDepartmentHandlerTests()
        {
            _department = new GetDepartment.Handler(_mapper, _mockDepartmentRepository.Object);
        }

        [Fact]
        public async Task GetDepartment_ReturnsNull_IfDepartmentDoesNotExist()
        {
            var query = new GetDepartment.Query(1);
            var result = await _department.Handle(query,default);

            result.Should().BeNull();
        }
    }
}
