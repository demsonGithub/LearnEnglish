using DotNetCore.CAP;
using Newtonsoft.Json;

namespace Demkin.Listen.WebApi.Admin.Application.IntegrationEvents
{
    public class UploadFileResultIntegrationEvent : ICapSubscribe
    {
        [CapSubscribe("UploadFileCompleted")]
        public Task UploadFileCompletedHandle(object parameters)
        {
            var completedInputParams = JsonConvert.DeserializeObject<UploadFileCompletedInputParams>(Convert.ToString(parameters));

            // todo

            return Task.CompletedTask;
        }
    }

    public class UploadFileCompletedInputParams
    {
        public string FileId { get; set; }

        public string FileUrl { get; set; }
    }
}