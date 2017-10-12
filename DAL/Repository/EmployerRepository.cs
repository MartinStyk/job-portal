using System;
using DAL.Entities;
using Infrastructure.EntityFramework.Repository;
using Infrastructure.UnitOfWork;

namespace DAL.Repository
{
    public class EmployerRepository : EntityFrameworkRepository<Employer, int>
    {
        public EmployerRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}