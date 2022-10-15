using AutoMapper;

namespace Demkin.Listen.WebApi.Admin.MapperConfig
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            CreateMap<Album, AlbumDetailDto>();

            CreateMap<AlbumDetailDto, Album>();
        }
    }
}