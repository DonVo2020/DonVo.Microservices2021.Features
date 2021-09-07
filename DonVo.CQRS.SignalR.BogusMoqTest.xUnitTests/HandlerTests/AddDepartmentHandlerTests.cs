using AutoMapper;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Commands.AddCommands;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Mappers;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DonVo.CQRS.SignalR.BogusMoqTest.xUnitTests.HandlerTests
{
    public class AddDepartmentHandlerTests
    {
        private readonly Mock<IDepartmentRepository> _departmentRepository = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ILogger<AddDepartment.Handler>> _addHomeworkLogger = new();
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));

        private readonly AddDepartment.Handler _department;

        public AddDepartmentHandlerTests()
        {
            _unitOfWork.Setup(_ => _.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
            _department = new AddDepartment.Handler(_mapper, _unitOfWork.Object, _departmentRepository.Object, _addHomeworkLogger.Object);
        }

        [Fact]
        public async Task AddDepartment()
        {
            var command = new AddDepartment.Command("Computer Science", 1, 6, 9);
            Department savedDepartment = null;

            _departmentRepository.Setup(_ => _.AddDepartmentAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()))
                .Callback<Department, CancellationToken>((department, y) => savedDepartment = department);

            var newDepartment = await _department.Handle(command, default);
            newDepartment.Name.Should().Be(savedDepartment.Name);
        }
    }
}
