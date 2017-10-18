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
        public async Task GetEmployerAsync_AlreadyStoredInDBNoIncludes_ReturnsCorrectCategory()
        {
            // Arrange
            Employer employer;

            // Act
            using (unitOfWorkProvider.Create())
            {
                employer = await employerRepository.GetAsync(1);
            }

            // Assert
            Assert.NotNull(employer);
            // TODO Assert.AreEqual(androidCategory.Id, androidCategoryId);
        }
    }
}