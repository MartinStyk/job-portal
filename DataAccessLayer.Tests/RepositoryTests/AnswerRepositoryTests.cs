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
    public class AnswerRepositoryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Provider;

        private readonly QuestionAnswerRepository repository = new QuestionAnswerRepository(Initializer.Provider);

        [Test]
        public async Task GetAnswerAsync_AlreadyStoredInDBNoIncludes_ReturnsCorrectAnswer()
        {
            QuestionAnswer answer;

            using (unitOfWorkProvider.Create())
            {
                answer = await repository.GetAsync(Initializer.AnswerJavaEeRedHat.Id);
            }

            Assert.NotNull(answer);
            Assert.AreEqual(answer.Id, Initializer.AnswerJavaEeRedHat.Id);
        }

        [Test]
        public async Task CreateAnswerAsync_AnswerIsNotPreviouslySeeded_CreatesNewAnswer()
        {
            QuestionAnswer answer;

            using (var unitOfWork = Initializer.Provider.Create())
            {
                answer = new QuestionAnswer
                {
                    Application = Initializer.ApplicationRedHatQuality,
                    Question = Initializer.SoftSkillQuestion,

                    Text = "My answer"
                };

                repository.Create(answer);
                await unitOfWork.Commit();
            }

            Assert.IsFalse(answer.Id.Equals(0));
        }

        [Test]
        public async Task UpdateAnswerAsync_AnswerIsPreviouslySeeded_UpdatesAnswer()
        {
            QuestionAnswer updatedAnswer;
            QuestionAnswer newAnswer;

            using (var uow = unitOfWorkProvider.Create())
            {
                newAnswer = Initializer.AnswerJavaEeRedHat;
                newAnswer.Text = "I dont know";

                repository.Update(newAnswer);
                await uow.Commit();
                updatedAnswer = await repository.GetAsync(Initializer.AnswerJavaEeRedHat.Id);
            }

            Assert.IsTrue(newAnswer.Text.Equals(updatedAnswer.Text));
        }

        
        [Test]
        public async Task DeleteAnswerAsync_AnswerIsPreviouslySeeded_DeleteAnswer()
        {
            QuestionAnswer deleted;

            using (var uow = unitOfWorkProvider.Create())
            {
                repository.Delete(Initializer.AnswerJavaEeRedHat.Id);
                await uow.Commit();
                deleted = await repository.GetAsync(Initializer.AnswerJavaEeRedHat.Id);
            }

            Assert.Null(deleted);
        }

    }
}