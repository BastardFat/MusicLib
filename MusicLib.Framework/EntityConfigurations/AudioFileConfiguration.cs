using MusicLib.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLib.Framework.EntityConfigurations
{
    public class AudioFileConfiguration
        : EntityTypeConfiguration<AudioFile>
    {
        public AudioFileConfiguration()
        {
            ToTable("files");
            HasKey(x => x.Id);
        }
    }
}
