using System;
using System.Data.Entity;

namespace MusicLib.Framework.Common
{
    public abstract class DbContextFactoryBase<TDbConetxt> : Disposable, IDbContextFactory<TDbConetxt>
        where TDbConetxt : DbContext, new()
    {
        public virtual TDbConetxt GetDbContext()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException("ContextFactory");
            }
            return _context ?? (_context = new TDbConetxt());
        }

        protected override void DisposeCore()
        {
            _context?.Dispose();
        }

        private TDbConetxt _context;
    }
}
