using System;
using System.Linq;
using DAL.Entities;
using Infrastructure.EntityFramework.Repository;
using Infrastructure.UnitOfWork;

namespace DAL.Repository
{
    public class EmployerRepository : EntityFrameworkRepository<Employer>, IEmployerRepository
    {
        public EmployerRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        public Employer GetByEmail(string mail)
        {
            return Context.Set<Employer>().SingleOrDefault(employer => employer.Email.Equals(mail));
        }
    }
}