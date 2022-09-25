namespace Demkin.Listen.Domain.Interfaces
{
    public interface IAlbumRepository : IRepository<Album, long>, IDenpendencyScope
    {
    }
}