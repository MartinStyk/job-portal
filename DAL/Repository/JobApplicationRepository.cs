using System;
using DAL.Entities;
using Infrastructure.EntityFramework.Repository;
using Infrastructure.UnitOfWork;

namespace DAL.Repository
{
    public class JobApplicationRepository : EntityFrameworkRepository<JobApplication>
    {
        public JobApplicationRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}