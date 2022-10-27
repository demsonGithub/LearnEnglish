using Demkin.Search.Domain.Interfaces;
using MediatR;

namespace Demkin.Search.WebApi.Application.Commands
{
    public class DeleteEpisodeCommand : MediatR.IRequest<string>
    {
        public string EpisodeId { get; set; }
    }

    public class DeleteEpisodeCommandHandler : IRequestHandler<DeleteEpisodeCommand, string>
    {
        private readonly ISearchRepository _repository;

        public DeleteEpisodeCommandHandler(ISearchRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(DeleteEpisodeCommand request, CancellationToken cancellationToken)
        {
            string episodeId = request.EpisodeId;

            await _repository.DeleteAsync(episodeId);

            return episodeId;
        }
    }
}