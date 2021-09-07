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
    public class DepartmentRepositoryTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly string _DepartmentId;

        private readonly IDepartmentRepository _department;

        public DepartmentRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("Departments");
            _context = new DataContext(contextOptions.Options);
            _department = new DepartmentRepository(_context);

            //random Departments
            var Departments = DataSeed.CreateDepartmentGenerator()
                .RuleFor(x => x.Id, x => x.Random.Number(1, 1000).ToString())
                .RuleFor(x => x.DepartmentHeadId, x => x.Random.Number(13))
                .RuleFor(x => x.DepartmentRepId, x => x.Random.Number(13))
                .RuleFor(x => x.CollectionPointId, x => x.Random.Number(2))
                .RuleFor(x => x.Name, x => x.Person.FullName)
                .Generate(5);

            //other random Departments again
            var departments = DataSeed.CreateDepartmentGenerator()
                .RuleFor(x => x.Id, x => x.Random.Number(1010,5100).ToString())
                .RuleFor(x => x.DepartmentHeadId, x => x.Random.Number(117,127))
                .RuleFor(x => x.DepartmentRepId, x => x.Random.Number(217, 227))
                .RuleFor(x => x.CollectionPointId, x => x.Random.Number(2))
                .RuleFor(x => x.Name, x => x.Person.FullName)
                .Generate(5);

            _context.Departments.AddRange(Departments);
            _context.Departments.AddRange(departments);
            _context.SaveChanges();

            _DepartmentId = Departments.FirstOrDefault().Id;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task GetDepartment_ReturnsDepartment_When_Exists()
        {
            var Department = await _department.GetDepartmentAsync(Convert.ToInt32(_DepartmentId));

            Department.Should().NotBeNull();
            Department.Id.Should().Be(_DepartmentId);
        }

        [Fact]
        public async Task GetDepartments_ReturnsDepartments_With_TheSameSubjectId()
        {
            var departments = await _department.GetDepartmentsAsync();

            departments.Should().NotBeNull();
            departments.Count().Equals(10);
        }

        [Fact]
        public async Task RemoveDepartment_ReturnsDepartment_When_Exists()
        {
            var department = await _department.GetDepartmentAsync(Convert.ToInt32(_DepartmentId));
            var departments = new List<Department>();
            departments.Add(department);
            department.Should().NotBeNull();
            departments.Count.Equals(1);
            _department.RemoveDepartment(department);
            departments.Count.Equals(0);
        }
    }
}
