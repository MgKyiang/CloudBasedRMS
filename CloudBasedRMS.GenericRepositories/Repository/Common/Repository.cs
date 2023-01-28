using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
namespace CloudBasedRMS.GenericRepositories
{
    //Repositry class By implementaion wiht IReposityry Interface 
    //class [ClassName]<TEntity>:IRepository<Tentity> where TEntity :class
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        /// <summary>
        /// Insert Entity[model]
        /// </summary>
        /// <param name="entity">Entity</param>
        public bool Add(TEntity entity)
        {
            try
            {
                _dbContext.Set<TEntity>().Add(entity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /// <summary>
        /// Insert Entities
        /// </summary>
        /// <param name="entities">entities</param>
        public bool AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _dbContext.Set<TEntity>().AddRange(entities);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Find / Where
        /// </summary>
        /// <param name="predicate">LINQ Expression</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return _dbContext.Set<TEntity>().Where(predicate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// GET by ALL
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetByAll()
        {
            try
            {
                return _dbContext.Set<TEntity>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// GetByAll with Include Method
        /// </summary>
        /// <param name="Includes"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetByAll(List<string> Includes)
        {
            try
            {
                IQueryable<TEntity> query = _dbContext.Set<TEntity>();

                foreach (string includeProperty in Includes)
                {
                    query.Include(includeProperty);
                }
                return query;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// GetByID
        /// </summary>
        /// <param name="ID">PK_ID</param>
        /// <returns>Entity</returns>
        public TEntity GetByID(string ID)
        {
            try
            {
                return _dbContext.Set<TEntity>().Find(ID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="entity"></param>
        public bool Remove(TEntity entity)
        {
            try
            {
                _dbContext.Set<TEntity>().Remove(entity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        /// <summary>
        /// Delete Entities
        /// </summary>
        /// <param name="entities">entities</param>
        public bool RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _dbContext.Set<TEntity>().RemoveRange(entities);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// linq where singleOrdefault
        /// </summary>
        /// <param name="predicate">LINQ Expression</param>
        /// <returns></returns>
        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return _dbContext.Set<TEntity>().SingleOrDefault(predicate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        public bool Update(TEntity entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
