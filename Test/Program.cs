using System;
using System.Linq;
using DAL.Context;
using System.Data.Entity;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new JobPortalDbContext())
            {
                // there should be 3 companies
                foreach (var company in context.Employers.Include(b => b.Address).ToList())
                {
                    Console.WriteLine(company.Name + " : " + company.Address.City);
                }

                // there should be 2 users
                foreach (var user in context.Users.Include(u => u.Skills).Include(u => u.JobApplications).ToList())
                {
                    Console.WriteLine("User: " + user.Name);
                    Console.WriteLine("Skills: " + string.Join(",", user.Skills));
                    Console.WriteLine("Number of applications: " + user.JobApplications.Count);
                }
            }
            Console.ReadLine();
        }
    }
}