using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{

    public abstract class UnitOfWorkProviderBase : IUnitOfWorkProvider
    {
        protected readonly AsyncLocal<IUnitOfWork> UowLocalInstance = new AsyncLocal<IUnitOfWork>();

        public abstract IUnitOfWork Create();

        public IUnitOfWork GetInstance()
        {
            return UowLocalInstance != null ? UowLocalInstance.Value : throw new InvalidOperationException("UoW not created");
        }

        public void Dispose()
        {
            UowLocalInstance.Value?.Dispose();
            UowLocalInstance.Value = null;
        }
    }
}
