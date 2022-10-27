using Demkin.Search.Domain;
using Demkin.Search.Domain.Interfaces;
using MediatR;

namespace Demkin.Search.WebApi.Application.Queries
{
    public class SearchEpisodesQuery : IRequest<SearchEpisodeResponse>
    {
        public string Keyword { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }

    public class SearchEpisodesQueryHandler : IRequestHandler<SearchEpisodesQuery, SearchEpisodeResponse>
    {
        private readonly ISearchRepository _repository;

        public SearchEpisodesQueryHandler(ISearchRepository repository)
        {
            _repository = repository;
        }

        public async Task<SearchEpisodeResponse> Handle(SearchEpisodesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.SearchEpisodes(request.Keyword, request.PageIndex, request.PageSize);

            return result;
        }
    }
}