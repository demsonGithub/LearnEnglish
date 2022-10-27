using Demkin.Search.Domain.AggregateModels;

namespace Demkin.Search.Domain
{
    public class SearchEpisodeResponse
    {
        public IEnumerable<Episode> Episodes { get; set; }

        public long TotalCount { get; set; }
    }
}