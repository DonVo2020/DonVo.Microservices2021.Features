using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DonVo.SignalR_MediatR.Core.Helpers;
using DonVo.SignalR_MediatR.Core.Models.Entities;

namespace DonVo.SignalR_MediatR.Core.Data
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

            var subjects = CreateSubjectGenerator()
                 .RuleFor(x => x.UserId, x => users[x.Random.Number(0, users.Count - 1)].Id)
                 .Generate(5).ToList();
            await context.Subjects.AddRangeAsync(subjects);

            foreach (var item in subjects)
            {
                var exam = CreateExamGenerator().Generate();
                var homework = CreateHomeworkGenerator().Generate();

                exam.UserId = item.UserId;
                exam.SubjectId = item.Id;
                homework.SubjectId = item.Id;
                homework.UserId = item.UserId;
            }

            var reminders = CreateReminderGenerator()
                .RuleFor(x => x.UserId, x => users[x.Random.Number(0, users.Count - 1)].Id)
                .Generate(5).ToList();

            await context.Reminders.AddRangeAsync(reminders);
            await context.SaveChangesAsync();
        }

        public static Faker<Subject> CreateSubjectGenerator()
        {
            return new Faker<Subject>()
               .RuleFor(x => x.Name, x => x.Name.JobTitle())
               .RuleFor(x => x.Description, x => x.Lorem.Sentence());
        }

        public static Faker<Reminder> CreateReminderGenerator()
        {
            return new Faker<Reminder>()
                .RuleFor(x => x.Name, x => x.Lorem.Sentence())
                .RuleFor(x => x.Priority, x => x.Random.Enum<Priority>());
        }

        public static Faker<Exam> CreateExamGenerator()
        {
            return new Faker<Exam>()
                 .RuleFor(x => x.Name, x => x.Name.JobTitle())
                 .RuleFor(x => x.Description, x => x.Lorem.Sentence())
                 .RuleFor(x => x.DueDate, x => x.Date.Future());
        }

        public static Faker<Homework> CreateHomeworkGenerator()
        {
            return new Faker<Homework>()
                .RuleFor(x => x.Name, x => x.Name.JobTitle())
                .RuleFor(x => x.Description, x => x.Lorem.Sentence())
                .RuleFor(x => x.DueDate, x => x.Date.Future());
        }
    }
}
