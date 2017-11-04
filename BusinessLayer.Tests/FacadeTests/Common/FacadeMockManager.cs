using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Configuration;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.QueryObjects.Common;
using Infrastructure.Entity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Moq;

namespace BusinessLayer.Tests.FacadeTests.Common
{
    public class FacadeMockManager
    {
        internal int CapturedCreatedId { get; private set; }

        internal static IMapper ConfigureRealMapper() => new Mapper(new MapperConfiguration(MappingConfiguration.ConfigureMapping));

        internal static Mock<IUnitOfWorkProvider> ConfigureUowMock()
        {
            var uowMock = new Mock<IUnitOfWorkProvider>(MockBehavior.Loose);
            uowMock.Setup(uow => uow.Create()).Returns(new StubUow());
            return uowMock;
        }

        internal Mock<IRepository<TEntity>> ConfigureRepositoryMock<TEntity>() where TEntity : class, IEntity, new()
        {
            return new Mock<IRepository<TEntity>>(MockBehavior.Loose);
        }

        internal Mock<IRepository<TEntity>> ConfigureGetRepositoryMock<TEntity>(TEntity result) where TEntity : class, IEntity, new()
        {
            var mockRepository = new Mock<IRepository<TEntity>>(MockBehavior.Loose);
            mockRepository.Setup(repository => repository.GetAsync(It.IsAny<int>(), It.IsAny<string[]>())).ReturnsAsync(result);
            mockRepository.Setup(repository => repository.GetAsync(It.IsAny<int>())).ReturnsAsync(result);
            return mockRepository;
        }

        internal Mock<IRepository<TEntity>> ConfigureCreateRepositoryMock<TEntity>(string propertyName) where TEntity : class, IEntity, new()
        {
            var mockRepository = new Mock<IRepository<TEntity>>(MockBehavior.Loose);
            mockRepository.Setup(repo => repo.Create(It.IsAny<TEntity>()))
                .Callback<TEntity>(param => CapturedCreatedId = (int)(param.GetType().GetProperty(propertyName)?.GetValue(param) ?? 1));
            return mockRepository;
        }

        internal Mock<QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>>>
            ConfigureQueryObjectMock<TDto, TEntity, TFilterDto>(QueryResultDto<TDto, TFilterDto> result)
            where TEntity : class, IEntity, new()
            where TFilterDto : FilterDtoBase
        {
            var queryMock = new Mock<QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>>>(MockBehavior.Loose, null, null);
            queryMock
                .Setup(query => query.ExecuteQuery(It.IsAny<TFilterDto>()))
                .ReturnsAsync(result);
            return queryMock;
        }
    }
}
