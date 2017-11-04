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

            await employerFacade.Register(new EmployerDto
                {
                    Name = "Employer1",
                    Address = "Brno",
                    Email = "mail@empl.com",
                    Password = "pass",
                    PhoneNumber = "+421902333666"
                }
            );


            Assert.AreNotEqual(0, mockManager.CapturedCreatedId);
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
    }
}