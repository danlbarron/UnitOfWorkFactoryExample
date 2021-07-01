using System;
using Sample.Data.DbContexts;

namespace Sample.Data.UnitOfWork {
    public class SampleUnitOfWorkFactory : UnitOfWorkFactoryBase<SampleDbContext> {
        public SampleUnitOfWorkFactory(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}