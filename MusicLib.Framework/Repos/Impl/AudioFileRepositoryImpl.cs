using MusicLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLib.Framework.Common;

namespace MusicLib.Framework.Repos.Impl
{
    public class AudioFileRepositoryImpl : MusicLibRepositoryBase<AudioFile>, IAudioFileRepository
    {
        public AudioFileRepositoryImpl(IDbContextFactory<MusicLibDbContext> contextFactory) : base(contextFactory)
        {
        }
    }
}
