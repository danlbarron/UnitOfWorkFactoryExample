using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sample.Common.Repositories;
using Sample.Models;

namespace Sample.Data.Repositories {
    public abstract class ReadOnlyEntityRepositoryBase<TEntity> : IReadOnlyEntityRepository<TEntity>
        where TEntity : class {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected ReadOnlyEntityRepositoryBase(DbContext dbContext) {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null) {
            IQueryable<TEntity> query = _dbSet;

            query = filter != null ? query.Where(filter) : query;

            query = includeProperties?.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty)) ?? query;

            query = orderBy != null ? orderBy(query) : query;

            return await query.ToArrayAsync();
        }
        
        public virtual async Task<IReadOnlyList<TEntity>> GetTopXAsync(
            int count,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null) {
            IQueryable<TEntity> query = _dbSet;

            query = filter != null ? query.Where(filter) : query;

            query = includeProperties?.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty)) ?? query;

            query = orderBy != null ? orderBy(query) : query;

            return await query
                .Take(count)
                .ToArrayAsync();
        }
        
        public virtual async Task<TEntity> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null) {
            IQueryable<TEntity> query = _dbSet;

            query = filter != null ? query.Where(filter) : query;

            query = includeProperties?.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty)) ?? query;

            query = orderBy != null ? orderBy(query) : query;

            return await query.FirstOrDefaultAsync();
        }
        
        public virtual async Task<PaginatedResult<TEntity>> GetPageAsync(int pageIndex, int pageSize) {
            IQueryable<TEntity> query = _dbSet;

            var count = await query.CountAsync();
            var entities = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();

            return new PaginatedResult<TEntity>(entities, count, pageIndex, pageSize);
        }
        
        public virtual async Task<TResult> GetMaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector) {
            IQueryable<TEntity> query = _dbSet;

            return await query.MaxAsync(selector);
        }
    }
}