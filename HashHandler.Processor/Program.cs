using HashHandler.Shared.Configuration;
using HashHandler.Shared.Extensions;
using HashHandler.Shared.Repositories;
using HashHandler.Shared.Repositories.Interfaces;
using HashProcessor.Consumers;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<HashCreatedConsumer>();
        services.AddDatabase(context.Configuration, "Default");
        services.AddScoped<IHashesRepository, HashesRepository>();
        services.Configure<RabbitMqOptions>(context.Configuration.GetSection(RabbitMqOptions.RabbitMqSection));
    })
    .Build();

await host.RunAsync();