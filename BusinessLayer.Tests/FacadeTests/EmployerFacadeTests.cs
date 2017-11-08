using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Employers;
using BusinessLayer.Tests.FacadeTests.Common;
using DAL.Entities;
using Infrastructure.Query;
using Infrastructure.Repository;
using Moq;
using NUnit.Framework;

namespace BusinessLayer.Tests.FacadeTests
{
    class EmployerFacadeTests
    {
        [Test]
        public async Task CreateEmployer()
        {
            var mockManager = new FacadeMockManager();
            var employerRepositoryMock = mockManager.ConfigureCreateRepositoryMock<Employer>(nameof(Employer.Id));
            var employerQueryMock =
                mockManager.ConfigureQueryObjectMock<EmployerDto, Employer, EmployerFilterDto>(null);
            var employerFacade = CreateFacade(employerQueryMock, employerRepositoryMock);


            var employer = new EmployerDto
            {
                Id = 2,
                Name = "Employer1",
                Address = "Brno",
                Email = "mail@empl.com",
                Password = "pass",
                PhoneNumber = "+421902333666"
            };
            await employerFacade.Register(employer);

            employerRepositoryMock.Verify(repository => repository.Create(It.IsAny<Employer>()), Times.AtLeastOnce);
        }

        [Test]
        public async Task GetEmployerAccordingToEmailAsync_ExistingEmployer_ReturnsCorrectEmployer()
        {
            const string email = "user@somewhere.com";
            var expectedEmployer = new EmployerDto()
            {
                Id = 1,
                Name = "Dilino",
                Password = "pwd",
                Email = email,
                Address = ":D",
                PhoneNumber = "+421910987654"
            };
            var expectedQueryResult =
                new QueryResultDto<EmployerDto, EmployerFilterDto> {Items = new List<EmployerDto> {expectedEmployer}};
            var customerFacade = CreateFacade(expectedQueryResult);

            var actualCustomer = await customerFacade.GetEmployerByEmail(email);

            Assert.AreEqual(actualCustomer, expectedEmployer);
        }

        [Test]
        public async Task GetEmployerAccordingToNameAsync_ExistingEmployer_ReturnsCorrectEmployer()
        {
            const string name = "Dilino";
            var expectedEmployer = new EmployerDto()
            {
                Id = 1,
                Name = name,
                Password = "pwd",
                Email = "user@somewhere.com",
                Address = ":D",
                PhoneNumber = "+421910987654"
            };
            var expectedQueryResult =
                new QueryResultDto<EmployerDto, EmployerFilterDto> {Items = new List<EmployerDto> {expectedEmployer}};
            var customerFacade = CreateFacade(expectedQueryResult);

            var actualCustomer = await customerFacade.GetEmployerByName(name);

            Assert.AreEqual(actualCustomer, expectedEmployer);
        }


        [Test]
        public async Task GetAllEmployersAsync_TwoExistingEmployers_ReturnsCorrectQueryResult()
        {
            var expectedQueryResult = new QueryResultDto<EmployerDto, EmployerFilterDto>
            {
                Filter = new EmployerFilterDto(),
                Items = new List<EmployerDto> {new EmployerDto {Id = 5}, new EmployerDto {Id = 1}},
                PageSize = 10,
                RequestedPageNumber = null
            };
            var customerFacade = CreateFacade(expectedQueryResult);

            var actualQueryResult = await customerFacade.GetAllEmployersAsync();

            Assert.AreEqual(actualQueryResult, expectedQueryResult);
        }

        private static EmployerFacade CreateFacade(
            Mock<QueryObjectBase<EmployerDto, Employer, EmployerFilterDto, IQuery<Employer>>> employerQueryMock,
            Mock<IRepository<Employer>> employerRepositoryMock)
        {
            var mockManager = new FacadeMockManager();
            var uowMock = FacadeMockManager.ConfigureUowMock();
            var mapper = FacadeMockManager.ConfigureRealMapper();
            var service = new EmployerService(mapper, employerRepositoryMock.Object, employerQueryMock.Object);
            var facade = new EmployerFacade(uowMock.Object, service);
            return facade;
        }

        private static EmployerFacade CreateFacade(QueryResultDto<EmployerDto, EmployerFilterDto> expectedQueryResult)
        {
            var mockManager = new FacadeMockManager();
            var uowMock = FacadeMockManager.ConfigureUowMock();
            var mapper = FacadeMockManager.ConfigureRealMapper();
            var repositoryMock = mockManager.ConfigureRepositoryMock<Employer>();
            var queryMock =
                mockManager.ConfigureQueryObjectMock<EmployerDto, Employer, EmployerFilterDto>(expectedQueryResult);
            var customerService = new EmployerService(mapper, repositoryMock.Object, queryMock.Object);
            var customerFacade = new EmployerFacade(uowMock.Object, customerService);
            return customerFacade;
        }
    }
}