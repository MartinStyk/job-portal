using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using DAL.Repository;
using Infrastructure.EntityFramework.Query;
using Infrastructure.EntityFramework.UnitOfWork;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;
using Infrastructure.UnitOfWork;

namespace Test
{
    class Program
    {
        private static readonly IUnitOfWorkProvider Provider =
            new EntityFrameworkUnitOfWorkProvider(() => new JobPortalDbContext());

        private static readonly ApplicantRepository ApplicantRepository = new ApplicantRepository(Provider);

        private static readonly EntityFrameworkQuery<Applicant> ApplicantQuery = new EntityFrameworkQuery<Applicant>(Provider);


        static void Main(string[] args)
        {
            using (var context = new JobPortalDbContext())
            {
                // there should be 3 companies
                foreach (var company in context.Employers.ToList())
                {
                    Console.WriteLine(company.Name + " : " + company.JobOffers?.Count);
                }

                // there should be 2 users
                foreach (var user in context.Users.ToList())
                {
                    Console.WriteLine("User: " + user.FirstName + "(" + user.Password + ")");
                    Console.WriteLine("Skills: " + string.Join(",", user.Skills));
                    Console.WriteLine("Number of applications: " + user.JobApplications.Count);
                }


                // test repository
                using (var unitOfWork = Provider.Create())
                {
                    Console.WriteLine(ApplicantRepository.Get(1).FirstName);
                }

                TestQuery().Wait();

            }
            Console.ReadLine();
        }

        private static async Task TestQuery()
        {
            using (Provider.Create())
            {
                var madkiApplicantQuery = await ApplicantQuery
                    .Where(new SimplePredicate(nameof(Applicant.FirstName), ValueComparingOperator.StringContains, "Madki"))
                    .ExecuteAsync();
                foreach(var res in madkiApplicantQuery.Items)
                {
                    Console.WriteLine(res.FirstName + res.LastName);
                }
            }
        }

    }
}