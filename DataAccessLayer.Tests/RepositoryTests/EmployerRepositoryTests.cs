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
    public class EmployerRepositoryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Provider;

        private readonly EmployerRepository employerRepository = new EmployerRepository(Initializer.Provider);

        [Test]
        public async Task GetEmployerAsync_AlreadyStoredInDBNoIncludes_ReturnsCorrectEmployer()
        {
            Employer employer;

            using (unitOfWorkProvider.Create())
            {
                employer = await employerRepository.GetAsync(Initializer.RedHatEmployer.Id);
            }

            Assert.NotNull(employer);
            Assert.AreEqual(employer.Id, Initializer.RedHatEmployer.Id);
        }

        [Test]
        public async Task CreateEmployerAsync_EmployerIsNotPreviouslySeeded_CreatesNewEmployer()
        {
            var capco = new Employer
            {
                Name = "Capco",
                Address = "Bratislava, Slovakia",
                Email = "mail@capco.xxx",
                PhoneNumber = "+421 123 456 789",
                PasswordHash = "password",
                PasswordSalt = "password"
            };

            using (var unitOfWork = Initializer.Provider.Create())
            {
                employerRepository.Create(capco);
                await unitOfWork.Commit();
            }

            Assert.IsFalse(capco.Id.Equals(0));
        }

        [Test]
        public async Task UpdateEmployerAsync_EmployerIsPreviouslySeeded_UpdatesEmployer()
        {
            Employer updatedEmployer;
            Employer newEmployer;

            using (var uow = unitOfWorkProvider.Create())
            {
                newEmployer = new Employer
                {
                    Id = Initializer.MicrosoftEmployer.Id,
                    Name = "Microsoft 2.0",
                    Address = "Praha, CZ",
                    Email = "mail@microsoft.xxx",
                    PhoneNumber = "(425) 882-8080",
                    PasswordHash = "password",
                    PasswordSalt = "password"
                };

                employerRepository.Update(newEmployer);
                await uow.Commit();
                updatedEmployer = await employerRepository.GetAsync(Initializer.MicrosoftEmployer.Id);
            }

            Assert.IsTrue(newEmployer.Name.Equals(updatedEmployer.Name));
            Assert.IsTrue(newEmployer.Address.Equals(updatedEmployer.Address));
        }

        
        [Test]
        public async Task DeleteEmployerAsync_EmployerIsPreviouslySeeded_DeletesEmployer()
        {
            Employer deletedEmployer;

            using (var uow = unitOfWorkProvider.Create())
            {
                employerRepository.Delete(Initializer.MicrosoftEmployer.Id);
                await uow.Commit();
                deletedEmployer = await employerRepository.GetAsync(Initializer.MicrosoftEmployer.Id);
            }

            Assert.Null(deletedEmployer);
        }

    }
}