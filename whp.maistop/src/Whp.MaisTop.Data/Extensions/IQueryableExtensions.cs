using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Whp.MaisTop.Data.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int page, int pageSize)
        {
            if (pageSize <= 0)
                pageSize = 10;

            return query.Skip(page * pageSize).Take(pageSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="orderingExpression">IExpression object containing ordering delegate</param>
        /// <param name="dir">Ordering direction: asc or desc</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, Expression<Func<T, object>> orderingExpression, string dir)
        {
            var ordered = query.Expression.Type == typeof(IOrderedQueryable<T>);

            switch (dir)
            {

                case "asc":
                    query = ordered ? (query as IOrderedQueryable<T>).ThenBy(orderingExpression)
                        : query.OrderBy(orderingExpression);
                    break;
                case "desc":
                    query = ordered ? (query as IOrderedQueryable<T>).ThenByDescending(orderingExpression)
                        : query.OrderByDescending(orderingExpression);
                    break;
                default:
                    break;
            }

            return query;
        }

        public static IQueryable<T> EagerLoad<T>(this IQueryable<T> query, params
            Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes != null)
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));

            return query;
        }

    }
}
