using Demkin.Core.Extensions;
using Demkin.FileOperation.WebApi.Hubs;
using Serilog;

string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
SerilogHelper.LogInitialize(logFilePath);

try
{
    Log.Information("Starting web host");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers().AddNewtonsoftJson();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.InitConfigureDefaultServices();
    builder.Services.AddDbSetup(builder.Configuration.GetValue<string>("ConnectionStrings:sqlserver"));

    builder.Services.AddSignalR();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/v1/swagger.json", "File Service");
            c.RoutePrefix = "api";
        });
    }

    app.InitUseDefaultMiddleware();

    app.MapControllers();
    app.MapHub<FileUploadStatusHub>("/Hubs/FileUploadStatusHub");

    app.Run();
}
catch (Exception ex)
{
    if (ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}