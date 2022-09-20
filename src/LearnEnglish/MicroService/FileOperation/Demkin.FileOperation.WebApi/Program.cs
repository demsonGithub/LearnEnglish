using Demkin.FileOperation.Domain.Interfaces;
using Demkin.FileOperation.Infrastructure.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatRService();
builder.Services.AddDbSetup(builder.Configuration.GetValue<string>("ConnectionStrings:sqlserver"));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IStorageFile, CloundStorageFile>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();