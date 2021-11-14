using Microsoft.EntityFrameworkCore.Storage;
using ShopBridge.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShopBridge.Repository
{
    /// <summary>
    /// IGenericRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Add new record of type T.
        /// </summary>
        /// <param name="entity">T</param>
        /// <returns>Task<T/></returns>
        Task<T> Add(T entity);

        /// <summary>
        /// Get all aysc response of IEnumerable of type T.
        /// </summary>
        /// <returns>Task<IEnumerable<T/>></returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// WherePagedresultWithIncludes
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pagedParameters"></param>
        /// <param name="orderbySelector"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<PagedModel<T>> GetAllPagedresultWithIncludes<TKey>(PagedParameters pagedParameters,
           Func<T, TKey> orderbySelector, IEnumerable<Expression<Func<T, TKey>>> includes);


        /// <summary>
        /// Get the count based on expression
        /// </summary>
        /// <param name="expression">Expression<Func<T, bool/>></param>
        /// <returns>int</returns>
        Task<int> Count(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Get first or default response of type T.
        /// </summary>
        /// <param name="expression">Expression<Func<T, bool>></param>
        /// <returns>Task<T></returns>
        Task<T> FirstOrDefault(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Get all records of type T based on expression.
        /// </summary>
        /// <param name="expression">Expression<Func<T, bool>></param>
        /// <returns>Task<IEnumerable<T/>></returns>
        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> expression);


        /// <summary>
        /// Update exiting record of type T.
        /// </summary>
        /// <param name="entity">T</param>
        /// <returns>T</returns>
        Task<T> Update(T entity);

        /// <summary>
        /// Delete the existing record of type T.
        /// </summary>
        /// <param name="entity">T</param>
        /// <returns>T</returns>2
        Task<T> Delete(T entity);

        /// <summary>
        /// BeginTransactionAsync
        /// </summary>
        /// <returns></returns>
        Task<IDbContextTransaction> BeginTransactionAsync();

        /// <summary>
        /// CreateExecutionStrategy
        /// </summary>
        /// <returns></returns>
        IExecutionStrategy CreateExecutionStrategy();

    }
}
