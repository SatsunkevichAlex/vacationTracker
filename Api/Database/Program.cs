using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Contracts;
using Contracts.Enums;

namespace Database
{
    class Program
    {
        // need to create 2 admins
        // need to create 2 leads
        // need to create 10 users in a team for 1 lead
        // nned to creat 5 user in a team for 2 lead
        // create 15 users without team
        static void Main()
        {
            var appContext = new ApplicationContext();
            var admins = CreateAdminUsers();
            appContext.Employees.AddRange(admins);
            appContext.SaveChanges();

            var leads = CreateLeadUsers();
            appContext.Employees.AddRange(leads);
            appContext.SaveChanges();

            var teamUsers = CreateUsersForTeams(appContext);
            appContext.Employees.AddRange(teamUsers);
            appContext.SaveChanges();

            var nonTeamUsers = CreateUsers(15);
            appContext.Employees.AddRange(nonTeamUsers);
            appContext.SaveChanges();
        }

        static List<Employee> CreateAdminUsers()
        {
            var result = new List<Employee>();

            var admin1 = CreateAdminUser();
            var admin2 = CreateAdminUser();

            result.Add(admin1);
            result.Add(admin2);

            return result;
        }

        static List<Employee> CreateUsersForTeams(ApplicationContext appContext)
        {
            var leads = appContext.Employees
                .Where(it => it.Role == UserRole.Lead)
                .Take(2)
                .ToList();

            var usersFor1Lead = CreateUsers(10);
            usersFor1Lead.ForEach(it => it.LeadId = leads[0].Id);

            var usersFor2Lead = CreateUsers(5);
            usersFor2Lead.ForEach(it => it.LeadId = leads[1].Id);

            var result = new List<Employee>();
            result.AddRange(usersFor1Lead);
            result.AddRange(usersFor2Lead);
            return result;
        }

        static List<Employee> CreateLeadUsers()
        {
            var result = new List<Employee>();

            var lead1 = CreateLeadUser();
            var lead2 = CreateLeadUser();

            result.Add(lead1);
            result.Add(lead2);

            return result;
        }

        static List<Employee> CreateUsers(int numberToCreate)
        {
            var autoFixture = new Fixture();
            var users = new List<Employee>();
            for (int i = 0; i < numberToCreate; i++)
            {
                var user = autoFixture.Build<Employee>()
                    .Without(it => it.Id)
                    .Without(it => it.LeadId)
                    .With(it => it.Role, UserRole.Regular)
                    .Create();
                users.Add(user);
            }
            return users;
        }

        static Employee CreateAdminUser()
        {
            var autoFixture = new Fixture();
            return autoFixture
                .Build<Employee>()
                .With(it => it.Role, UserRole.Admin)
                .Without(it => it.Id)
                .Without(it => it.LeadId)
                .Create();
        }

        static Employee CreateLeadUser()
        {
            var autoFixture = new Fixture();
            return autoFixture
                .Build<Employee>()
                .With(it => it.Role, UserRole.Lead)
                .Without(it => it.Id)
                .Without(it => it.LeadId)
                .Create();
        }
    }
}
