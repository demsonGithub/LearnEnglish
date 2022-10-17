using Demkin.Listen.WebApi.Admin.Application.Models;
using Demkin.Listen.WebApi.Admin.Hubs;
using Demkin.Utils.IdGenerate;
using DotNetCore.CAP;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text.Json;

namespace Demkin.Listen.WebApi.Admin.Application.Commands
{
    public class AddEpisodeCommand : IRequest<bool>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int SequenceNumber { get; set; }

        public string AudioUrl { get; set; }

        public double DurationInSecond { get; set; }

        public string Subtitles { get; set; }

        public long AlbumId { get; set; }
    }

    public class AddEpisodeCommandHandler : IRequestHandler<AddEpisodeCommand, bool>
    {
        private readonly DomainService _domainService;
        private readonly ICapPublisher _capPublisher;
        private readonly IConnectionMultiplexer _redisConn;
        private readonly IEpisodeRepository _episodeRepository;
        private readonly IHubContext<TranscodeFileHub> _hubContext;

        public AddEpisodeCommandHandler(DomainService domainService, ICapPublisher capPublisher, IConnectionMultiplexer redisConn, IEpisodeRepository episodeRepository, IHubContext<TranscodeFileHub> hubContext)
        {
            _domainService = domainService;
            _capPublisher = capPublisher;
            _redisConn = redisConn;
            _episodeRepository = episodeRepository;
            _hubContext = hubContext;
        }

        public async Task<bool> Handle(AddEpisodeCommand request, CancellationToken cancellationToken)
        {
            // 1. 判断是否需要转码，保证存入数据库的数据都是合法数据
            var audioUrl = _domainService.IsNeedTranscode(request.AudioUrl);

            if (!audioUrl)
            {
                // 不需要转码，直接保存到数据库
                var builder = new Episode.Builder();
                builder.Title(request.Title)
                    .Description(request.Description)
                    .SequenceNumber(request.SequenceNumber)
                    .AudioUrl(request.AudioUrl)
                    .DurationInSecond(request.DurationInSecond)
                    .Subtitles(request.Subtitles)
                    .AlbumId(request.AlbumId);
                var entity = builder.Create();

                await _episodeRepository.AddAsync(entity, cancellationToken);
                return await _episodeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
            else
            {
                var redisKeyEpisodeId = Constant.RedisPrefix_Episode + IdGenerateHelper.Instance.GenerateId();

                var episodeFileInfo = new EpisodeFileInfo()
                {
                    RedisKey = redisKeyEpisodeId,
                    Title = request.Title,
                    Description = request.Description,
                    SequenceNumber = request.SequenceNumber,
                    SourceFileUrl = request.AudioUrl,
                    DurationInSecond = request.DurationInSecond,
                    Subtitles = request.Subtitles,
                    AlbumId = request.AlbumId,
                    CurrentStatus = EpisodeTranscodeState.Create
                };
                // 将信息保存到redis
                var redisDb = _redisConn.GetDatabase();
                await redisDb.StringSetAsync(redisKeyEpisodeId, JsonConvert.SerializeObject(episodeFileInfo));

                await _hubContext.Clients.All.SendAsync("RecieveMessage", new
                {
                    TranscodeStatus = TranscodeStatus.Ready,
                    Title = request.Title,
                    CreateTime = DateTime.Now,
                });

                // 将转码过程交给转码服务
                await _capPublisher.PublishAsync("Transcoding.Audio", new
                {
                    RedisKey = redisKeyEpisodeId,
                    FileTitle = request.Title,
                    FileSourceUrl = request.AudioUrl,
                    OutputFormat = "m4a"
                });

                return false;
            }
        }
    }
}