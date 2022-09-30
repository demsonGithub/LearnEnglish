namespace Demkin.Listen.WebApi.Admin.Application.Commands
{
    public class AddNewAlbumCommand : IRequest<AlbumDetailDto>
    {
        public string Title { get; set; }

        public string? CoverUrl { get; set; }

        public int SequenceNumber { get; set; }

        public long CategoryId { get; set; }
    }

    public class AddNewAlbumCommandHandler : IRequestHandler<AddNewAlbumCommand, AlbumDetailDto>
    {
        private readonly ListenDomainService _domainService;
        private readonly IMapper _mapper;
        private readonly IAlbumRepository _albumRepository;

        public AddNewAlbumCommandHandler(ListenDomainService domainService, IMapper mapper, IAlbumRepository albumRepository)
        {
            _domainService = domainService;
            _mapper = mapper;
            _albumRepository = albumRepository;
        }

        public async Task<AlbumDetailDto> Handle(AddNewAlbumCommand request, CancellationToken cancellationToken)
        {
            var album = await _domainService.AddNewAlbum(request.Title, request.CoverUrl, request.SequenceNumber, request.CategoryId);

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