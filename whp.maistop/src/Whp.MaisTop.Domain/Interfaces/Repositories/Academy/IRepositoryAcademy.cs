using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Whp.MaisTop.Domain.Interfaces.Repositories.Academy
{
    public interface IRepositoryAcademy<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(int id);

        Task<IEnumerable<TEntity>> CustomFind(Expression<Func<TEntity, bool>> where);

        Task<IEnumerable<TEntity>> CustomFind(Expression<Func<TEntity, bool>> where,
            params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> CustomFind(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, int>> orderby,
            params Expression<Func<TEntity, object>>[] includes);

        void Save(TEntity entity);

        void SaveMany(IEnumerable<TEntity> entity);

        void Delete(TEntity entity);

        void DeleteMany(IEnumerable<TEntity> entity);
    }
}
