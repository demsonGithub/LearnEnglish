using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

try
{
    Log.Information("Starting web host");

    var builder = WebApplication.CreateBuilder(args);

    builder.ConfigureInitService();

    // Add services to the container.
    builder.Services.AddControllers().AddNewtonsoftJson();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbSetup(Configuration.GetValue<string>("ConnectionStrings:sqlserver"));

    builder.Services.AddCorsSetup();

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
    app.UseCors("LearnEnglish");

    app.UseAuthorization();

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
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}