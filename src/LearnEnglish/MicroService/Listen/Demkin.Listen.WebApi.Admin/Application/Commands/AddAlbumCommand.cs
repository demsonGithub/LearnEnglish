namespace Demkin.Listen.WebApi.Admin.Application.Commands
{
    public class AddAlbumCommand : IRequest<AlbumDetailDto>
    {
        public string Title { get; set; }

        public string? CoverUrl { get; set; }

        public int SequenceNumber { get; set; }

        public long CategoryId { get; set; }
    }

    public class AddAlbumCommandHandler : IRequestHandler<AddAlbumCommand, AlbumDetailDto>
    {
        private readonly DomainService _domainService;
        private readonly IMapper _mapper;
        private readonly IAlbumRepository _albumRepository;

        public AddAlbumCommandHandler(DomainService domainService, IMapper mapper, IAlbumRepository albumRepository)
        {
            _domainService = domainService;
            _mapper = mapper;
            _albumRepository = albumRepository;
        }

        public async Task<AlbumDetailDto> Handle(AddAlbumCommand request, CancellationToken cancellationToken)
        {
            var album = await _domainService.AddAlbum(request.Title, request.CoverUrl, request.SequenceNumber, request.CategoryId);

            await _albumRepository.AddAsync(album, cancellationToken);
            var isSaved = await _albumRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (isSaved)
            {
                var result = _mapper.Map<AlbumDetailDto>(album);
                return result;
            }
            else
            {
                throw new DomainException("未保存");
            }
        }
    }
}