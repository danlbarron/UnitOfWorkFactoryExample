using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sample.Models;

namespace Sample.Common.Repositories {
    public interface IReadOnlyEntityRepository<TEntity>
        where TEntity : class {
        Task<IReadOnlyList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null);
        
        Task<IReadOnlyList<TEntity>> GetTopXAsync(
            int count,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null);
        
        Task<TEntity> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null);

        Task<PaginatedResult<TEntity>> GetPageAsync(int pageIndex, int pageSize);

        Task<TResult> GetMaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector);
    }
}