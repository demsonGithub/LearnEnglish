using Demkin.Search.Domain.AggregateModels;
using Demkin.Search.Domain.Interfaces;
using Demkin.Search.WebApi.Application.Models;
using DotNetCore.CAP;
using Newtonsoft.Json;

namespace Demkin.Search.WebApi.Application.IntegrationEvents
{
    public class EpsiodeUpdateIntegrationEvent : ICapSubscribe
    {
        private readonly ISearchRepository _repository;

        public EpsiodeUpdateIntegrationEvent(ISearchRepository repository)
        {
            _repository = repository;
        }

        [CapSubscribe("EpisodeCreated")]
        public async Task EpisodeCreatedHandle(object obj)
        {
            EpisodeUpdateParams item = JsonConvert.DeserializeObject<EpisodeUpdateParams>(Convert.ToString(obj));

            Episode entity = new Episode
            {
                EpisodeId = item.EpisodeId.ToString(),
                Title = item.Title,
                Description = item.Description,
                Subtitles = item.Subtitles,
                AlbumId = item.AlbumId.ToString(),
            };

            await _repository.UpdateAsync(entity);
        }
    }
}