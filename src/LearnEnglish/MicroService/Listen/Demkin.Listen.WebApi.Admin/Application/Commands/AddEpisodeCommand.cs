using Demkin.Listen.WebApi.Admin.Application.Models;
using Demkin.Utils.IdGenerate;
using DotNetCore.CAP;
using StackExchange.Redis;
using System.Text.Json;

namespace Demkin.Listen.WebApi.Admin.Application.Commands
{
    public class AddEpisodeCommand : IRequest<string>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int SequenceNumber { get; set; }

        public string AudioUrl { get; set; }

        public double DurationInSecond { get; set; }

        public string Subtitles { get; set; }

        public long AlbumId { get; set; }
    }

    public class AddEpisodeCommandHandler : IRequestHandler<AddEpisodeCommand, string>
    {
        private readonly DomainService _domainService;
        private readonly ICapPublisher _capPublisher;
        private readonly IConnectionMultiplexer _redisConn;
        private readonly IEpisodeRepository _episodeRepository;

        public AddEpisodeCommandHandler(DomainService domainService, ICapPublisher capPublisher, IConnectionMultiplexer redisConn, IEpisodeRepository episodeRepository)
        {
            _domainService = domainService;
            _capPublisher = capPublisher;
            _redisConn = redisConn;
            _episodeRepository = episodeRepository;
        }

        public async Task<string> Handle(AddEpisodeCommand request, CancellationToken cancellationToken)
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
                await _episodeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                // todo 通知前端刷新

                return entity.Id.ToString(); ;
            }
            else
            {
                var episodeId = IdGenerateHelper.Instance.GenerateId();
                // 待转码文件信息保存到redis
                string redisKeyForEpisode = "Demkin.Listen.Episode." + episodeId;
                var redisDb = _redisConn.GetDatabase();
                var episodeFileInfo = new EpisodeFileInfo()
                {
                    Title = request.Title,
                    Description = request.Description,
                    SequenceNumber = request.SequenceNumber,
                    DurationInSecond = request.DurationInSecond,
                    Subtitles = request.Subtitles,
                    AlbumId = request.AlbumId,
                };
                await redisDb.StringSetAsync(redisKeyForEpisode, JsonSerializer.Serialize(episodeFileInfo));

                // 通知转码
                await _capPublisher.PublishAsync("Transcoding.Audio", new
                {
                    RedisKey = redisKeyForEpisode,
                    MediaUrl = request.AudioUrl,
                    OutputFormat = "m4a"
                });

                return "";
            }
        }
    }
}