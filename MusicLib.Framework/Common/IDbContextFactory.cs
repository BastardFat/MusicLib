using System.Data.Entity;

namespace MusicLib.Framework.Common
{
    public interface IDbContextFactory<out TDbContext> where TDbContext : DbContext
    {
        TDbContext GetDbContext();
    }
}
