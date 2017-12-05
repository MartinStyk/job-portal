using System;
using System.Linq;
using DAL.Entities;
using Infrastructure.EntityFramework.Repository;
using Infrastructure.UnitOfWork;

namespace DAL.Repository
{
    public class UserRepository :  EntityFrameworkRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        public User GetByEmail(string mail)
        {
            return Context.Set<User>().SingleOrDefault(user => user.Email.Equals(mail));
        }
    }
}