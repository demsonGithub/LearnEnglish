namespace Demkin.Listen.WebApi.Admin.Application.Commands
{
    public class UpdateAlbumCommand : IRequest<string>
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string? CoverUrl { get; set; }

        public int SequenceNumber { get; set; }
    }

    public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, string>
    {
        private readonly IAlbumRepository _albumRepository;

        public UpdateAlbumCommandHandler(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<string> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
        {
            var albumEntity = await _albumRepository.FindAsync(request.Id);

            var targetAlbum = albumEntity.ChangeTitle(request.Title)
                .ChangeCoverUrl(request.CoverUrl)
                .ChangeSequenceNumber(request.SequenceNumber);

            await _albumRepository.UpdateAsync(targetAlbum);
            var result = await _albumRepository.UnitOfWork.SaveEntitiesAsync();

            if (!result)
                throw new DomainException("修改失败");

            return targetAlbum.Id.ToString();
        }
    }
}