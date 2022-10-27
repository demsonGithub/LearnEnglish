using Demkin.Core.Exceptions;
using Demkin.Search.Domain;
using Demkin.Search.Domain.AggregateModels;
using Demkin.Search.Domain.Interfaces;
using Nest;

namespace Demkin.Search.Infrastructure
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IElasticClient _elasticClient;

        public SearchRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task UpdateAsync(Episode episode)
        {
            var response = await _elasticClient.IndexAsync(episode, idx => idx.Index("episodes").Id(episode.Id));
            if (!response.IsValid)
            {
                throw new DomainException(response.DebugInformation);
            }
        }

        public Task DeleteAsync(long episodeId)
        {
            _elasticClient.DeleteByQuery<Episode>(iq => iq.Index("episodes").Query(rq => rq.Term(f => f.Id, "elasticsearch.pm")));

            //如果Episode被删除，则把对应的数据也从Elastic Search中删除
            return _elasticClient.DeleteAsync(new DeleteRequest("episodes", episodeId));
        }

        public async Task<SearchEpisodeResponse> SearchEpisodes(string keyword, int pageIndex, int pageSize)
        {
            int from = pageSize * (pageIndex - 1);
            string kw = keyword;
            Func<QueryContainerDescriptor<Episode>, QueryContainer> query = (q) =>
                          q.Match(mq => mq.Field(f => f.Title).Query(kw))
                          || q.Match(mq => mq.Field(f => f.Subtitles).Query(kw));
            Func<HighlightDescriptor<Episode>, IHighlight> highlightSelector = h => h
                .Fields(fs => fs.Field(f => f.Subtitles));
            var result = await _elasticClient.SearchAsync<Episode>(s => s.Index("episodes").From(from)
                .Size(pageSize).Query(query).Highlight(highlightSelector));
            if (!result.IsValid)
            {
                throw result.OriginalException;
            }
            List<Episode> episodes = new List<Episode>();
            foreach (var hit in result.Hits)
            {
                string highlightedSubtitle;
                //如果没有预览内容，则显示前50个字
                if (hit.Highlight.ContainsKey("subtitles"))
                {
                    highlightedSubtitle = string.Join("\r\n", hit.Highlight["subtitles"]);
                }
                else
                {
                    highlightedSubtitle = Cut(hit.Source.Subtitles, 50);
                }
                var episode = Episode.Create(hit.Source.EpisodeId, hit.Source.Title, hit.Source.Description, highlightedSubtitle, hit.Source.AlbumId);

                episodes.Add(episode);
            }
            return new SearchEpisodeResponse()
            {
                Episodes = episodes,
                TotalCount = result.Total
            };
        }

        public string Cut(string s1, int maxLen)
        {
            if (s1 == null)
            {
                return string.Empty;
            }
            int len = s1.Length <= maxLen ? s1.Length : maxLen;//不能超过字符串的最大大小
            return s1[0..len];
        }
    }
}