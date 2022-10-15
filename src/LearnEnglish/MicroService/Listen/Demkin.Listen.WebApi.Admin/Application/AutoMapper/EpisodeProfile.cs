namespace Demkin.Listen.WebApi.Admin.MapperConfig
{
    public class EpisodeProfile : Profile
    {
        public EpisodeProfile()
        {
            CreateMap<Episode, EpisodeDto>();
        }
    }
}