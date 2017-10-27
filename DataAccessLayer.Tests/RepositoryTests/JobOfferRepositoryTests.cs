using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repository;
using Infrastructure.UnitOfWork;
using NUnit.Framework;

namespace DataAccessLayer.Tests.RepositoryTests
{
    [TestFixture]
    public class JobOfferRepositoryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Provider;

        private readonly JobOfferRepository repository = new JobOfferRepository(Initializer.Provider);

        [Test]
        public async Task GetOfferAsync_AlreadyStoredInDBNoIncludes_ReturnsCorrectOffer()
        {
            JobOffer offer;

            using (unitOfWorkProvider.Create())
            {
                offer = await repository.GetAsync(Initializer.GoogleAndroidOffer.Id);
            }

            Assert.NotNull(offer);
            Assert.AreEqual(offer.Id, Initializer.GoogleAndroidOffer.Id);
        }

        [Test]
        public async Task CreateOfferAsync_OfferIsNotPreviouslySeeded_CreatesNewOffer()
        {
            JobOffer offer;

            using (var unitOfWork = Initializer.Provider.Create())
            {
                offer = new JobOffer
                {
                    Name = "New OFfer",
                    Employer = Initializer.GoogleEmployer,
                    Location = "TBD",
                    Skills = new List<SkillTag>
                    {
                        Initializer.AngularSkill
                    },
                    Questions = new List<Question>
                    {
                        Initializer.SoftSkillQuestion
                    }
                };

                repository.Create(offer);
                await unitOfWork.Commit();
            }

            Assert.IsFalse(offer.Id.Equals(0));
        }

        [Test]
        public async Task UpdateOfferAsync_OfferIsPreviouslySeeded_UpdatesOffer()
        {
            JobOffer updatedOffer;
            JobOffer newOffer;

            using (var uow = unitOfWorkProvider.Create())
            {
                newOffer = Initializer.MicrosoftManagerOffer;
                newOffer.Description = "I dont know";
                newOffer.Employer = Initializer.RedHatEmployer;

                repository.Update(newOffer);
                await uow.Commit();
                updatedOffer = await repository.GetAsync(Initializer.MicrosoftManagerOffer.Id);
            }

            Assert.AreEqual(newOffer.Description, updatedOffer.Description);
            Assert.AreEqual(newOffer.Employer, updatedOffer.Employer);
        }


        [Test]
        public async Task DeleteOfferAsync_OfferIsPreviouslySeeded_DeletesOffer()
        {
            JobOffer deleted;

            using (var uow = unitOfWorkProvider.Create())
            {
                repository.Delete(Initializer.MicrosoftCSharpOffer.Id);
                await uow.Commit();
                deleted = await repository.GetAsync(Initializer.MicrosoftCSharpOffer.Id);
            }

            Assert.Null(deleted);
        }

    }
}