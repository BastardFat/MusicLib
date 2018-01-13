using System.Data.Entity;
using System.Threading.Tasks;

namespace MusicLib.Framework.Common
{
    public abstract class UnitOfWorkBase<TDbContext> : IUnitOfWork<TDbContext>
        where TDbContext : DbContext
    {
        protected UnitOfWorkBase(IDbContextFactory<TDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public virtual void Commit()
        {
            DbContext.SaveChanges();
        }

        public virtual async Task CommitAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        protected TDbContext DbContext => _dbContextFactory.GetDbContext();

        protected IDbContextFactory<TDbContext> DbContextFactory => _dbContextFactory;

        private readonly IDbContextFactory<TDbContext> _dbContextFactory;
    }
}
