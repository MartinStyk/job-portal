﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using Infrastructure.EntityFramework.UnitOfWork;
using Infrastructure.UnitOfWork;
using NUnit.Framework;

namespace DataAccessLayer.Tests
{
    [SetUpFixture]
    public class Initializer
    {
        private const string TestDbConnectionString = "InMemoryTestDBJobPortal";

        internal static readonly IUnitOfWorkProvider Provider = new EntityFrameworkUnitOfWorkProvider(InitializeDatabase);

        /// <summary>
        /// Initializes all Business Layer tests
        /// </summary>
        [OneTimeSetUp]
        public void InitializeBusinessLayerTests()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            Database.SetInitializer(new DropCreateDatabaseAlways<JobPortalDbContext>());
        }

        private static DbContext InitializeDatabase()
        {
            var context = new JobPortalDbContext(Effort.DbConnectionFactory.CreatePersistent(TestDbConnectionString));
            context.Employers.RemoveRange(context.Employers);
            context.Users.RemoveRange(context.Users);
            context.Applicants.RemoveRange(context.Applicants);
            context.JobOffers.RemoveRange(context.JobOffers);
            context.JobApplications.RemoveRange(context.JobApplications);
            context.Questions.RemoveRange(context.Questions);
            context.QuestionAnswers.RemoveRange(context.QuestionAnswers);
            context.SkillTags.RemoveRange(context.SkillTags);
        
            context.SaveChanges();

            #region employers

            var redHat = new Employer
            {
                Name = "RedHat",
                Address = "Brno, CzechRepublic",
                Email = "mail@redhat.xxx",
                PhoneNumber = "+420 123 456 789",
                Password = "rh_pwd"
            };
            var google = new Employer
            {
                Name = "Google",
                Address = "MountainView, CA",
                Email = "mail@google.xxx",
                PhoneNumber = "+421 123 456 789",
                Password = "google_pwd"
            };

            var microsoft = new Employer
            {
                Name = "Microsoft",
                Address = "Praha, CZ",
                Email = "mail@microsoft.xxx",
                PhoneNumber = "(425) 882-8080",
                Password = "ms_pwd"
            };

            context.Employers.Add(redHat);
            context.Employers.Add(google);
            context.Employers.Add(microsoft);

            #endregion

            #region skills

            var cSharp = new SkillTag { Name = "C#" };
            var java = new SkillTag { Name = "Java" };
            var php = new SkillTag { Name = "Php" };
            var angular = new SkillTag { Name = "Angular" };
            var android = new SkillTag { Name = "Android" };

            context.SkillTags.Add(cSharp);
            context.SkillTags.Add(java);
            context.SkillTags.Add(php);
            context.SkillTags.Add(angular);
            context.SkillTags.Add(android);

            #endregion

            #region users

            var piskula = new User
            {
                FirstName = "Piskula",
                LastName = "Zeleny",
                Email = "piskula@programmer.net",
                PhoneNumber = "+420 123 456 789",
                Education = "High School of Live",
                Skills = new List<SkillTag>
                {
                    java,
                    php,
                    angular
                },
                Password = "password"
            };

            var madki = new User
            {
                FirstName = "Madki",
                LastName = "Programmer",
                Email = "madki@programmer.net",
                PhoneNumber = "+421 999 666 789",
                Education = "Programming High",
                Skills = new List<SkillTag>
                {
                    java,
                    cSharp,
                    android
                },
                Password = "password"
            };

            var anonymous = new Applicant()
            {
                FirstName = "Anonymous",
                LastName = "Inkognito",
                Email = "Anonymous@programmer.net",
                PhoneNumber = "+420 5565893",
                Education = "Secret"
            };

            context.Applicants.Add(piskula);
            context.Applicants.Add(madki);
            context.Applicants.Add(anonymous);

            #endregion

            #region questions

            var javaExperience = new Question { Text = "What is your exeperience with Java programming?" };
            var javaEeExperience = new Question { Text = "What is your exeperience with Java EE programming?" };
            var cSharpExperience = new Question { Text = "What is your exeperience with .Net programming?" };
            var webExperience = new Question { Text = "What is your exeperience with web application programming?" };
            var androidExperience = new Question { Text = "What is your exeperience with Android programming?" };
            var softSkills = new Question { Text = "Tell us about your soft skills" };
            var hobby = new Question { Text = "What is your hobby?" };

            context.Questions.Add(javaExperience);
            context.Questions.Add(javaEeExperience);
            context.Questions.Add(cSharpExperience);
            context.Questions.Add(webExperience);
            context.Questions.Add(androidExperience);
            context.Questions.Add(softSkills);
            context.Questions.Add(hobby);

            #endregion

            #region job offers

            var googleAndroidOffer = new JobOffer
            {
                Name = "Associate Android Developer",
                Employer = google,
                Location = "Paris, FR",
                Description = "Develop apps for Android - it will be fun!",
                Skills = new List<SkillTag>
                {
                    java,
                    android
                },
                Questions = new List<Question>
                {
                    softSkills,
                    javaExperience,
                    androidExperience
                }
            };

            var googleBackendOffer = new JobOffer
            {
                Name = "Java backend senior",
                Employer = google,
                Location = "Vienna, Austria",
                Description = "Be a backend hero!",
                Skills = new List<SkillTag>
                {
                    java
                },
                Questions = new List<Question>
                {
                    javaExperience,
                    javaEeExperience
                }
            };

            var googleFronetEndOffer = new JobOffer
            {
                Name = "Javascript front end developer",
                Employer = google,
                Location = "San Francisco, CA",
                Description = "Create amazing UI",
                Skills = new List<SkillTag>
                {
                    angular,
                    php
                },
                Questions = new List<Question>
                {
                    webExperience,
                    softSkills,
                    hobby
                }
            };

            var microsoftCsharpDev = new JobOffer
            {
                Name = "C# dev",
                Employer = microsoft,
                Location = "Seattle, WS",
                Description = "Lets see sharp!",
                Skills = new List<SkillTag>
                {
                    cSharp
                },
                Questions = new List<Question>
                {
                    cSharpExperience
                }
            };

            var microsoftProjectManager = new JobOffer
            {
                Name = "Project manager junior",
                Employer = microsoft,
                Location = "Seattle 2, WS",
                Description = "Manage amazing projects",
                Skills = new List<SkillTag>
                {
                    cSharp
                },
                Questions = new List<Question>
                {
                    softSkills,
                    hobby
                }
            };

            var redHatQalityEngineer = new JobOffer
            {
                Name = "Quality engineer",
                Employer = redHat,
                Location = "Brno, CZ",
                Description = "Quality matters",
                Skills = new List<SkillTag>
                {
                    java
                },
                Questions = new List<Question>
                {
                    softSkills,
                    javaExperience,
                    javaEeExperience
                }
            };

            context.JobOffers.Add(googleAndroidOffer);
            context.JobOffers.Add(googleBackendOffer);
            context.JobOffers.Add(googleFronetEndOffer);
            context.JobOffers.Add(microsoftCsharpDev);
            context.JobOffers.Add(microsoftProjectManager);
            context.JobOffers.Add(redHatQalityEngineer);

            #endregion

            #region applications

            var applicationRedHatQuality = new JobApplication
            {
                Applicant = madki,
                JobOffer = redHatQalityEngineer,
                JobApplicationStatus = JobApplicationStatus.Open
            };

            var answersoftSkillsRedHat = new QuestionAnswer
            {
                Text = "Great",
                Application = applicationRedHatQuality,
                Question = softSkills
            };

            var answersJavaRedHat = new QuestionAnswer
            {
                Text = "Very Good",
                Application = applicationRedHatQuality,
                Question = javaExperience
            };

            var answerJavaEeRedHat = new QuestionAnswer
            {
                Text = "Basic",
                Application = applicationRedHatQuality,
                Question = javaEeExperience
            };

            applicationRedHatQuality.QuestionAnswers = new List<QuestionAnswer>
            {
                answersoftSkillsRedHat,
                answerJavaEeRedHat,
                answersJavaRedHat
            };


            context.QuestionAnswers.Add(answersoftSkillsRedHat);
            context.QuestionAnswers.Add(answerJavaEeRedHat);
            context.QuestionAnswers.Add(answersJavaRedHat);
            context.JobApplications.Add(applicationRedHatQuality);

            #endregion

            context.SaveChanges();

            return context;
        }
    }

}
