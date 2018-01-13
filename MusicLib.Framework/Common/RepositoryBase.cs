using MusicLib.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLib.Framework.Common
{
    public abstract class RepositoryBase<TEntity, TDbContext> : IRepository<TEntity>
        where TEntity : EntityBase, new() where TDbContext : DbContext
    {
        protected RepositoryBase(IDbContextFactory<TDbContext> contextFactory)
        {
            DbContextFactory = contextFactory;
        }

        public virtual TEntity GetById(long id)
        {
            return DbContextFactory.GetDbContext().Set<TEntity>().FirstOrDefault(x => x.Id == id);
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await DbContextFactory.GetDbContext().Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public TEntity Create()
        {
            return DbContextFactory.GetDbContext().Set<TEntity>().Create();
        }

        public TEntity Attach(long id)
        {
            return Attach(new TEntity { Id = id });
        }

        public TEntity Attach(TEntity entity)
        {
            var entitySet = DbContextFactory.GetDbContext().Set<TEntity>();
            var entry = entitySet.Local.FirstOrDefault(x => x.Id == entity.Id) ??
                        DbContextFactory.GetDbContext().Set<TEntity>().Attach(entity);
            return entry;
        }

        public IEnumerable<TEntity> Attach(IEnumerable<TEntity> entities)
        {
            return entities.Select(Attach).ToArray();
        }

        public TEntity Add(TEntity entity)
        {
            return Set.Add(entity);
        }

        public IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            return Set.AddRange(entities);
        }

        public TEntity Update(TEntity entity)
        {
            Attach(entity);
            var entry = DbContext.Entry(entity);
            entry.State = EntityState.Modified;
            return entry.Entity;
        }

        public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            return entities.Select(Update).ToArray();
        }

        public TEntity AddOrUpdate(TEntity entity)
        {
            return entity.Id == 0 ? Add(entity) : Update(entity);
        }

        public IEnumerable<TEntity> AddOrUpdate(IEnumerable<TEntity> entities)
        {
            return entities.Select(AddOrUpdate).ToArray();
        }

        public System.Data.Entity.Infrastructure.DbEntityEntry<TEntity> Entry(TEntity entity)
        {
            return DbContext.Entry(entity);
        }

        public TEntity Delete(TEntity entity)
        {
            return Set.Remove(entity);
        }

        public TEntity Delete(long id)
        {
            var entity = GetById(id);
            return Delete(entity);
        }

        public async Task<TEntity> DeleteAsync(long id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
                entity = Delete(entity);

            return entity;
        }

        public IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
        {
            return Set.RemoveRange(entities);
        }

        public IQueryable<TEntity> Query()
        {
            return DbContextFactory.GetDbContext().Set<TEntity>();
        }


        protected DbSet<TEntity> Set => DbContext.Set<TEntity>();
        protected DbContext DbContext => DbContextFactory.GetDbContext();
        protected IDbContextFactory<TDbContext> DbContextFactory { get; }
    }
}
