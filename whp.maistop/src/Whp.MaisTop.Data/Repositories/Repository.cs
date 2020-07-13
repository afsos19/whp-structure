using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Data.Extensions;
using Whp.MaisTop.Domain.Interfaces.Repositories;

namespace Whp.MaisTop.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly WhpDbContext _dbContext;

        public Repository(WhpDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CustomFindCount(Expression<Func<TEntity, bool>> where) => await _dbContext.Set<TEntity>().Where(where).CountAsync();

        public async Task<int> CustomFindCount(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbContext.Set<TEntity>() as IQueryable<TEntity>;
            query = query.EagerLoad(includes);

            return await query.Where(where).CountAsync();
        }
        public async Task<TEntity> CustomFindLast(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbContext.Set<TEntity>() as IQueryable<TEntity>;
            query = query.EagerLoad(includes);

            return await query.LastOrDefaultAsync(where);
        }

        public async Task<TEntity> CustomFindLast(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, int>> orderBy, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbContext.Set<TEntity>() as IQueryable<TEntity>;
            query = query.EagerLoad(includes);
            query = query.OrderBy(orderBy);

            return await query.LastOrDefaultAsync(where);
        }

        public async Task<IEnumerable<TEntity>> CustomFind(Expression<Func<TEntity, bool>> where) => await _dbContext.Set<TEntity>().Where(where).ToListAsync();

        public async Task<IEnumerable<TEntity>> CustomFind(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbContext.Set<TEntity>() as IQueryable<TEntity>;
            query = query.EagerLoad(includes);

            return await query.Where(where).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> CustomFind(Expression<Func<TEntity, bool>> where, int start, int limit, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbContext.Set<TEntity>() as IQueryable<TEntity>;
            query = query.EagerLoad(includes);
            query = query.Where(where);
            query = query.Skip(start).Take(limit);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> CustomFind(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, int>> orderBy, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbContext.Set<TEntity>() as IQueryable<TEntity>;
            query = query.EagerLoad(includes);

            return await query.Where(where).OrderBy(orderBy).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll() => await _dbContext.Set<TEntity>().ToListAsync();
        public async Task<TEntity> GetLast(Expression<Func<TEntity, bool>> where) => await _dbContext.Set<TEntity>().Where(where).LastOrDefaultAsync();
        public async Task<TEntity> GetLast(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, int>> orderBy) => await _dbContext.Set<TEntity>().Where(where).OrderByDescending(orderBy).LastOrDefaultAsync();
        public async Task<TEntity> GetLast(Expression<Func<TEntity, int>> orderBy) => await _dbContext.Set<TEntity>().OrderByDescending(orderBy).LastOrDefaultAsync();
        public async Task<IEnumerable<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbContext.Set<TEntity>() as IQueryable<TEntity>;
            query = query.EagerLoad(includes);

            return await query.ToListAsync();
        }
        public async Task<TEntity> GetById(int id) => await _dbContext.Set<TEntity>().FindAsync(id);

        public void Save(TEntity entity) => _dbContext.Add(entity);
        
        public void SaveMany(IEnumerable<TEntity> entity) => _dbContext.AddRange(entity);

        public void Delete(TEntity entity) => _dbContext.Remove(entity);

        public void DeleteMany(IEnumerable<TEntity> entity) => _dbContext.RemoveRange(entity);
        
    }
}
