using Demkin.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace Demkin.Listen.WebApi.Admin.Application.Queries
{
    public class GetAlbumListQuery : IRequest<ApiResult<List<AlbumDetailDto>>>
    {
        public long CategoryId { get; set; }

        public string? Title { get; set; }
    }

    public class GetAlbumListQueryHandler : IRequestHandler<GetAlbumListQuery, ApiResult<List<AlbumDetailDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IDbContextFactory<ListenDbContext> _dbContextFactory;

        public GetAlbumListQueryHandler(IMapper mapper, IDbContextFactory<ListenDbContext> dbContextFactory)
        {
            _mapper = mapper;
            _dbContextFactory = dbContextFactory;
        }

        public async Task<ApiResult<List<AlbumDetailDto>>> Handle(GetAlbumListQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.CreateDbContext();

            var query = from a in dbContext.Albums
                        from c in dbContext.Categories.Where(c => a.CategoryId == c.Id).DefaultIfEmpty().Select(c => c.Title)
                        where a.CategoryId == request.CategoryId
                        select new AlbumDetailDto
                        {
                            Id = a.Id,
                            Title = a.Title,
                            CoverUrl = a.CoverUrl == null ? "" : a.CoverUrl.ToString(),
                            CategoryName = c,
                            SequenceNumber = a.SequenceNumber,
                            CreateTime = a.CreateTime
                        };
            var result = query.ToList();

            return ApiResultBuilder<List<AlbumDetailDto>>.Success(result);
        }
    }
}