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
    public class GetDepartmentsHandlerTests
    {
        private readonly Mock<IDepartmentRepository> _mockDepartmentRepository = new();
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));
        private readonly GetDepartments.Handler _department;

        public GetDepartmentsHandlerTests()
        {
            _department = new GetDepartments.Handler(_mockDepartmentRepository.Object,_mapper);
        }

        [Fact]
        public async Task GetDepartment_ReturnsNull_IfDepartmentDoesNotExist()
        {
            var query = new GetDepartments.Query();
            var result = await _department.Handle(query,default);

            result.Should().NotBeNull();
        }
    }
}
