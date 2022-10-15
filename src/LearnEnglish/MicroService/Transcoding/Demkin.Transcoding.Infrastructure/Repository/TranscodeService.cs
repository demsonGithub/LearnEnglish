using Demkin.Transcoding.Domain.Interfaces;
using FFmpeg.NET;

namespace Demkin.Transcoding.Infrastructure.Repository
{
    public class FFMpegTranscodeService : ITranscodeService
    {
        public async Task TranscodeFileToTarget(string sourceUrl, string targetUrl, CancellationToken ct = default)
        {
            var inputFile = new InputFile(sourceUrl);
            var outputFile = new OutputFile(targetUrl);

            string baseDir = AppContext.BaseDirectory;
            string ffmpegPath = Path.Combine(baseDir, "FFmpeg", "ffmpeg.exe");
            var ffmpeg = new Engine(ffmpegPath);

            string? errorMsg = null;
            ffmpeg.Error += (sender, e) =>
            {
                errorMsg += e.Exception.Message;
            };

            await ffmpeg.ConvertAsync(inputFile, outputFile, ct);

            if (!string.IsNullOrEmpty(errorMsg))
            {
                throw new Exception(errorMsg);
            }
        }
    }
}