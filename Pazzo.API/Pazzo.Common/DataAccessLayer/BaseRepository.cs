using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pazzo.Common.DataAccessLayer
{
    /// <summary>
    /// It's a BaseRepository for generic type
    /// </summary>
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbContext _dbContext;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<T> Queryable()
        {
            return _dbContext.Set<T>().AsQueryable().AsNoTracking();
        }

        public virtual int Delete(T obj)
        {
            return DeleteAsync(obj).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual async Task<int> DeleteAsync(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            _dbContext.Set<T>().Remove(obj);

            return 1;
        }

        public virtual int Delete(IEnumerable<T> list)
        {
            return DeleteAsync(list).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual async Task<int> DeleteAsync(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            _dbContext.Set<T>().RemoveRange(list);

            return 1;
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return GetAsync(predicate).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return GetAllAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate)
        {
            return GetManyAsync(predicate).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.Where(predicate).AsNoTracking().ToListAsync();
        }

        public virtual int Insert(T obj)
        {
            return InsertAsync(obj).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual async Task<int> InsertAsync(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            _dbContext.Set<T>().Add(obj);
            _dbContext.ChangeTracker.DetectChanges();
            return 1;
        }

        public virtual int Insert(IEnumerable<T> list)
        {
            return InsertAsync(list).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual async Task<int> InsertAsync(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            _dbContext.Set<T>().AddRange(list);

            return 1;
        }

        public virtual int Update(T obj)
        {
            return UpdateAsync(obj).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual async Task<int> UpdateAsync(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            _dbContext.Set<T>().Update(obj);

            return 1;
        }

        public virtual int Update(IEnumerable<T> list)
        {
            return UpdateAsync(list).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual async Task<int> UpdateAsync(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            _dbContext.Set<T>().UpdateRange(list);

            return 1;
        }

        /// <summary>
        /// entityNew 如果是空值，不更新資料至Table
        /// </summary>
        /// <param name="entityOri">原DB資料</param>
        /// <param name="entityNew">欲更新的資料</param>
        /// <returns></returns>
        public async Task<T> UpdateIfNotNullAsync(T entityOri, T entityNew)
        {
            var entity = this.SetEntityValueIfNotNull(entityOri, entityNew);

            await this.UpdateAsync(entity);

            return entity;
        }

        /// <summary>
        /// entityNew 如果是空值，不更新資料至Table
        /// </summary>
        /// <param name="entityOri">原DB資料</param>
        /// <param name="entityNew">欲更新的資料</param>
        /// <returns></returns>
        public async Task<List<T>> UpdateIfNotNullAsync(List<T> entityOris, List<T> entityNews)
        {
            var entities = new List<T>();
            for (var i = 0; i < entityOris.Count; i++)
            {
                var entityOri = entityOris[i];
                var entityNew = entityNews[i];

                var entity = this.SetEntityValueIfNotNull(entityOri, entityNew);
                entities.Add(entity);
            }

            await this.UpdateAsync(entities);

            return entities;
        }

        private T SetEntityValueIfNotNull(T entityOri, T entityNew)
        {
            foreach (var p in entityOri.GetType().GetProperties())
            {
                var typeName = p.PropertyType.GenericTypeArguments.Any() ?
                        p.PropertyType.GenericTypeArguments[0].Name : p.PropertyType.Name;

                var newValue = p.GetValue(entityNew, null);

                if (typeName == "Int32" || typeName == "Decimal")
                {
                    if (Convert.ToInt32(newValue) == 0) // 0 不更新
                        continue;
                }

                if (!string.IsNullOrEmpty(newValue?.ToString())) // 不等於空值才更新
                {
                    p.SetValue(entityOri, newValue, null);
                }
            }

            return entityOri;
        }

        public virtual IEnumerable<T> ExecuteSqlQuery(string sql, object parameters)
        {
            return ExecuteSqlQueryAsync(sql, parameters).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual async Task<IEnumerable<T>> ExecuteSqlQueryAsync(string sql, object parameters)
        {
            using (IDbConnection conn = _dbContext.Database.GetDbConnection())
            {
                conn.Open();

                return await conn.QueryAsync<T>(sql, parameters);
            }
        }

        public virtual IEnumerable<TEntity> ExecuteSqlQuery<TEntity>(string sql, object parameters)
        {
            return ExecuteSqlQueryAsync<TEntity>(sql, parameters).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual async Task<IEnumerable<TEntity>> ExecuteSqlQueryAsync<TEntity>(string sql, object parameters)
        {
            using (IDbConnection conn = _dbContext.Database.GetDbConnection())
            {
                conn.Open();

                return await conn.QueryAsync<TEntity>(sql, parameters);
            }
        }

        public virtual int ExecuteSql(string sql, object parameters)
        {
            return ExecuteSqlAsync(sql, parameters).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public virtual async Task<int> ExecuteSqlAsync(string sql, object parameters)
        {
            using (IDbConnection conn = _dbContext.Database.GetDbConnection())
            {
                conn.Open();

                return await conn.ExecuteAsync(sql, parameters);
            }
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_dbContext.Database.GetConnectionString());
        }
    }
}