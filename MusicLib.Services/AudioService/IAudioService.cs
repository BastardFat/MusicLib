using MusicLib.Services.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLib.Services.AudioService
{
    public interface IAudioService
    {
        Task<AudioFileModel> UploadFile(byte[] content, string filename, string folderpath);
        Task<AudioFileModel> RemoveFile(long id, string folderpath);
        Task<IEnumerable<AudioFileModel>> ListAllFiles();
        Task<byte[]> GetFileContent(long id, string folderpath);
        Task<string> GetFileName(long id);
    }
}
