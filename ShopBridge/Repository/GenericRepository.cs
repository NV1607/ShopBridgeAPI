using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ShopBridge.DataTransferObjects;
using ShopBridge.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShopBridge.Repository
{
    /// <summary>
    /// We are using Generic Repository to 
    /// perform all generic database operation like
    /// create, update, select, modify for each table
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DbContext Context { get; set; }


        private DbSet<T> DbSet { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        /// <summary>
        /// Commit
        /// </summary>
        /// <returns></returns>
        private async Task<int> Commit()
        {
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> Add(T entity)
        {
            entity = (await DbSet.AddAsync(entity)).Entity;
            _ = await Commit();
            return entity;
        }

        /// <summary>
        /// Count
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<int> Count(Expression<Func<T, bool>> expression)
        {
            return await DbSet.CountAsync(expression);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> Delete(T entity)
        {
            entity = DbSet.Remove(entity).Entity;
            _ = await Commit();
            return entity;
        }

        /// <summary>
        /// FirstOrDefault
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return await DbSet.FirstOrDefaultAsync(expression);
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        /// <summary>
        /// GetAllPagedresultWithIncludes
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pagedParameters"></param>
        /// <param name="orderbySelector"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<PagedModel<T>> GetAllPagedresultWithIncludes<TKey>(PagedParameters pagedParameters,
           Func<T, TKey> orderbySelector, IEnumerable<Expression<Func<T, TKey>>> includes)
        {
            return await DbSet.IncludeMultipleNavigateProperty(includes).AsNoTracking()
                .PaginateDataAsync(pagedParameters, orderbySelector);

        }


        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> Update(T entity)
        {
            entity = DbSet.Update(entity).Entity;
            _ = await Commit();
            return entity;
        }

        /// <summary>
        /// Where
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> expression)
        {
            return await DbSet.Where(expression).ToListAsync();
        }

        /// <summary>
        /// BeginTransactionAsync
        /// </summary>
        /// <returns></returns>
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await Context.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// BeginTransactionAsync
        /// </summary>
        /// <returns></returns>
        public IExecutionStrategy CreateExecutionStrategy()
        {
            return Context.Database.CreateExecutionStrategy();
        }
    }
}
