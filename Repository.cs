using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Fissoft;
using Fissoft.EntitySearch;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;

namespace Dreamless.Core
{
    /// <summary>
    /// Represents a default generic repository implements the <see cref="IRepository{TEntity}"/> interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual PagedList<TEntity> QueryPagedList(SearchModel searchModel, bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            return query.WhereToPagedList(searchModel);
        }

        public virtual PagedList<TResult> QueryPagedList<TResult>(SearchModel searchModel, Expression<Func<TEntity, TResult>> selector) where TResult : class
        {
            return _dbSet.Where(searchModel).Select(selector).Pager(searchModel);
        }

        /// <summary>
        /// 映射Mapper
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="searchModel">searchModel</param>
        /// <returns></returns>
        public virtual PagedList<TResult> QueryPagedMappingList<TResult>(SearchModel searchModel) where TResult : class
        {
            return _dbSet.Where(searchModel).SelectAndMapper<TResult>().Pager(searchModel);
        }

        public virtual List<TEntity> QueryList(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            return query.Where(predicate).ToList();
        }
        public List<TResult> QueryList<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, bool disableTracking = true) where TResult : class
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                return query.Where(predicate).Select(selector).ToList();
            }
            else
            {
                return query.Select(selector).ToList();
            }
        }
        public List<TResult> QueryMappingList<TResult>(Expression<Func<TEntity, bool>> predicate = null) where TResult : class
        {
            IQueryable<TEntity> query = _dbSet;
            if (predicate != null)
            {
                return query.AsNoTracking().Where(predicate).SelectAndMapper<TResult>().ToList();
            }
            else
            {
                return query.AsNoTracking().SelectAndMapper<TResult>().ToList();
            }
        }


        /// <summary>
        /// Gets the count based on a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return _dbSet.Count();
            }
            else
            {
                return _dbSet.Count(predicate);
            }
        }

        /// <summary>
        /// Inserts a new entity synchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        public virtual void Insert(TEntity entity)
        {
            var entry = _dbSet.Add(entity);
        }

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        public virtual void Insert(params TEntity[] entities) => _dbSet.AddRange(entities);

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        public virtual void Insert(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);

        /// <summary>
        /// Inserts a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
        public virtual Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbSet.AddAsync(entity, cancellationToken);

            // Shadow properties?
            //var property = _dbContext.Entry(entity).Property("Created");
            //if (property != null) {
            //property.CurrentValue = DateTime.Now;
            //}
        }

        /// <summary>
        /// Inserts a range of entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <returns>A <see cref="Task" /> that represents the asynchronous insert operation.</returns>
        public virtual Task InsertAsync(params TEntity[] entities) => _dbSet.AddRangeAsync(entities);

        /// <summary>
        /// Inserts a range of entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
        public virtual Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AddRangeAsync(entities, cancellationToken);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);

        }

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void Update(params TEntity[] entities) => _dbSet.UpdateRange(entities);

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void Update(IEnumerable<TEntity> entities) => _dbSet.UpdateRange(entities);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public virtual void Delete(TEntity entity) => _dbSet.Remove(entity);

        /// <summary>
        /// Deletes the entity by the specified primary key.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        public virtual void Delete(object id)
        {
            // using a stub entity to mark for deletion
            var typeInfo = typeof(TEntity).GetTypeInfo();
            var key = _dbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                property.SetValue(entity, id);
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                var entity = _dbSet.Find(id);
                if (entity != null)
                {
                    Delete(entity);
                }
            }
        }

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void Delete(params TEntity[] entities) => _dbSet.RemoveRange(entities);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void Delete(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate = null, bool disableTracking = false)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate == null)
            {
                return query.FirstOrDefault();
            }
            else
            {
                return query.FirstOrDefault(predicate);
            }
        }

        IEnumerable<TEntity> IRepository<TEntity>.FromSql(string sql, params object[] parameters) => _dbSet.FromSql(sql, parameters).ToList();

        public bool Any(Expression<Func<TEntity, bool>> predicate) => _dbSet.Any(predicate);

        public IQueryable<TEntity> Queryable(bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }

        public virtual bool InsertSaveChange(TEntity entity)
        {
            _dbSet.Add(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public virtual bool InsertSaveChange(List<TEntity> entity)
        {
            _dbSet.AddRange(entity);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Update and Save
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateSaveChange(TEntity entity)
        {
            _dbSet.Update(entity);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Update and Save
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateSaveChange(List<TEntity> entity)
        {
            _dbSet.UpdateRange(entity);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Delete And Save
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteSaveChanges(TEntity entity)
        {
            _dbSet.Remove(entity);
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Delete And Save
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteSaveChanges(List<TEntity> entity)
        {
            _dbSet.RemoveRange(entity);
            return _dbContext.SaveChanges() > 0;
        }

    }
}
