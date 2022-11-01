using Demkin.Listen.WebApi.Admin.Extensions;
using Demkin.Listen.WebApi.Admin.Hubs;
using Microsoft.EntityFrameworkCore;

string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
SerilogHelper.LogInitialize(logFilePath);

try
{
    Log.Information("Host Starting");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers().AddNewtonsoftJson();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.InitConfigureDefaultServices();

    builder.Services.AddSignalR();

    // Ìí¼ÓCapÊÂ¼þ¶©ÔÄ
    builder.Services.AddSubscribeEvent();

    builder.Services.AddDbContext<ListenDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetSection("DbConnection:MasterDb_Listen").Value);
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Listen Service");
            c.RoutePrefix = "api";
        });
    }

    app.InitUseDefaultMiddleware();

    app.MapControllers();
    app.MapHub<UploadFileHub>("/Hubs/UploadFileHub");
    app.MapHub<TranscodeFileHub>("/Hubs/TranscodeFileHub");

    var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    var lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
    app.UseConsulMiddleware(builder.Configuration, lifetime);

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