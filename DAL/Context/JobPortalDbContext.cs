using System.Data.Common;
using DAL.Entities;
using System.Data.Entity;
using DAL.Initializer;

namespace DAL.Context
{
    public class JobPortalDbContext : DbContext
    {
        public JobPortalDbContext() : base("JobPortalDb")
        {
            Database.SetInitializer(new JobPortalInitializer());
        }

        public JobPortalDbContext(DbConnection connection) : base(connection, true)
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Employer> Employers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Applicant> Applicants { get; set; }

        public DbSet<JobOffer> JobOffers { get; set; }

        public DbSet<JobApplication> JobApplications { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }

        public DbSet<SkillTag> SkillTags { get; set; }
    }
}