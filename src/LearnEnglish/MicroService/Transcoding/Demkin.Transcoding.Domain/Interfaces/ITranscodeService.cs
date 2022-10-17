namespace Demkin.Transcoding.Domain.Interfaces
{
    public interface ITranscodeService : IDenpendencyScope
    {
        Task TranscodeFileToTarget(string sourceUrl, string targetUrl, CancellationToken ct = default);

        Task<string> UploadFile(string uploadApiUrl, string filePath);
    }
}