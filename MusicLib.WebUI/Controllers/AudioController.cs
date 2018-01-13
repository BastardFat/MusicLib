using MusicLib.Framework.Web;
using MusicLib.Services.AudioService;
using MusicLib.Services.EntityModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MusicLib.WebUI.Controllers
{
    [RoutePrefix("audio")]
    public class AudioController : ApiController
    {
        private string AudioRoot => HttpContext.Current.Server.MapPath("~/App_Data/Audio");


        private readonly IAudioService _service;

        public AudioController(IAudioService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ApiResult<IEnumerable<AudioFileModel>>> ListAllFiles()
        {
            return await ApiResult<IEnumerable<AudioFileModel>>.Wrap(async f=>
            {
                return await _service.ListAllFiles();
            });
        }

        [HttpPost]
        [Route("delete")]
        public async Task<ApiResult<AudioFileModel>> DeleteFile(long id)
        {
            return await ApiResult<AudioFileModel>.Wrap(async f =>
            {
                return await _service.RemoveFile(id, AudioRoot);
            });
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ApiResult<AudioFileModel>> UploadFile()
        {
            return await ApiResult<AudioFileModel>.Wrap(async f =>
            {
                if (!Request.Content.IsMimeMultipartContent())
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

                var provider = new MultipartFormDataStreamProvider(AudioRoot);
                    await Request.Content.ReadAsMultipartAsync(provider);
                var file = provider.FileData.FirstOrDefault();
                if (file == null)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                var displayName = file.Headers.ContentDisposition.FileName.Trim('\"');
                if (displayName.EndsWith(".mp3"))
                    displayName = displayName.Substring(0, displayName.Length - 4);

                var result = await _service.UploadFile(File.ReadAllBytes(file.LocalFileName), displayName, AudioRoot);
                File.Delete(file.LocalFileName);
                return result;
            });
        }

        [HttpGet]
        [Route("listen")]
        public async Task<HttpResponseMessage> GetFileContent(long id)
        {

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(await _service.GetFileContent(id, AudioRoot))
            };

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = (await _service.GetFileName(id)) + ".mp3"
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("audio/mpeg");

            return result;
        }
       
    }
}
