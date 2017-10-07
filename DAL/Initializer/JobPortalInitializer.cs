using System.Collections.Generic;
using System.Data.Entity;
using DAL.Context;
using DAL.Entities;

namespace DAL.Initializer
{
    public class JobPortalInitializer : DropCreateDatabaseAlways<JobPortalDbContext>
    {
        protected override void Seed(JobPortalDbContext context)
        {
            #region address

            var redHatAddress = new Address
            {
                Country = "Czech Republic",
                City = "Brno",
                Street = "Purkyňova",
                Number = "111"
            };

            var googleAddress = new Address
            {
                Country = "US",
                City = "Mountain View",
                Street = "Amphitheatre Parkway",
                Number = "1600"
            };

            var microsoftAddress = new Address
            {
                Country = "USA",
                City = "Redmond",
                Street = "One Microsoft Way",
                Number = "98052"
            };

            #endregion

            #region employers

            var redHat = new Employer
            {
                Name = "RedHat",
                Address = redHatAddress,
                Email = "mail@redhat.xxx",
                PhoneNumber = "+420 123 456 789"
            };
            var google = new Employer
            {
                Name = "Google",
                Address = googleAddress,
                Email = "mail@google.xxx",
                PhoneNumber = "+421 123 456 789"
            };
            var microsoft = new Employer
            {
                Name = "Microsoft",
                Address = microsoftAddress,
                Email = "mail@microsoft.xxx",
                PhoneNumber = "(425) 882-8080"
            };

            context.Employers.Add(redHat);
            context.Employers.Add(google);
            context.Employers.Add(microsoft);

            #endregion

            #region skills

            var cSharp = new SkillTag {Name = "C#"};
            var java = new SkillTag {Name = "Java"};
            var php = new SkillTag {Name = "Php"};
            var angular = new SkillTag {Name = "Angular"};
            var android = new SkillTag {Name = "Android"};

            context.SkillTags.Add(cSharp);
            context.SkillTags.Add(java);
            context.SkillTags.Add(php);
            context.SkillTags.Add(angular);
            context.SkillTags.Add(android);

            #endregion

            #region users

            var piskula = new User
            {
                Name = "Piskula",
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
                Name = "Madki",
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

            var anonymous = new Applicant
            {
                Name = "Anonymous",
                Email = "Anonymous@programmer.net",
                PhoneNumber = "+420 5565893",
                Education = "Secret"
            };

            context.Users.Add(piskula);
            context.Users.Add(madki);
            context.Applicants.Add(anonymous);

            #endregion

            #region questions

            var javaExperience = new Question {Text = "What is your exeperience with Java programming?"};
            var javaEeExperience = new Question {Text = "What is your exeperience with Java EE programming?"};
            var cSharpExperience = new Question {Text = "What is your exeperience with .Net programming?"};
            var webExperience = new Question {Text = "What is your exeperience with web application programming?"};
            var androidExperience = new Question {Text = "What is your exeperience with Android programming?"};
            var softSkills = new Question {Text = "Tell us about your soft skills"};
            var hobby = new Question {Text = "What is your hobby?"};

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
                Location = googleAddress,
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
                Location = googleAddress,
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
                Location = googleAddress,
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
                Location = microsoftAddress,
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
                Location = microsoftAddress,
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
                Location = redHatAddress,
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
                Status = Status.Open
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

            base.Seed(context);
        }
    }
}