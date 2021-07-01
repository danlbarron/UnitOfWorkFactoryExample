using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Data.Configurations {
    public interface IEntityConfiguration<TEntity> where TEntity : class {
        void Configure(EntityTypeBuilder<TEntity> entity);
    }
}