using MusicLib.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLib.Framework
{
    public class MusicLibUnitOfWorkImpl
        : UnitOfWorkBase<MusicLibDbContext>, IMusicLibUnitOfWork
    {
        public MusicLibUnitOfWorkImpl(IDbContextFactory<MusicLibDbContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
