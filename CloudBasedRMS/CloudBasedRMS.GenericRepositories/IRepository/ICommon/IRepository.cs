using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace CloudBasedRMS.GenericRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //Insert 
        bool Add(TEntity entity);
        bool AddRange(IEnumerable<TEntity> entities);
        //Delete
        bool Remove(TEntity entity);
        bool RemoveRange(IEnumerable<TEntity> entities);
        //Update
        bool Update(TEntity entity);
        //Select by id
        TEntity GetByID(string ID);
        //select all
        IEnumerable<TEntity> GetByAll();
        IEnumerable<TEntity> GetByAll(List<string> Includes);
        //select by
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
