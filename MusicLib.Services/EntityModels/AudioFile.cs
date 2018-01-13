using System;

namespace MusicLib.Services.EntityModels
{
    public class AudioFileModel : EntityBaseModel
    {
        public string DisplayName { get; set; }
        public string Hash { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
