using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLib.Framework.Web
{
    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public string Error { get; set; }   

        public static async Task<ApiResult<T>> Wrap(Func<ApiResult<T>, Task<T>> action)
        {
            var apiResult = new ApiResult<T>();
            try
            {
                apiResult.Result = await action(apiResult);
                apiResult.IsSuccess = true;
            }
            catch (Exception ex)
            {
                apiResult.IsSuccess = false;
                apiResult.Error = ex.Message;
            }
            return apiResult;
        }
    }
}
