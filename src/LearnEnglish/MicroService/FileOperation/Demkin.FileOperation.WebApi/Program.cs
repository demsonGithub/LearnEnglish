using Demkin.Core.Extensions;
using Demkin.FileOperation.WebApi.GrpcServices;
using Microsoft.EntityFrameworkCore;
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

    builder.Services.AddGrpc();

    builder.InitConfigureDefaultServices();
    builder.Services.AddDbContext<FileDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetSection("DbConnection:MasterDb_File").Value);
    });

    builder.Services.AddMemoryCache();

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

    app.MapGrpcService<UploadFileGrpcServiceImpl>();
    app.MapControllers();
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
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}