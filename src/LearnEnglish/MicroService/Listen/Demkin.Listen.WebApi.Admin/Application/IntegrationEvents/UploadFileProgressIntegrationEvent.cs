using Demkin.Listen.WebApi.Admin.Hubs;
using DotNetCore.CAP;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Demkin.Listen.WebApi.Admin.Application.IntegrationEvents
{
    public class UploadFileProgressIntegrationEvent : ICapSubscribe
    {
        private readonly IHubContext<UploadFileHub> _hubContext;

        public UploadFileProgressIntegrationEvent(IHubContext<UploadFileHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [CapSubscribe("FileOperation.UploadFile.Progress")]
        public Task UploadFileProgressHandle(object completedProgress)
        {
            var parameter = JsonConvert.DeserializeObject<UploadFileProgressInputParams>(Convert.ToString(completedProgress));

            _hubContext.Clients.Client(parameter?.IdentityId).SendAsync("RecieveMessage", parameter.CompletedProgress);

            return Task.CompletedTask;
        }
    }

    public class UploadFileProgressInputParams
    {
        public string IdentityId { get; set; }

        public int CompletedProgress { get; set; }
    }
}