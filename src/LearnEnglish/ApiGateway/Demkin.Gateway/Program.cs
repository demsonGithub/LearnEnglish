using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

var configuration = builder.Configuration;

builder.Services.AddOcelot(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseOcelot().Wait();

app.Run();