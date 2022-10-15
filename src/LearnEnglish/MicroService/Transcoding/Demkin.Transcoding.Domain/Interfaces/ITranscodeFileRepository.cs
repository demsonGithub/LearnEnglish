namespace Demkin.Transcoding.Domain.Interfaces
{
    public interface ITranscodeFileRepository : IRepository<TranscodeFile, long>, IDenpendencyScope
    {
    }
}