string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
SerilogHelper.LogInitialize(logFilePath);

try
{
    Log.Information("Host Starting");

    var builder = WebApplication.CreateBuilder(args);
    IConfiguration configuration = builder.Configuration;

    // Add services to the container.
    builder.Services.AddControllers().AddNewtonsoftJson();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.InitConfigureDefaultServices();
    builder.Services.AddDbSetup(configuration.GetSection("ConnectionStrings:sqlserver").Value);

    var app = builder.Build();

    var scope = app.Services.CreateScope();
    SeedData.Initialize(scope.ServiceProvider);

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/v1/swagger.json", "System Service");
            c.RoutePrefix = "api";
        });
    }

    app.InitUseDefaultMiddleware();

    app.MapControllers();

    app.Run();
    //when(ex.GetType().Name == "StopTheHostException")
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
    Log.Fatal(ex, "Host Build Error");
}
finally
{
    Log.CloseAndFlush();
}