using Demkin.Listen.WebApi.Admin.Hubs;
using DotNetCore.CAP;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Demkin.Listen.WebApi.Admin.Application.IntegrationEvents
{
    public class UploadFileProgressIntegrationEvent : ICapSubscribe
    {
        private readonly IHubContext<UploadFileHub> _hubContext;
        private readonly IConnectionMultiplexer _redisConn;

        public UploadFileProgressIntegrationEvent(IHubContext<UploadFileHub> hubContext, IConnectionMultiplexer redisConn)
        {
            _hubContext = hubContext;
            _redisConn = redisConn;
        }

        [CapSubscribe("FileOperation.UploadFile.Progress")]
        public Task UploadFileProgressHandle(object obj)
        {
            var parameter = JsonConvert.DeserializeObject<UploadFileProgressInputParams>(Convert.ToString(obj));

            var redisDb = _redisConn.GetDatabase();
            return Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var completedProgress = await redisDb.StringGetAsync(parameter.RedisCacheKey);
                    await _hubContext.Clients.Client(parameter?.IdentityId).SendAsync("RecieveMessage", Convert.ToInt32(completedProgress));

                    Thread.Sleep(1000);
                    if (Convert.ToInt32(completedProgress) >= 100)
                    {
                        redisDb.KeyDelete(parameter.RedisCacheKey);
                        break;
                    }
                }
            });
        }
    }

    public class UploadFileProgressInputParams
    {
        public string IdentityId { get; set; }

        public string RedisCacheKey { get; set; }
    }
}