using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MovieBooking.Core;

public static class Configure
{
    public static void ConfigureCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDB"));
        services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            var mongoDbSettings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(mongoDbSettings.ConnectionString);
        });
        services.AddHostedService<DatabaseSeeder>();
    }
}
