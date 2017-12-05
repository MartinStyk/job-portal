using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DAL.Context;
using DAL.Entities;
using DAL.Repository;
using Infrastructure.EntityFramework.Query;
using Infrastructure.EntityFramework.Repository;
using Infrastructure.EntityFramework.UnitOfWork;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace DAL.Configuration
{
    public class DataAccessLaterInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(() => new JobPortalDbContext())
                    .LifestyleTransient(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<EntityFrameworkUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For<IJobOfferRepository>()
                    .ImplementedBy<JobOfferRepository>()
                    .LifestyleTransient(),
                Component.For<ISkillRepository>()
                    .ImplementedBy<SkillRepository>()
                    .LifestyleTransient(),
                Component.For<IUserRepository>()
                    .ImplementedBy<UserRepository>()
                    .LifestyleTransient(),
                Component.For<IEmployerRepository>()
                    .ImplementedBy<EmployerRepository>()
                    .LifestyleTransient(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(EntityFrameworkRepository<>))
                    .LifestyleTransient(),
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(EntityFrameworkQuery<>))
                    .LifestyleTransient()
            );
        }
    }
}
