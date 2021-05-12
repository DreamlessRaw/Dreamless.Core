using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Fissoft;
using Fissoft.EntitySearch;
using Microsoft.EntityFrameworkCore.Query;
namespace Dreamless.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="searchModel">searchModel</param>
        /// <param name="disableTracking">默认不跟踪查询</param>
        /// <returns></returns>
        PagedList<TEntity> QueryPagedList(SearchModel searchModel, bool disableTracking = true);

        /// <summary>
        /// 获取分页数据,只查询需要的数据
        /// </summary>
        /// <param name="searchModel">searchModel</param>
        /// <returns></returns>
        PagedList<TResult> QueryPagedList<TResult>(SearchModel searchModel, Expression<Func<TEntity, TResult>> selector) where TResult : class;

        /// <summary>
        /// 获取分页数据并映射Mapper
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="searchModel">searchModel</param>
        /// <returns></returns>
        PagedList<TResult> QueryPagedMappingList<TResult>(SearchModel searchModel) where TResult : class;

        /// <summary>
        /// 查询符合条件的数据,全部字段
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate">条件</param>
        /// <param name="disableTracking">默认不跟踪查询</param>
        /// <returns></returns>
        List<TEntity> QueryList(Expression<Func<TEntity, bool>> predicate,
                                                  bool disableTracking = true);

        /// <summary>
        /// 查询符合条件的数据,查询字段
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate">条件</param>
        /// <param name="selector">字段信息</param>
        /// <param name="disableTracking">默认不跟踪查询</param>
        /// <returns></returns>
        List<TResult> QueryList<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                  Expression<Func<TEntity, bool>> predicate = null,
                                                  bool disableTracking = true) where TResult : class;

        /// <summary>
        /// 查询并映射Mapper
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        List<TResult> QueryMappingList<TResult>(Expression<Func<TEntity, bool>> predicate = null) where TResult : class;


        /// <summary>
        /// 获取IQueryable
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="disableTracking">默认不跟踪查询</param>
        /// <returns></returns>
        IQueryable<TEntity> Queryable(bool disableTracking = true);

        /// <summary>
        /// 查询符合条件的数据第一条数据
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="disableTracking">默认跟踪查询,方便查询出修改</param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate = null, bool disableTracking = false);

        /// <summary>
        /// Query then Table
        /// </summary>
        /// <param name="sql">exec statement</param>
        /// <param name="parameters">param</param>
        /// <returns></returns>
        IEnumerable<TEntity> FromSql(string sql, params object[] parameters);

        /// <summary>
        /// Gets the total number of rows
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// Does the data exist
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Any(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Inserts a new entity synchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        void Insert(TEntity entity);


        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        void Insert(params TEntity[] entities);

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Inserts a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
        ValueTask<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Inserts a range of entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
        Task InsertAsync(params TEntity[] entities);

        /// <summary>
        /// Inserts a range of entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Update(params TEntity[] entities);

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes the entity by the specified primary key.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        void Delete(object id);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Delete(params TEntity[] entities);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Add and Save
        /// </summary>
        /// <param name="entity">The entities.</param>
        /// <returns></returns>
        bool InsertSaveChange(TEntity entity);

        /// <summary>
        /// Add and Save
        /// </summary>
        /// <param name="entity">The entities.</param>
        /// <returns></returns>
        bool InsertSaveChange(List<TEntity> entity);

        /// <summary>
        /// Update and Save
        /// </summary>
        /// <param name="entity">The entities.</param>
        /// <returns></returns>
        bool UpdateSaveChange(TEntity entity);

        /// <summary>
        /// Update and Save
        /// </summary>
        /// <param name="entity">The entities.</param>
        /// <returns></returns>
        bool UpdateSaveChange(List<TEntity> entity);

        /// <summary>
        /// Delete And Save
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool DeleteSaveChanges(TEntity entity);

        /// <summary>
        /// Delete And Save
        /// </summary>
        /// <param name="entity">The entities.</param>
        /// <returns></returns>
        bool DeleteSaveChanges(List<TEntity> entity);
    }
}