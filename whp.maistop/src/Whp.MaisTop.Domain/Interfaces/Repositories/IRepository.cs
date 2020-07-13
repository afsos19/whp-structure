using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Whp.MaisTop.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetLast(Expression<Func<TEntity, bool>> where);
        Task<TEntity> GetLast(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, int>> orderBy);
        Task<IEnumerable<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetLast(Expression<Func<TEntity, int>> orderBy);

        Task<TEntity> GetById(int id);

        Task<TEntity> CustomFindLast(Expression<Func<TEntity, bool>> where,
            params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> CustomFindLast(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, int>> orderBy, params Expression<Func<TEntity, object>>[] includes);

        Task<int> CustomFindCount(Expression<Func<TEntity, bool>> where);

        Task<int> CustomFindCount(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> CustomFind(Expression<Func<TEntity, bool>> where);

        Task<IEnumerable<TEntity>> CustomFind(Expression<Func<TEntity, bool>> where,
            params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> CustomFind(Expression<Func<TEntity, bool>> where, int start, int limit, params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> CustomFind(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, int>> orderby,
            params Expression<Func<TEntity, object>>[] includes);

        void Save(TEntity entity);

        void SaveMany(IEnumerable<TEntity> entity);

        void Delete(TEntity entity);

        void DeleteMany(IEnumerable<TEntity> entity);

    }
}
