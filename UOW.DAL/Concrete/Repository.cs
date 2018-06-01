using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using UOW.DAL.Abstruct;

namespace UOW.DAL.Concrete
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Adds entity into current context
        /// </summary>
        /// <param name="entity">Entity to add</param>
        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Adds entity collection into current context
        /// </summary>
        /// <param name="entities">Collection of Entity to add</param>
        public void AddRange(System.Collections.Generic.IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        /// <summary>
        /// Fetches collection of entity using condition
        /// </summary>
        /// <param name="predicate">Linq expression</param>
        /// <returns>Entity Collection</returns>
        public System.Collections.Generic.IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        /// <summary>
        /// Gets an entity by Id
        /// </summary>
        /// <param name="id">Id of entity</param>
        /// <returns>Entity</returns>
        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Gets all entitiy
        /// </summary>
        /// <returns>Entity Collection</returns>
        public System.Collections.Generic.IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        /// <summary>
        /// Removes entity from current context
        /// </summary>
        /// <param name="entity">Db Entity</param>
        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Removes entity collection from current context
        /// </summary>
        /// <param name="entities">Collection of Entity to remove</param>
        public void RemoveRange(System.Collections.Generic.IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        /// <summary>
        /// Gets Single or Default entity
        /// </summary>
        /// <param name="predicate">Linq expression</param>
        /// <returns>Db Entity</returns>
        public TEntity SingleOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }
        /// <summary>
        /// Gets number of rows in a table
        /// </summary>
        /// <returns>Number of row</returns>
        public long Count()
        {
            string name = (Context as IObjectContextAdapter).ObjectContext.CreateObjectSet<TEntity>().EntitySet.Name;
            string tableName = name.Substring(0, name.Length - 1);
            string query = "EXEC SP_GetRowCount '" + tableName + "'";
            long rowCount = Context.Database.SqlQuery<long>(query)
                .FirstOrDefault();
            return rowCount;
        }
    }
}