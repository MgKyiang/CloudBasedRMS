using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace CloudBasedRMS.GenericRepositories
{

    //IReposistory interface with the generic pattern:
    //public interface [interfaceName]<TEntity> where TEntity:class
    public interface IRepository<TEntity> where TEntity : class
    {
        //Insert(Create) 
        bool Add(TEntity entity);
        bool AddRange(IEnumerable<TEntity> entities);
        //Delete(Delete)
        bool Remove(TEntity entity);
        bool RemoveRange(IEnumerable<TEntity> entities);
        //Update(Update)
        bool Update(TEntity entity);
        //Select by id(select by id)
        TEntity GetByID(string ID);
        //select all(select )
        IEnumerable<TEntity> GetByAll();
        IEnumerable<TEntity> GetByAll(List<string> Includes);
        //select by
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
