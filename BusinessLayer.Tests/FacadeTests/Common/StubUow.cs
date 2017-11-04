using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Tests.FacadeTests.Common
{
    internal class StubUow : IUnitOfWork
    {
        public void Dispose()
        {
        }

        public Task Commit()
        {
            return Task.Delay(15);
        }
    }
}