using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Configuration;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Employers;
using BusinessLayer.Services.JobApplications;
using BusinessLayer.Services.JobOffers;
using BusinessLayer.Services.Questions;
using BusinessLayer.Services.Skills;
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

        private static readonly EntityFrameworkQuery<Applicant> ApplicantQuery =
            new EntityFrameworkQuery<Applicant>(Provider);


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

                Mapper mapper = new Mapper(new MapperConfiguration(MappingConfiguration.ConfigureMapping));

                TestRegister(mapper).Wait();
                TestCreateOffer(mapper).Wait();
                TestCreateApplication(mapper).Wait();


            }
            Console.ReadLine();
        }

        private static async Task TestRegister(Mapper mapper)
        {
            EmployerFacade employerFacade = new EmployerFacade(Provider,
                new EmployerService(mapper, new EmployerRepository(Provider),
                    new EmployerQueryObject(mapper, new EntityFrameworkQuery<Employer>(Provider))));
            await employerFacade.Register(new EmployerDto
                {
                    Name = "Employer1",
                    Address = "Brno",
                    Email = "mail@empl.com",
                    Password = "pass",
                    PhoneNumber = "+421902333666"
                }
            );

            var results = await employerFacade.GetAllEmployersAsync();
            foreach (var resultsItem in results.Items)
            {
                Console.WriteLine(resultsItem.Name, resultsItem.Id);
            }
        }

        private static async Task TestCreateOffer(Mapper mapper)
        {
            JobOfferFacade jobOfferFacade = new JobOfferFacade(Provider,mapper,
                new EmployerService(mapper, new EmployerRepository(Provider),
                    new EmployerQueryObject(mapper, new EntityFrameworkQuery<Employer>(Provider))),
                new JobOfferService(mapper, new JobOfferRepository(Provider),
                    new JobOfferQueryObject(mapper, new EntityFrameworkQuery<JobOffer>(Provider))),
                new SkillService(mapper, new SkillRepository(Provider),
                    new SkillQueryObject(mapper, new EntityFrameworkQuery<SkillTag>(Provider))),
                new QuestionService(mapper, new QuestionRepository(Provider),
                    new QuestionQueryObject(mapper, new EntityFrameworkQuery<Question>(Provider))));

            await jobOfferFacade.CreateJobOffer(new JobOfferCreateDto{
                Description = "desc1",
                EmployerId = 1,
                Location = "loc1",
                Name = "name1",
                QuestionTexts = new[] {"q1", "q2", "q3"},
                SkillsIds = new[] {1, 2, 3},
            });

            var results = await jobOfferFacade.GetAllOffersOfEmployer(1);
            foreach (var resultsItem in results)
            {
                Console.WriteLine(resultsItem.Name);
            }

            var results1 = await jobOfferFacade.GetOffersBySkill(1);
            foreach (var resultsItem in results1)
            {
                Console.WriteLine(resultsItem.Name);
            }
        }

        private static async Task TestCreateApplication(Mapper mapper)
        {
            JobApplicationFacade jobApplicationFacade = new JobApplicationFacade(Provider,
                new JobApplicationService(mapper, new JobApplicationRepository(Provider),
                    new JobApplicationQueryObject(mapper, new EntityFrameworkQuery<JobApplication>(Provider))));

            List<QuestionAnswerDto> questionAnswers = new List<QuestionAnswerDto>();
            questionAnswers.Add(new QuestionAnswerDto{QuestionId = 1, Text = "aaaaa"});
            await jobApplicationFacade.CreateApplication(new JobApplicationDto
            {
                ApplicantId = 1,
                JobOfferId = 1,
                QuestionAnswers = questionAnswers
            });

            var results = await jobApplicationFacade.GetAllApplications();
            foreach (var resultsItem in results)
            {
                Console.WriteLine(resultsItem.JobApplicationStatus);
            }
        }

        private static async Task TestQuery()
        {
            using (Provider.Create())
            {
                var madkiApplicantQuery = await ApplicantQuery
                    .Where(new SimplePredicate(nameof(Applicant.FirstName), ValueComparingOperator.StringContains,
                        "Madki"))
                    .ExecuteAsync();
                foreach (var res in madkiApplicantQuery.Items)
                {
                    Console.WriteLine(res.FirstName + res.LastName);
                }
            }
        }
    }
}