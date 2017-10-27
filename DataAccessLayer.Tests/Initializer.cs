using System;
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

        public static Employer RedHatEmployer, GoogleEmployer, MicrosoftEmployer;
        public static User PiskulaUser, MadkiUser;
        public static Applicant AnonymousUser;
        public static SkillTag JavaSkill, CSharpSkill, PhpSkill, AngularSkill, AndroidSkill;
        public static JobOffer RedHatQualityOffer, GoogleAndroidOffer, MicrosoftManagerOffer, MicrosoftCSharpOffer;
        public static Question JavaExperienceQuestion, JavaEeExperienceQuestion, CSharpExperienceQuestion,
        WebEprienceQuestion, AndroidExperienceQuestion, SoftSkillQuestion, HobbyQuesiton;
        public static JobApplication ApplicationRedHatQuality;
        public static QuestionAnswer AnswersoftSkillsRedHat, ÀnswersJavaRedHat, AnswerJavaEeRedHat;



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

            RedHatEmployer = new Employer
            {
                Name = "RedHat",
                Address = "Brno, CzechRepublic",
                Email = "mail@redhat.xxx",
                PhoneNumber = "+420 123 456 789",
                Password = "rh_pwd"
            };
            GoogleEmployer = new Employer
            {
                Name = "Google",
                Address = "MountainView, CA",
                Email = "mail@google.xxx",
                PhoneNumber = "+421 123 456 789",
                Password = "google_pwd"
            };

            MicrosoftEmployer = new Employer
            {
                Name = "Microsoft",
                Address = "Praha, CZ",
                Email = "mail@microsoft.xxx",
                PhoneNumber = "(425) 882-8080",
                Password = "ms_pwd"
            };

            context.Employers.Add(RedHatEmployer);
            context.Employers.Add(GoogleEmployer);
            context.Employers.Add(MicrosoftEmployer);

            #endregion

            #region skills

            CSharpSkill = new SkillTag { Name = "C#" };
            JavaSkill = new SkillTag { Name = "Java" };
            PhpSkill = new SkillTag { Name = "Php" };
            AngularSkill = new SkillTag { Name = "Angular" };
            AndroidSkill = new SkillTag { Name = "Android" };

            context.SkillTags.Add(CSharpSkill);
            context.SkillTags.Add(JavaSkill);
            context.SkillTags.Add(PhpSkill);
            context.SkillTags.Add(AngularSkill);
            context.SkillTags.Add(AndroidSkill);

            #endregion

            #region users

            PiskulaUser = new User
            {
                FirstName = "Piskula",
                LastName = "Zeleny",
                Email = "piskula@programmer.net",
                PhoneNumber = "+420 123 456 789",
                Education = "High School of Live",
                Skills = new List<SkillTag>
                {
                    JavaSkill,
                    PhpSkill,
                    AngularSkill
                },
                Password = "password"
            };

            MadkiUser = new User
            {
                FirstName = "Madki",
                LastName = "Programmer",
                Email = "madki@programmer.net",
                PhoneNumber = "+421 999 666 789",
                Education = "Programming High",
                Skills = new List<SkillTag>
                {
                    JavaSkill,
                    CSharpSkill,
                    AndroidSkill
                },
                Password = "password"
            };

            AnonymousUser = new Applicant()
            {
                FirstName = "Anonymous",
                LastName = "Inkognito",
                Email = "Anonymous@programmer.net",
                PhoneNumber = "+420 5565893",
                Education = "Secret"
            };

            context.Applicants.Add(PiskulaUser);
            context.Applicants.Add(MadkiUser);
            context.Applicants.Add(AnonymousUser);

            #endregion

            #region questions

            JavaExperienceQuestion = new Question { Text = "What is your exeperience with Java programming?" };
            JavaEeExperienceQuestion = new Question { Text = "What is your exeperience with Java EE programming?" };
            CSharpExperienceQuestion = new Question { Text = "What is your exeperience with .Net programming?" };
            WebEprienceQuestion = new Question { Text = "What is your exeperience with web application programming?" };
            AndroidExperienceQuestion = new Question { Text = "What is your exeperience with Android programming?" };
            SoftSkillQuestion = new Question { Text = "Tell us about your soft skills" };
            HobbyQuesiton = new Question { Text = "What is your hobby?" };

            context.Questions.Add(JavaExperienceQuestion);
            context.Questions.Add(JavaEeExperienceQuestion);
            context.Questions.Add(CSharpExperienceQuestion);
            context.Questions.Add(WebEprienceQuestion);
            context.Questions.Add(AndroidExperienceQuestion);
            context.Questions.Add(SoftSkillQuestion);
            context.Questions.Add(HobbyQuesiton);

            #endregion

            #region job offers

            GoogleAndroidOffer = new JobOffer
            {
                Name = "Associate Android Developer",
                Employer = GoogleEmployer,
                Location = "Paris, FR",
                Description = "Develop apps for Android - it will be fun!",
                Skills = new List<SkillTag>
                {
                    JavaSkill,
                    AndroidSkill
                },
                Questions = new List<Question>
                {
                    SoftSkillQuestion,
                    JavaExperienceQuestion,
                    AndroidExperienceQuestion
                }
            };

          
            MicrosoftCSharpOffer = new JobOffer
            {
                Name = "C# dev",
                Employer = MicrosoftEmployer,
                Location = "Seattle, WS",
                Description = "Lets see sharp!",
                Skills = new List<SkillTag>
                {
                    CSharpSkill
                },
                Questions = new List<Question>
                {
                    CSharpExperienceQuestion
                }
            };

            MicrosoftManagerOffer = new JobOffer
            {
                Name = "Project manager junior",
                Employer = MicrosoftEmployer,
                Location = "Seattle 2, WS",
                Description = "Manage amazing projects",
                Skills = new List<SkillTag>
                {
                    CSharpSkill
                },
                Questions = new List<Question>
                {
                    SoftSkillQuestion,
                    HobbyQuesiton
                }
            };

            RedHatQualityOffer = new JobOffer
            {
                Name = "Quality engineer",
                Employer = RedHatEmployer,
                Location = "Brno, CZ",
                Description = "Quality matters",
                Skills = new List<SkillTag>
                {
                    JavaSkill
                },
                Questions = new List<Question>
                {
                    SoftSkillQuestion,
                    JavaExperienceQuestion,
                    JavaEeExperienceQuestion
                }
            };

            context.JobOffers.Add(GoogleAndroidOffer);
            context.JobOffers.Add(MicrosoftCSharpOffer);
            context.JobOffers.Add(MicrosoftManagerOffer);
            context.JobOffers.Add(RedHatQualityOffer);

            #endregion

            #region applications

            ApplicationRedHatQuality = new JobApplication
            {
                Applicant = MadkiUser,
                JobOffer = RedHatQualityOffer,
                JobApplicationStatus = JobApplicationStatus.Open
            };

            AnswersoftSkillsRedHat = new QuestionAnswer
            {
                Text = "Great",
                Application = ApplicationRedHatQuality,
                Question = SoftSkillQuestion
            };

            ÀnswersJavaRedHat = new QuestionAnswer
            {
                Text = "Very Good",
                Application = ApplicationRedHatQuality,
                Question = JavaExperienceQuestion
            };

            AnswerJavaEeRedHat = new QuestionAnswer
            {
                Text = "Basic",
                Application = ApplicationRedHatQuality,
                Question = JavaEeExperienceQuestion
            };

            ApplicationRedHatQuality.QuestionAnswers = new List<QuestionAnswer>
            {
                AnswersoftSkillsRedHat,
                ÀnswersJavaRedHat,
                AnswerJavaEeRedHat
            };


            context.QuestionAnswers.Add(AnswersoftSkillsRedHat);
            context.QuestionAnswers.Add(AnswerJavaEeRedHat);
            context.QuestionAnswers.Add(ÀnswersJavaRedHat);
            context.JobApplications.Add(ApplicationRedHatQuality);

            #endregion

            context.SaveChanges();

            return context;
        }
    }

}
