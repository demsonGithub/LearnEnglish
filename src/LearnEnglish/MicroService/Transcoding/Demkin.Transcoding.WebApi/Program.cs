using Demkin.Core.Extensions;
using Demkin.Transcoding.Domain;
using Demkin.Transcoding.WebApi.Extensions;
using Demkin.Transcoding.WebApi.IntegrationEvents;
using Demkin.Transcoding.WebApi.Services;
using Demkin.Utils;
using Serilog;

string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
SerilogHelper.LogInitialize(logFilePath);

try
{
    Log.Information("Host Starting");
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddHostedService<TranscodeBgService>();
    builder.Services.AddHttpClient();

    builder.InitConfigureDefaultServices();
    builder.Services.AddDbSetup(builder.Configuration.GetSection("DbConnection:MasterDb_Transcode").Value);
    builder.Services.AddEventBusSetup();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Transcode Service");
            c.RoutePrefix = "api";
        });
    }

    app.InitUseDefaultMiddleware();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    if (ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
    Log.Fatal(ex, "Host Build Error");
}
finally
{
    Log.CloseAndFlush();
}