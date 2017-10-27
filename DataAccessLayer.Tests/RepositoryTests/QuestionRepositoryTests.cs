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
    public class QuestionRepositoryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Provider;

        private readonly QuestionRepository repository = new QuestionRepository(Initializer.Provider);

        [Test]
        public async Task GetQuestionAsync_AlreadyStoredInDBNoIncludes_ReturnsCorrectQuestion()
        {
            Question question;

            using (unitOfWorkProvider.Create())
            {
                question = await repository.GetAsync(Initializer.JavaExperienceQuestion.Id);
            }

            Assert.NotNull(question);
            Assert.AreEqual(question.Id, Initializer.JavaExperienceQuestion.Id);
        }

        [Test]
        public async Task CreateQuestionAsync_QuestionIsNotPreviouslySeeded_CreatesNewQuestion()
        {
            var question = new Question { Text = "My Question" };

            using (var unitOfWork = Initializer.Provider.Create())
            {
                repository.Create(question);
                await unitOfWork.Commit();
            }

            Assert.IsFalse(question.Id.Equals(0));
        }

        [Test]
        public async Task UpdateQuestionAsync_QuestionIsPreviouslySeeded_UpdatesQuestion()
        {
            Question updatedQuestion;
            Question newQuestion;

            using (var uow = unitOfWorkProvider.Create())
            {
                newQuestion = new Question
                {
                    Id = Initializer.JavaExperienceQuestion.Id,
                    Text = "Are you Java Super Hero?"
                };

                repository.Update(newQuestion);
                await uow.Commit();
                updatedQuestion = await repository.GetAsync(Initializer.JavaExperienceQuestion.Id);
            }

            Assert.IsTrue(newQuestion.Text.Equals(updatedQuestion.Text));
        }

        
        [Test]
        public async Task DeleteQuestionAsync_QuestionIsPreviouslySeeded_DeletesQuestion()
        {
            Question deleted;

            using (var uow = unitOfWorkProvider.Create())
            {
                repository.Delete(Initializer.JavaExperienceQuestion.Id);
                await uow.Commit();
                deleted = await repository.GetAsync(Initializer.JavaExperienceQuestion.Id);
            }

            Assert.Null(deleted);
        }

    }
}