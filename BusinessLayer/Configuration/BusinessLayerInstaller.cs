using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Facades.Common;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Auth;
using BusinessLayer.Services.Common;
using BusinessLayer.Services.JobOfferRecommendations;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DAL.Configuration;

namespace BusinessLayer.Configuration
{
    public class BusinessLayerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Install DAL
            new DataAccessLaterInstaller().Install(container, store);

            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn(typeof(QueryObjectBase<,,,>))
                    .WithServiceBase()
                    .LifestyleTransient(),
                Classes.FromThisAssembly()
                    .BasedOn<ServiceBase>()
                    .WithServiceDefaultInterfaces()
                    .LifestyleTransient(),
                Classes.FromThisAssembly()
                    .BasedOn<FacadeBase>()
                    .LifestyleTransient(),
                Component.For<IMapper>()
                    .Instance(new Mapper(new MapperConfiguration(MappingConfiguration.ConfigureMapping)))
                    .LifestyleSingleton(),
                Component.For<IJobOfferRecommendationService>()
                    .ImplementedBy<JobOfferRecommendationService>()
                    .LifestyleTransient(),
                Component.For<IAuthenticationService>()
                    .ImplementedBy<AuthenticationService>()
                    .LifestyleTransient()
            );

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
        }
    }
}