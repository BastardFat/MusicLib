using System.Data.Entity;
using System.Threading.Tasks;

namespace MusicLib.Framework.Common
{
    public interface IUnitOfWork<out TDbContext> where TDbContext : DbContext
    {
        void Commit();
        Task CommitAsync();
    }
}
