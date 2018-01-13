using System;

namespace MusicLib.Framework.Common
{
    public abstract class Disposable : IDisposable
    {
        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                DisposeCore();
            }

            IsDisposed = true;
        }

        protected abstract void DisposeCore();

        protected bool IsDisposed { get; private set; }

    }
}
