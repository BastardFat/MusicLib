using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLib.Services.EntityModels;
using MusicLib.Framework.Repos;
using AutoMapper;
using MusicLib.Models;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;
using System.Data.HashFunction;
using System.IO;
using MusicLib.Framework;

namespace MusicLib.Services.AudioService.Impl
{
    public class AudioServiceImpl : IAudioService
    {
        private readonly IAudioFileRepository _audioFileRepository;
        private readonly IMusicLibUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IHashFunction _hasher;

        public AudioServiceImpl(IAudioFileRepository audioFileRepository, IMusicLibUnitOfWork uow)
        {
            _audioFileRepository = audioFileRepository;
            _hasher = new Blake2B(256);
            _uow = uow;

            _mapper = new MapperConfiguration(x =>
            {
                x.CreateMap<AudioFile, AudioFileModel>()
                    .ReverseMap();
            }).CreateMapper();
        }

        public async Task<byte[]> GetFileContent(long id, string folderpath)
        {
            var entity = await _audioFileRepository.GetByIdAsync(id);
            var fullname = Path.Combine(folderpath, entity.Hash);
            return File.ReadAllBytes(fullname);
        }

        public async Task<string> GetFileName(long id)
        {
            return (await _audioFileRepository.GetByIdAsync(id)).DisplayName;
        }


        public async Task<IEnumerable<AudioFileModel>> ListAllFiles()
        {
            return await _audioFileRepository
                .Query()
                .ProjectTo<AudioFileModel>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        public async Task<AudioFileModel> RemoveFile(long id, string folderpath)
        {
            var entity = await _audioFileRepository.GetByIdAsync(id);
            if (await _audioFileRepository.Query().CountAsync(x => x.Hash == entity.Hash) == 1)
            {
                var fullname = Path.Combine(folderpath, entity.Hash);
                if (File.Exists(fullname))
                    File.Delete(fullname);
            }

            entity = await _audioFileRepository.DeleteAsync(id);
            await _uow.CommitAsync();
            return _mapper.Map<AudioFile, AudioFileModel>(entity);
        }

        public async Task<AudioFileModel> UploadFile(byte[] content, string filename, string folderpath)
        {
            var hash = BitConverter.ToString(_hasher.ComputeHash(content)).Replace("-", "");
            var fullname = Path.Combine(folderpath, hash);
            if (!File.Exists(fullname))
                File.WriteAllBytes(fullname, content);

            var entity = new AudioFile
            {
                CreatedAt = DateTime.Now,
                DisplayName = filename,
                Hash = hash
            };

            entity = _audioFileRepository.Add(entity);
            await _uow.CommitAsync();

            return _mapper.Map<AudioFile, AudioFileModel>(entity);
        }
    }
}
