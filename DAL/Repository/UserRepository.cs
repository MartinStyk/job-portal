using System;
using DAL.Entities;
using Infrastructure.EntityFramework.Repository;
using Infrastructure.UnitOfWork;

namespace DAL.Repository
{
    public class UserRepository : EntityFrameworkRepository<User>
    {
        public UserRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}