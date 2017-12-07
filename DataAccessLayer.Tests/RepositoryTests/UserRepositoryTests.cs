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
    public class UserRepositoryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Provider;

        private readonly UserRepository userRepository = new UserRepository(Initializer.Provider);

        [Test]
        public async Task GetUserAsync_AlreadyStoredInDBNoIncludes_ReturnsCorrectUser()
        {
            User user;

            using (unitOfWorkProvider.Create())
            {
                user = await userRepository.GetAsync(Initializer.MadkiUser.Id);
            }

            Assert.NotNull(user);
            Assert.AreEqual(user.Id, Initializer.MadkiUser.Id);
        }

        [Test]
        public async Task CreateUserAsync_UserIsNotPreviouslySeeded_CreatesNewUser()
        {
            var newUser = new User
            {
                FirstName = "New",
                LastName = "MajsterN",
                Email = "majster@n.net",
                PhoneNumber = "+421 123 456 789",
                Education = "I dont know",
                PasswordHash = "password",
                PasswordSalt = "password"
            };

            using (var unitOfWork = Initializer.Provider.Create())
            {
                userRepository.Create(newUser);
                await unitOfWork.Commit();
            }

            Assert.IsFalse(newUser.Id.Equals(0));
        }

        [Test]
        public async Task UpdateUserAsync_UserIsPreviouslySeeded_UpdatesUser()
        {
            User updatedUser;
            User newUser;

            using (var uow = unitOfWorkProvider.Create())
            {
                newUser = new User
                {
                    Id = Initializer.PiskulaUser.Id,
                    FirstName = "PiskulaUpdated",
                    LastName = "Majster",
                    Email = "majster@n.net",
                    PhoneNumber = "+421 123 456 789",
                    Education = "I dont know",
                    PasswordHash = "password",
                    PasswordSalt = "password"
                };

                userRepository.Update(newUser);
                await uow.Commit();
                updatedUser = await userRepository.GetAsync(Initializer.PiskulaUser.Id);
            }

            Assert.IsTrue(newUser.FirstName.Equals(updatedUser.FirstName));
            Assert.IsTrue(newUser.LastName.Equals(updatedUser.LastName));
        }

        
        [Test]
        public async Task DeleteUserAsync_UserIsPreviouslySeeded_DeletesUser()
        {
            User deletedUser;

            using (var uow = unitOfWorkProvider.Create())
            {
                userRepository.Delete(Initializer.PiskulaUser.Id);
                await uow.Commit();
                deletedUser = await userRepository.GetAsync(Initializer.PiskulaUser.Id);
            }

            Assert.Null(deletedUser);
        }

    }
}