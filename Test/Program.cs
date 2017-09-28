using System;
using System.Linq;
using DAL.Context;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new JobPortalDbContext())
            {
                var companies = context.Employers.OrderBy(emp => emp.Name).ToList();
                // there should be 3 companies
                foreach (var company in companies)
                {
                    context.Entry(company).Reference(c => c.Address).Load();
                    Console.WriteLine(company.Name + " : " + company.Address.City);
                }

                var registeredUsers = context.Users.OrderBy(emp => emp.Name).ToList();
                // there should be 2 users
                foreach (var user in registeredUsers)
                {
                    context.Entry(user).Collection(c => c.Skills).Load();
                    context.Entry(user).Collection(c => c.JobApplications).Load();

                    Console.WriteLine("User: " + user.Name);
                    Console.WriteLine("Skills: " + string.Join(",", user.Skills));
                    Console.WriteLine("Number of applications: " + user.JobApplications.Count);

                }
            }
            Console.ReadLine();
        }
    }
}