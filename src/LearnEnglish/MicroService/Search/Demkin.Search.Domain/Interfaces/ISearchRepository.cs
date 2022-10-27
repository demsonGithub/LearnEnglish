using Demkin.Domain.Abstraction;
using Demkin.Search.Domain.AggregateModels;

namespace Demkin.Search.Domain.Interfaces
{
    public interface ISearchRepository : IDenpendencyScope
    {
        Task UpdateAsync(Episode episode);

        Task DeleteAsync(string episodeId);

        Task<SearchEpisodeResponse> SearchEpisodes(string keyword, int pageIndex, int pageSize);
    }
}