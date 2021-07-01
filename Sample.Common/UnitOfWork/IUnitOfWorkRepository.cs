using System.Threading.Tasks;
using Sample.Common.Repositories;

namespace Sample.Common.UnitOfWork {
    public interface IUnitOfWorkRepository<TEntity> : IReadOnlyEntityRepository<TEntity>
        where TEntity : class {
        Task CreateAsync(TEntity entity);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}