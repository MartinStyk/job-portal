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
    public class SkillTagRepositoryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Provider;

        private readonly SkillRepository repository = new SkillRepository(Initializer.Provider);

        [Test]
        public async Task GetSkillAsync_AlreadyStoredInDBNoIncludes_ReturnsCorrectSkill()
        {
            SkillTag skill;

            using (unitOfWorkProvider.Create())
            {
                skill = await repository.GetAsync(Initializer.JavaSkill.Id);
            }

            Assert.NotNull(skill);
            Assert.AreEqual(skill.Id, Initializer.JavaSkill.Id);
        }

        [Test]
        public async Task CreateSkillAsync_SkillIsNotPreviouslySeeded_CreatesNewSkill()
        {
            var skill = new SkillTag { Name = "Super Hero" };

            using (var unitOfWork = Initializer.Provider.Create())
            {
                repository.Create(skill);
                await unitOfWork.Commit();
            }

            Assert.IsFalse(skill.Id.Equals(0));
        }

        [Test]
        public async Task UpdateUserAsync_UserIsPreviouslySeeded_UpdatesUser()
        {
            SkillTag updatedSkill;
            SkillTag newSkill;

            using (var uow = unitOfWorkProvider.Create())
            {
                newSkill = new SkillTag
                {
                    Id = Initializer.JavaSkill.Id,
                    Name = "Java Super Hero"
                };

                repository.Update(newSkill);
                await uow.Commit();
                updatedSkill = await repository.GetAsync(Initializer.JavaSkill.Id);
            }

            Assert.IsTrue(newSkill.Name.Equals(updatedSkill.Name));
        }

        
        [Test]
        public async Task DeleteSkillAsync_SkillIsPreviouslySeeded_DeletesSkill()
        {
            SkillTag deleted;

            using (var uow = unitOfWorkProvider.Create())
            {
                repository.Delete(Initializer.JavaSkill.Id);
                await uow.Commit();
                deleted = await repository.GetAsync(Initializer.JavaSkill.Id);
            }

            Assert.Null(deleted);
        }

    }
}