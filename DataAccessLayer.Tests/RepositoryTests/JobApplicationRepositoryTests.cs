using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repository;
using Infrastructure.EntityFramework.Repository;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using NUnit.Framework;

namespace DataAccessLayer.Tests.RepositoryTests
{
    [TestFixture]
    public class JobApplicationRepositoryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Provider;

        private readonly JobApplicationRepository repository = new JobApplicationRepository(Initializer.Provider);

        [Test]
        public async Task GetApplicationAsync_AlreadyStoredInDBNoIncludes_ReturnsCorrectApplication()
        {
            JobApplication application;

            using (unitOfWorkProvider.Create())
            {
                application = await repository.GetAsync(Initializer.ApplicationRedHatQuality.Id);
            }

            Assert.NotNull(application);
            Assert.AreEqual(application.Id, Initializer.ApplicationRedHatQuality.Id);
        }
        
        [Test]
        public async Task DeleteJobApplicationAsync_ApplicationIsPreviouslySeeded_DeletesApplication()
        {
            JobApplication jobApplication;

            using (var uow = unitOfWorkProvider.Create())
            {
                repository.Delete(Initializer.ApplicationRedHatQuality.Id);
                await uow.Commit();
                jobApplication = await repository.GetAsync(Initializer.ApplicationRedHatQuality.Id);
            }

            Assert.Null(jobApplication);
        }

    }
}