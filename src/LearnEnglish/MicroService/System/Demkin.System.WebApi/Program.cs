using Demkin.System.Domain.AggregatesModel.UserAggregate;
using Demkin.System.Infrastructure;
using Demkin.System.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var Configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(Program).Assembly, typeof(User).Assembly);
builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<SystemDbContext>();

builder.Services.AddDbContext<SystemDbContext>(options =>
{
    options.UseSqlServer(Configuration.GetValue<string>("ConnectionStrings:sqlserver1"), x => x.CommandTimeout(20));
});

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