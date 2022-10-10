namespace Demkin.Listen.Domain.Interfaces
{
    public interface IEpisodeRepository : IRepository<Episode, long>, IDenpendencyScope
    {
    }
}