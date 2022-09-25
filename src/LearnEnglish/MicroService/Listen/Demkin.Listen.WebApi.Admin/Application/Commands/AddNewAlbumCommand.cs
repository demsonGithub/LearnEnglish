namespace Demkin.Listen.WebApi.Admin.Application.Commands
{
    public class AddNewAlbumCommand : IRequest<AlbumDetailViewModel>
    {
        public string Title { get; set; }

        public string? Url { get; set; }

        public int SequenceNumber { get; set; }

        public long CategoryId { get; set; }
    }

    public class AddNewAlbumCommandHandler : IRequestHandler<AddNewAlbumCommand, AlbumDetailViewModel>
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

        public async Task<AlbumDetailViewModel> Handle(AddNewAlbumCommand request, CancellationToken cancellationToken)
        {
            var album = await _domainService.AddNewAlbum(request.Title, request.Url, request.SequenceNumber, request.CategoryId);

            await _albumRepository.AddAsync(album, cancellationToken);
            var isSaved = await _albumRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (isSaved)
            {
                var result = _mapper.Map<AlbumDetailViewModel>(album);
                return result;
            }
            else
            {
                throw new DomainException("未保存");
            }
        }
    }
}