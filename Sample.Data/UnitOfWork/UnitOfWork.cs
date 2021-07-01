using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sample.Common.UnitOfWork;

namespace Sample.Data.UnitOfWork {
    public class UnitOfWork : IUnitOfWork {
        protected readonly DbContext _dbContext;
        protected bool _disposed;

        public UnitOfWork(DbContext dbContext) {
            _dbContext = dbContext;
            _disposed = false;
        }

        public virtual IUnitOfWorkRepository<TEntity> GetRepository<TEntity>() where TEntity : class {
            return new UnitOfWorkRepository<TEntity>(_dbContext);
        }

        public virtual Task SaveAsync() {
            return _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing) {
            if (!_disposed) {
                if (disposing) {
                    _dbContext.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}