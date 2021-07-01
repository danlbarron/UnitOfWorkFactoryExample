using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.Common.UnitOfWork;

namespace Sample.Data.UnitOfWork {
    public abstract class UnitOfWorkFactoryBase<TDbContext> : IUnitOfWorkFactory
        where TDbContext : DbContext {
        /// <summary>
        /// !! Using this is an intentional use of an anti-pattern.
        /// This is solely used for creating DbContexts via the host builder configuration. !!
        /// </summary>
        protected readonly IServiceProvider _serviceProvider;

        public UnitOfWorkFactoryBase(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public IUnitOfWork CreateUnitOfWork() {
            var dbContext = ActivatorUtilities.CreateInstance<TDbContext>(_serviceProvider);
            return new UnitOfWork(dbContext);
        }
    }
}