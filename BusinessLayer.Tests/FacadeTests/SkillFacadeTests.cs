using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Skills;
using BusinessLayer.Tests.FacadeTests.Common;
using DAL.Entities;
using Infrastructure.Query;
using Infrastructure.Repository;
using Moq;
using NUnit.Framework;

namespace BusinessLayer.Tests.FacadeTests
{
    class SkillFacadeTests
    {
        [Test]
        public async Task CreateSkill()
        {
            var mockManager = new FacadeMockManager();
            var skillRepositoryMock = mockManager.ConfigureCreateRepositoryMock<SkillTag>(nameof(SkillTag.Id));
            var skillQueryMock =
                mockManager.ConfigureQueryObjectMock<SkillTagDto, SkillTag, SkillTagFilterDto>(null);
            var skillFacade = CreateFacade(skillQueryMock, skillRepositoryMock);

            await skillFacade.CreateSkill(new SkillTagDto
                {
                    Name = "C# master"
                }
            );

            skillRepositoryMock.Verify(repository => repository.Create(It.IsAny<SkillTag>()), Times.AtLeastOnce);
        }


        private static SkillFacade CreateFacade(
            Mock<QueryObjectBase<SkillTagDto, SkillTag, SkillTagFilterDto, IQuery<SkillTag>>> skillQueryMock,
            Mock<IRepository<SkillTag>> skillRepositoryMock)
        {
            var uowMock = FacadeMockManager.ConfigureUowMock();
            var mapper = FacadeMockManager.ConfigureRealMapper();
            var service = new SkillService(mapper, skillRepositoryMock.Object, skillQueryMock.Object);
            var facade = new SkillFacade(uowMock.Object, service);
            return facade;
        }
    }
}