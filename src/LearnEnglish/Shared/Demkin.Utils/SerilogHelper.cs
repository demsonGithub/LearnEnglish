using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Demkin.Utils
{
    public class SerilogHelper
    {
        public static void LogInitialize(string logFilePath)
        {
            //日志的输出模板
            string SerilogOutputTemplate = "Time:{Timestamp:yyyy-MM-dd HH:mm:ss.fff}  Level：[{Level}]{NewLine}" +
                "Message: {Message}{NewLine}" +
               "{Exception}{NewLine}" +
                new string('-', 70) + "{NewLine}";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .MinimumLevel.Override("Default", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .WriteTo.Console(outputTemplate: SerilogOutputTemplate)
                .WriteTo.Async(o =>
                {
                    //输出到文件,需要提供输出路径和周期
                    o.File(logFilePath + "/log_.log",
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: SerilogOutputTemplate,
                        rollOnFileSizeLimit: true,
                        fileSizeLimitBytes: 102400000,
                        retainedFileCountLimit: 365
                        );
                })
                .CreateLogger();
        }

        /// <summary>
        /// 初始化Serilog，从appsettings.json读取配置
        /// </summary>
        public static void LogInitializeByConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfiguration Configuration = new ConfigurationBuilder()
                                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                                            .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .CreateLogger();
        }
    }
}