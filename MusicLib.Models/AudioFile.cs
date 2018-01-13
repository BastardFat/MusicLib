using System;
using System.Collections.Generic;

namespace MusicLib.Models
{
    public class AudioFile : EntityBase
    {
        public virtual string DisplayName { get; set; }
        public virtual string Hash { get; set; }
        public virtual DateTime CreatedAt { get; set; }
    }
}
