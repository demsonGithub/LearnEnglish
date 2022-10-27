using Demkin.Core.Extensions;
using Nest;

using Demkin.Utils;
using Serilog;
using Demkin.Search.WebApi.Extensions;

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
    builder.InitConfigureDefaultServices();
    builder.Services.AddScoped<IElasticClient>(op =>
    {
        Uri url = new Uri(builder.Configuration.GetValue<string>("ElasticSearchConnection"));
        var settings = new ConnectionSettings(url);
        return new ElasticClient();
    });
    builder.Services.AddIntegrationEvent();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

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