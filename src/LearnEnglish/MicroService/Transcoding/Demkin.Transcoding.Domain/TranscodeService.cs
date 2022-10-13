namespace Demkin.Transcoding.Domain
{
    public class TranscodeService
    {
        public TranscodeService()
        {
        }

        public Task<string> TranscodeFileToM4a()
        {
            string destUrl = "";
            Thread.Sleep(TimeSpan.FromSeconds(10));

            return Task.FromResult(destUrl);
        }
    }
}