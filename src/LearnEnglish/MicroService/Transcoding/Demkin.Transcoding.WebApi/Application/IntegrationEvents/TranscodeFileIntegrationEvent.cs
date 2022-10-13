using Demkin.Transcoding.Domain;
using DotNetCore.CAP;
using Newtonsoft.Json;

namespace Demkin.Transcoding.WebApi.Application.IntegrationEvents
{
    public class TranscodeFileIntegrationEvent : ICapSubscribe
    {
        private readonly ICapPublisher _capPublisher;
        private readonly TranscodeService _transcodeService;

        public TranscodeFileIntegrationEvent(ICapPublisher capPublisher, TranscodeService transcodeService)
        {
            _capPublisher = capPublisher;
            _transcodeService = transcodeService;
        }

        [CapSubscribe("Transcoding.Audio")]
        public async Task HandleTransodeFile(object parameters)
        {
            //将json字符转换为实体对象
            TranscodeFileInputParams? inputParams = JsonConvert.DeserializeObject<TranscodeFileInputParams>(Convert.ToString(parameters));

            await _capPublisher.PublishAsync("Transcoding.Audio.Result", new { RedisKey = inputParams.RedisKey, Status = "Started", HasTranscodeFileUrl = "" });

            var targetFileUrl = await _transcodeService.TranscodeFileToM4a();

            // 转码完成，反馈结果
            await _capPublisher.PublishAsync("Transcoding.Audio.Result",
                new
                {
                    RedisKey = inputParams.RedisKey,
                    Status = "Completed",
                    HasTranscodeFileUrl = targetFileUrl
                });
        }
    }

    public class TranscodeFileInputParams
    {
        public string RedisKey { get; set; }

        public string MediaUrl { get; set; }

        public string OutputFormat { get; set; }
    }
}