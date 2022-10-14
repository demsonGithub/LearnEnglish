using System.Net;

namespace Demkin.Transcoding.WebApi.Extensions
{
    public static class HttpHelper
    {
        public static async Task SaveToFileAsync(this HttpResponseMessage respMsg, string file,
            CancellationToken cancellationToken = default)
        {
            if (respMsg.IsSuccessStatusCode == false)
            {
                throw new ArgumentException($"StatusCode of HttpResponseMessage is {respMsg.StatusCode}", nameof(respMsg));
            }
            using FileStream fs = new FileStream(file, FileMode.Create);
            await respMsg.Content.CopyToAsync(fs, cancellationToken);
        }

        public static async Task<HttpStatusCode> DownloadFileAsync(this HttpClient httpClient, Uri url, string localFile,
            CancellationToken cancellationToken = default)
        {
            var resp = await httpClient.GetAsync(url, cancellationToken);
            if (resp.IsSuccessStatusCode)
            {
                await SaveToFileAsync(resp, localFile, cancellationToken);
                return resp.StatusCode;
            }
            else
            {
                return HttpStatusCode.OK;
            }
        }
    }
}