using Microsoft.EntityFrameworkCore;
using HashProcessor.Context;
using HashProcessor.Consumers;



namespace HashProcessor.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRabbitMqBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<HashCreatedConsumer>();
            return services;
        }
        
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection));
            return services;
        }
    }
}