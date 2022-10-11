using Microsoft.EntityFrameworkCore;

namespace Demkin.Listen.WebApi.Admin.Application.Queries
{
    public class GetEpisodeListQuery : IRequest<List<EpisodeDto>>
    {
        public long AlbumId { get; set; }
    }

    public class GetEpisodeListQueryHandler : IRequestHandler<GetEpisodeListQuery, List<EpisodeDto>>
    {
        private readonly ListenDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEpisodeListQueryHandler(IDbContextFactory<ListenDbContext> dbContextFactory, IMapper mapper)
        {
            _dbContext = dbContextFactory.CreateDbContext();
            _mapper = mapper;
        }

        public async Task<List<EpisodeDto>> Handle(GetEpisodeListQuery request, CancellationToken cancellationToken)
        {
            var episodeList = await _dbContext.Audios.Where(x => x.AlbumId == request.AlbumId).ToListAsync();

            var episodeDtoList = _mapper.Map<List<EpisodeDto>>(episodeList);

            return episodeDtoList;
        }
    }
}