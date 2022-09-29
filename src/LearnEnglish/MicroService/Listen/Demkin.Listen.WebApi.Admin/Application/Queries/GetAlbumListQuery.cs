using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Demkin.Listen.WebApi.Admin.Application.Queries
{
    public class GetAlbumListQuery : IRequest<ApiResult<List<AlbumDetailViewModel>>>
    {
        public long CategoryId { get; set; }

        public string? Title { get; set; }
    }

    public class GetAlbumListQueryHandler : IRequestHandler<GetAlbumListQuery, ApiResult<List<AlbumDetailViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetAlbumListQueryHandler(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ApiResult<List<AlbumDetailViewModel>>> Handle(GetAlbumListQuery request, CancellationToken cancellationToken)
        {
            var connectionString = "server=192.168.0.143;uid=sa;pwd=abc123#;database=LearnEnglish_Listen;";
            var dbContext = new ListenDbContext(_mediator, connectionString);

            var query = from a in dbContext.Albums
                        from c in dbContext.Categories.Where(c => a.CategoryId == c.Id).DefaultIfEmpty().Select(c => c.Title)
                        select new AlbumDetailViewModel
                        {
                            Id = a.Id,
                            Title = a.Title,
                            CategoryName = c,
                            CreateTime = a.CreateTime
                        };
            var result = query.ToList();

            //            var result11 = await dbContext.Albums.GroupJoin(dbContext.Categories, a =>
            //a.CategoryId, b => b.Id, (a, b) => new { a, b }).SelectMany(item => item.b.DefaultIfEmpty(), (a, b)
            //=> new AlbumDetailViewModel
            //{
            //    Id = a.a.Id,
            //    Title = a.a.Title,
            //    CategoryName = b.Title,
            //    CreateTime = a.a.CreateTime,
            //}).ToListAsync();

            //var query = (from a in dbContext.Albums
            //             join c in dbContext.Categories on a.CategoryId equals c.Id
            //             select new AlbumDetailViewModel
            //             {
            //                 Id = a.Id,
            //                 Title = a.Title,
            //                 //CoverUrl = a.CoverUrl?.,
            //                 CategoryName = c.Title,
            //                 CreateTime = a.CreateTime,
            //             }).ToList();

            return ApiResultBuilder<List<AlbumDetailViewModel>>.Success(result);
        }
    }
}