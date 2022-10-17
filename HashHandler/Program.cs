using HashHandler.HashProvider;
using HashHandler.Services;
using HashHandler.Services.Interfaces;
using HashHandler.Shared.Configuration;
using HashHandler.Shared.Extensions;
using HashHandler.Shared.Repositories;
using HashHandler.Shared.Repositories.Interfaces;

const string defaultDbConnection = "Default";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

// Register dependencies
builder.Services.AddSwaggerGen();
builder.Services.AddDatabase(builder.Configuration, defaultDbConnection);

builder.Services.AddScoped<IHashesRepository, HashesRepository>();
builder.Services.AddScoped<IHashProvider, HashProvider>();
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection(RabbitMqOptions.RabbitMqSection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllerRoute(
    name: "hash",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();