using MusicLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLib.Framework.Common
{
    public interface IRepository<TEntity>
        where TEntity : EntityBase
    {
        TEntity GetById(long id);

        Task<TEntity> GetByIdAsync(long id);

        TEntity Create();

        TEntity Attach(long id);

        TEntity Attach(TEntity entity);

        IEnumerable<TEntity> Attach(IEnumerable<TEntity> entity);

        TEntity Add(TEntity entities);

        IEnumerable<TEntity> Add(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);

        TEntity AddOrUpdate(TEntity entity);

        IEnumerable<TEntity> AddOrUpdate(IEnumerable<TEntity> entities);

        System.Data.Entity.Infrastructure.DbEntityEntry<TEntity> Entry(TEntity entity);

        TEntity Delete(long id);

        Task<TEntity> DeleteAsync(long id);

        TEntity Delete(TEntity entity);

        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities);

        IQueryable<TEntity> Query();
    }
}