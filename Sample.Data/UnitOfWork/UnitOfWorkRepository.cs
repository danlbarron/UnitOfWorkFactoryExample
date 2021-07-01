using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sample.Common.UnitOfWork;
using Sample.Data.Repositories;

namespace Sample.Data.UnitOfWork {
    public class UnitOfWorkRepository<TEntity> : ReadOnlyEntityRepositoryBase<TEntity>, IUnitOfWorkRepository<TEntity>
        where TEntity : class {
        public UnitOfWorkRepository(DbContext dbContext) : base(dbContext) { }

        public virtual Task CreateAsync(TEntity entity) {
            return _dbSet.AddAsync(entity).AsTask();
        }

        public virtual void Delete(TEntity entityToDelete) {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached) {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate) {
            _dbSet.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}