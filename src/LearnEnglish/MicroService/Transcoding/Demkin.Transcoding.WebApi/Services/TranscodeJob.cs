namespace Demkin.Transcoding.WebApi.Services
{
    public class TranscodeJob : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TranscodeJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // 1. 从redis中获取需要转码的任务

                // 2.
                Console.WriteLine(DateTime.Now);

                await Task.Delay(TimeSpan.FromSeconds(3));
            }
        }
    }
}