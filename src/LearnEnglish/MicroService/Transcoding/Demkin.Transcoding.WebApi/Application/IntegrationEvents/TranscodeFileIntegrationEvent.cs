using DotNetCore.CAP;
using Newtonsoft.Json;

namespace Demkin.Transcoding.WebApi.Application.IntegrationEvents
{
    public class TranscodeFileIntegrationEvent : ICapSubscribe
    {
        private readonly ICapPublisher _capPublisher;

        public TranscodeFileIntegrationEvent(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        [CapSubscribe("Transcoding.Audio")]
        public async Task HandleTransodeFile(object parameters)
        {
            //将object对象转换为json字符
            //var objJson = JsonConvert.SerializeObject(parameters);
            //将json字符转换为实体对象
            TranscodeFileInputParams inputParams = JsonConvert.DeserializeObject<TranscodeFileInputParams>(parameters.ToString());
            Thread.Sleep(3000);

            Console.WriteLine(inputParams.MediaIdKey);
            Thread.Sleep(3000);
            Console.WriteLine(inputParams.MediaUrl);
            await _capPublisher.PublishAsync("Transcoding.Audio.Completed",
                new
                {
                    HasTranscodeFileUrl = "12312331.m4a"
                });
        }
    }

    public class TranscodeFileInputParams
    {
        public string MediaIdKey { get; set; }

        public string MediaUrl { get; set; }

        public string OutputFormat { get; set; }
    }
}