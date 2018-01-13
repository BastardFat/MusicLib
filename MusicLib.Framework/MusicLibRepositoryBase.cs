using MusicLib.Framework.Common;
using MusicLib.Models;

namespace MusicLib.Framework
{
    public abstract class MusicLibRepositoryBase<TEntity>
        : RepositoryBase<TEntity, MusicLibDbContext>
        where TEntity : EntityBase, new()
    {
        protected MusicLibRepositoryBase(IDbContextFactory<MusicLibDbContext> contextFactory) : base(contextFactory)
        {
        }
    }
}
