using System;
using System.Threading.Tasks;

namespace Sample.Common.UnitOfWork {
    public interface IUnitOfWork : IDisposable {
        IUnitOfWorkRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task SaveAsync();
    }
}