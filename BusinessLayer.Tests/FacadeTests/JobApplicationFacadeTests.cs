using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades;
using BusinessLayer.QueryObjects;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.ApplicationProcessing;
using BusinessLayer.Services.JobApplications;
using BusinessLayer.Services.Skills;
using BusinessLayer.Tests.FacadeTests.Common;
using Castle.Components.DictionaryAdapter;
using DAL.Entities;
using Infrastructure.Query;
using Infrastructure.Repository;
using Moq;
using NUnit.Framework;

namespace BusinessLayer.Tests.FacadeTests
{
    class JobApplicationFacadeTests
    {
        private JobOfferDto jobOfferToCancel;

        private ApplicantDto john;
        private JobApplicationDto johnApplication;

        private ApplicantDto kate;
        private JobApplicationDto kateApplication;

        private Mock<IRepository<JobApplication>> jobApplicationRepositoryMock;

        private Mock<QueryObjectBase<
            JobApplicationDto, JobApplication, JobApplicationFilterDto, IQuery<JobApplication> >> jobApplicationQueryMock;
        [SetUp]
        public void InitializeVariables()
        {
            jobOfferToCancel = new JobOfferDto()
            {
                Description = "",
                Employer = null,
                EmployerId = 100,
                Id = 1,
                Location = "",
                Name = "",
                Questions = new List<QuestionDto>(),
                Skills = new EditableList<SkillTagDto>()
            };

            john = new ApplicantDto()
            {
                Id = 200,
                FirstName = "",
                LastName = "",
                Email = "",
                Education = ""
            };

            johnApplication = new JobApplicationDto()
            {
                Applicant = john,
                ApplicantId = john.Id,
                Id = 300,
                JobApplicationStatus = JobApplicationStatus.Open,
                JobOffer = jobOfferToCancel,
                JobOfferId = jobOfferToCancel.Id,
                QuestionAnswers = new List<QuestionAnswerDto>()
            };

            kate = new ApplicantDto()
            {
                Id = 201,
                FirstName = "",
                LastName = "",
                Email = "",
                Education = ""
            };

            kateApplication = new JobApplicationDto()
            {
                Applicant = kate,
                ApplicantId = kate.Id,
                Id = 301,
                JobApplicationStatus = JobApplicationStatus.Open,
                JobOffer = jobOfferToCancel,
                JobOfferId = jobOfferToCancel.Id,
                QuestionAnswers = new List<QuestionAnswerDto>()
            };

            var jobApplications = new List<JobApplicationDto>();
            jobApplications.Add(johnApplication);
            jobApplications.Add(kateApplication);

            var queryResultDto = new QueryResultDto<JobApplicationDto, JobApplicationFilterDto>();
            queryResultDto.Items = jobApplications;
            queryResultDto.TotalItemsCount = jobApplications.Count;

            var mockManager = new FacadeMockManager();
            jobApplicationRepositoryMock =
                mockManager.ConfigureRepositoryMock<JobApplication>();
            jobApplicationQueryMock =
                mockManager.ConfigureQueryObjectMock<JobApplicationDto, JobApplication, JobApplicationFilterDto>(queryResultDto);
        }

        [Test]
        public async Task acceptOnlyOneJobApplication()
        {
            var facade = CreateFacade(jobApplicationQueryMock, jobApplicationRepositoryMock);
            await facade.AcceptOnlyThisApplication(johnApplication.Id);

            var johnStatusAfter = (await facade.GetApplication(johnApplication.Id));
            Assert.AreEqual(JobApplicationStatus.Accepted, johnStatusAfter.JobApplicationStatus);
            Assert.AreEqual(JobApplicationStatus.Rejected, (await facade.GetApplication(kateApplication.Id)).JobApplicationStatus);
        }

        private static JobApplicationFacade CreateFacade(
            Mock<QueryObjectBase<JobApplicationDto, JobApplication, JobApplicationFilterDto, IQuery<JobApplication>>> jobApplicationQueryMock,
            Mock<IRepository<JobApplication>> jobApplicationRepositoryMock)
        {
            var uowMock = FacadeMockManager.ConfigureUowMock();
            var mapper = FacadeMockManager.ConfigureRealMapper();
            var jobApplicationService = new JobApplicationService(mapper, jobApplicationRepositoryMock.Object, jobApplicationQueryMock.Object);
            var applicationProcessingService = new ApplicationProcesingService(jobApplicationService);
            var facade = new JobApplicationFacade(uowMock.Object, jobApplicationService, applicationProcessingService);
            return facade;
        }
    }
}
