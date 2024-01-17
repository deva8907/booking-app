using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieBooking.Core;

class DatabaseSeeder(IMongoClient mongoClient, IOptions<MongoDbSettings> mongoDbSettings) : BackgroundService
{
    private readonly IMongoDatabase _database = mongoClient.GetDatabase(mongoDbSettings.Value.Database);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await SeedCities();
    }

    private async Task SeedCities()
    {
        List<City> cities =
        [
            new() { Name = "Chennai" },
            new() { Name = "Mumbai" },
            new () { Name = "Bangalore"}
        ];
        var cityCollection = _database.GetCollection<City>("cities");
        var existingCities = cityCollection.Find(Builders<City>.Filter.Empty).ToList();
        foreach (var city in cities)
        {
            if (!existingCities.Any(x => x.Name == city.Name))
            {
                await cityCollection.InsertOneAsync(city);
            }
        }
    }
}