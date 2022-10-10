using DotNetCore.CAP;

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
        private readonly ICapPublisher _capPublisher;
        private readonly IEpisodeRepository _episodeRepository;

        public AddEpisodeCommandHandler(ICapPublisher capPublisher, IEpisodeRepository episodeRepository)
        {
            _capPublisher = capPublisher;
            _episodeRepository = episodeRepository;
        }

        public async Task<string> Handle(AddEpisodeCommand request, CancellationToken cancellationToken)
        {
            // 1. 如果不是m4a格式，通知转码业务去转码,保证存入数据库的数据都是合法数据
            //if (!request.AudioUrl.EndsWith("m4a", StringComparison.OrdinalIgnoreCase))
            //{
            //    var builder = new Episode.Builder();
            //    builder.Title(request.Title)
            //        .Description(request.Description)
            //        .SequenceNumber(request.SequenceNumber)
            //        .AudioUrl(request.AudioUrl)
            //        .DurationInSecond(request.DurationInSecond)
            //        .Subtitles(request.Subtitles)
            //        .AlbumId(request.AlbumId);
            //    var entity = builder.Create();

            //    await _capPublisher.PublishAsync("Transcoding.Audio", new { MediaId = entity.Id, MediaUrl = entity.AudioUrl, OutputFormat = "m4a" });

            //    return entity.Id.ToString();
            //}
            //else
            //{
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

            await _capPublisher.PublishAsync("Transcoding.Audio", new { MediaId = entity.Id, MediaUrl = entity.AudioUrl, OutputFormat = "m4a" });

            return entity.Id.ToString();
            //}
        }
    }
}