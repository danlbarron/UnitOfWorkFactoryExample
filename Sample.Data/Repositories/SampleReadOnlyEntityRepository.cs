using Sample.Data.DbContexts;

namespace Sample.Data.Repositories {
    public class SampleReadOnlyEntityRepository<TEntity> : ReadOnlyEntityRepositoryBase<TEntity>
        where TEntity : class {
        public SampleReadOnlyEntityRepository(SampleDbContext context)
            : base(context) { }
    }
}