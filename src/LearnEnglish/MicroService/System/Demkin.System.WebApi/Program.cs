using Demkin.Core.Jwt;
using Demkin.System.Infrastructure;
using Demkin.System.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtOptions>(options =>
{
    options.SecretKey = Configuration["JwtOptions:SecretKey"];
    options.Issuer = Configuration["JwtOptions:Issuer"];
    options.Audience = Configuration["JwtOptions:Audience"];
    options.ExpireSeconds = Convert.ToInt32(Configuration["JwtOptions:ExpireSeconds"]);
});

builder.Services.AddMediatRService();

builder.Services.AddDataBaseDomainContext(Configuration.GetValue<string>("ConnectionStrings:sqlserver"));

builder.Services.AddRepositoriesDI();

var app = builder.Build();

var scope = app.Services.CreateScope();
SeedData.Initialize(scope.ServiceProvider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();