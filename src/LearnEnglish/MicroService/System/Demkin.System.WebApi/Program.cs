using Demkin.System.WebApi.Extensions;

string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
SerilogHelper.LogInitialize(logFilePath);

try
{
    Log.Information("Host Starting");

    var builder = WebApplication.CreateBuilder(args);

    IConfiguration configuration = builder.Configuration;

    #region Add services to the container.

    builder.Services.AddControllers().AddNewtonsoftJson();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.InitConfigureDefaultServices();

    builder.Services.AddDbSetup(builder.Configuration.GetSection("DbConnection:MasterDb_System").Value);

    #endregion Add services to the container.

    var app = builder.Build();

    #region Configure the HTTP request pipeline.

    var scope = app.Services.CreateScope();
    SeedData.Initialize(scope.ServiceProvider);

    //if (app.Environment.IsDevelopment())
    //{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/v1/swagger.json", "System Service");
        c.RoutePrefix = "api";
    });
    //}

    app.InitUseDefaultMiddleware();

    app.MapControllers();

    var lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();

    app.UseConsulMiddleware(builder.Configuration, lifetime);

    #endregion Configure the HTTP request pipeline.

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host Build Error");

    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
}
finally
{
    Log.CloseAndFlush();
}