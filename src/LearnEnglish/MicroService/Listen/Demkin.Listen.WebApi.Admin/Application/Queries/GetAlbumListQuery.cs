using AutoMapper;

namespace Demkin.Listen.WebApi.Admin.Application.Queries
{
    public class GetAlbumListQuery : IRequest<ApiResult<List<AlbumDetailViewModel>>>
    {
        public long CategoryId { get; set; }

        public string? Title { get; set; }
    }

    public class GetAlbumListQueryHandler : IRequestHandler<GetAlbumListQuery, ApiResult<List<AlbumDetailViewModel>>>
    {
        private readonly ListenDomainService _domainService;
        private readonly IMapper _mapper;

        public GetAlbumListQueryHandler(ListenDomainService domainService, IMapper mapper)
        {
            _domainService = domainService;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<AlbumDetailViewModel>>> Handle(GetAlbumListQuery request, CancellationToken cancellationToken)
        {
            var albums = await _domainService.GetAlbumList(request.CategoryId, request.Title);

            var result = _mapper.Map<List<AlbumDetailViewModel>>(albums);

            return ApiResultBuilder<List<AlbumDetailViewModel>>.Success(result);
        }
    }
}