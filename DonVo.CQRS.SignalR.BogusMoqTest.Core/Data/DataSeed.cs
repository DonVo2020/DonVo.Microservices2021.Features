using Bogus;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Data
{
    public static class DataSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, DataContext context)
        {
            if (await userManager.Users.AnyAsync())
            {
                return;
            }

            var users = new Faker<AppUser>()
                 .RuleFor(x => x.Email, x => x.Person.Email)
                 .Generate(3).ToList();

            users.Add(new AppUser { Email = "christian@gmail.com" });
            users.Add(new AppUser { Email = "lester@gmail.com" });

            foreach (var user in users)
            {
                user.UserName = user.Email;
                await userManager.CreateAsync(user, "Password123");
            }

            var departments = CreateDepartmentGenerator()
                 //.RuleFor(x => x.CollectionPointId, x => users[x.Random.Number(0, users.Count - 1)].Id)
                 .Generate(5).ToList();
            await context.Departments.AddRangeAsync(departments);

            foreach (var item in departments)
            {
                var collectionPoint = CreateCollectionPointGenerator().Generate();
                var employee = CreateEmployeeGenerator().Generate();

                collectionPoint.Id = item.CollectionPointId;
                employee.Id = collectionPoint.EmployeeId ?? collectionPoint.EmployeeId.Value;

                await context.CollectionPoints.AddAsync(collectionPoint);
                await context.Employees.AddAsync(employee);
            }

            var employees = CreateEmployeeGenerator()
                .Generate(5).ToList();
            await context.Employees.AddRangeAsync(employees);

            var suppliers = CreateSupplierGenerator()
                .Generate(5).ToList();
            await context.Suppliers.AddRangeAsync(suppliers);

            //var reminders = CreateCollectionPointGenerator()
            //    //.RuleFor(x => x.UserId, x => users[x.Random.Number(0, users.Count - 1)].Id)
            //    .Generate(5).ToList();

            await context.SaveChangesAsync();
        }

        public static Faker<Department> CreateDepartmentGenerator()
        {
            return new Faker<Department>()
               .RuleFor(x => x.Name, x => x.Company.CompanyName())
               .RuleFor(x => x.CollectionPointId, x => x.Random.Int());
        }

        public static Faker<Supplier> CreateSupplierGenerator()
        {
            return new Faker<Supplier>()
                .RuleFor(x => x.Name, x => x.Random.Words(10))
                .RuleFor(x => x.Address, x => x.Random.Words(20))
                .RuleFor(x => x.ContactName, x => x.Random.Words(10))
                .RuleFor(x => x.FaxNo, x => x.Random.Int(10).ToString())
                .RuleFor(x => x.PhoneNo, x => x.Random.Int(10).ToString())
                .RuleFor(x => x.GSTReg, x => x.Random.Words(15))
                .RuleFor(x => x.Name, x => x.Random.Words(10));
        }

        public static Faker<Employee> CreateEmployeeGenerator()
        {
            return new Faker<Employee>()
                 .RuleFor(x => x.Name, x => x.Person.FullName)
                 .RuleFor(x => x.Email, x => x.Person.Email)
                .RuleFor(x => x.Password, "Password123")
                .RuleFor(x => x.Role, x => x.Random.Words(5));
        }

        public static Faker<CollectionPoint> CreateCollectionPointGenerator()
        {
            return new Faker<CollectionPoint>()
                .RuleFor(x => x.Name, x => x.Finance.AccountName())
                .RuleFor(x => x.Time, x => x.Date.Future().ToLongTimeString())
                .RuleFor(x => x.EmployeeId, x => x.Random.Int());
        }
    }
}
