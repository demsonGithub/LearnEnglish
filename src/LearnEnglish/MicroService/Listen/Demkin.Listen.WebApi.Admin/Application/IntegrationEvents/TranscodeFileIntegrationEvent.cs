using Demkin.Listen.WebApi.Admin.Application.Models;
using Demkin.Listen.WebApi.Admin.Hubs;
using DotNetCore.CAP;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Demkin.Listen.WebApi.Admin.Application.IntegrationEvents
{
    public class TranscodeFileIntegrationEvent : ICapSubscribe
    {
        private readonly IConnectionMultiplexer _redisConn;
        private readonly IEpisodeRepository _episodeRepository;
        private readonly IHubContext<TranscodeFileHub> _hubContext;

        public TranscodeFileIntegrationEvent(IConnectionMultiplexer redisConn, IEpisodeRepository episodeRepository, IHubContext<TranscodeFileHub> hubContext)
        {
            _redisConn = redisConn;
            _episodeRepository = episodeRepository;
            _hubContext = hubContext;
        }

        [CapSubscribe(Constant.Transcode_Result_Event)]
        public async Task TranscodeAudio(object obj)
        {
            var parameter = JsonConvert.DeserializeObject<TranscodeFileResultInputParams>(Convert.ToString(obj));

            var redisDb = _redisConn.GetDatabase();
            var episodeInfo = await redisDb.StringGetAsync(parameter.RedisKey);
            EpisodeFileInfo episodeFileInfo = JsonConvert.DeserializeObject<EpisodeFileInfo>(episodeInfo);

            switch (parameter.CurrentStatus)
            {
                case TranscodeStatus.Started:

                    await _hubContext.Clients.All.SendAsync("RecieveMessage",
                        new
                        {
                            TranscodeStatus = TranscodeStatus.Started,
                            Title = episodeFileInfo.Title,
                            CreateTime = DateTime.Now,
                        });
                    break;

                case TranscodeStatus.Completed:

                    // 将结果保存到数据库
                    var builder = new Episode.Builder();
                    builder.Title(episodeFileInfo.Title)
                        .Description(episodeFileInfo.Description)
                        .SequenceNumber(episodeFileInfo.SequenceNumber)
                        .DurationInSecond(episodeFileInfo.DurationInSecond)
                        .Subtitles(episodeFileInfo.Subtitles)
                        .AlbumId(episodeFileInfo.AlbumId)
                        .AudioUrl(parameter.TranscodingUrl);
                    var entity = builder.Create();

                    await _episodeRepository.AddAsync(entity);
                    await _episodeRepository.UnitOfWork.SaveEntitiesAsync();

                    await redisDb.KeyDeleteAsync(parameter.RedisKey);
                    await _hubContext.Clients.All.SendAsync("RecieveMessage", new
                    {
                        TranscodeStatus = TranscodeStatus.Completed,
                        Title = episodeFileInfo.Title,
                        CreateTime = DateTime.Now,
                    });

                    break;

                case TranscodeStatus.Failed:
                    await _hubContext.Clients.All.SendAsync("RecieveMessage",
                        new
                        {
                            TranscodeStatus = TranscodeStatus.Failed,
                            Title = episodeFileInfo.Title,
                            CreateTime = DateTime.Now,
                        });
                    await redisDb.KeyDeleteAsync(parameter.RedisKey);
                    break;

                default:
                    break;
            }
        }
    }
}