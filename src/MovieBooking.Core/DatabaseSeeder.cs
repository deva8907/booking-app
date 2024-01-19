using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieBooking.Core;

class DatabaseSeeder(IMongoClient mongoClient, IOptions<MongoDbSettings> mongoDbSettings) : BackgroundService
{
    private readonly IMongoDatabase _database = mongoClient.GetDatabase(mongoDbSettings.Value.Database);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        RunMigration();
        await SeedCities();
        await SeedCinemas();
        await SeedMovies();
    }

    private void RunMigration()
    {
        CreateUniqueConstraintForMovies();
        CreateUniqueConstraintForCinemas();
    }

    private void CreateUniqueConstraintForMovies()
    {
        var movieCollection = _database.GetCollection<Movie>("movies");
        var indexes = movieCollection.Indexes.List().ToList();
        var uniqueNameIndexExists = indexes.Any(index => index["key"].AsBsonDocument.Contains("MovieId") && index["unique"].AsBoolean);
        if (!uniqueNameIndexExists)
        {
            var indexKeysDefinition = Builders<Movie>.IndexKeys.Ascending(movie => movie.MovieId);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<Movie>(indexKeysDefinition, indexOptions);
            movieCollection.Indexes.CreateOne(indexModel);
        }
    }
    private void CreateUniqueConstraintForCinemas()
    {
        var cinemaCollection = _database.GetCollection<Cinema>("cinemas");
        var indexes = cinemaCollection.Indexes.List().ToList();
        var uniqueNameIndexExists = indexes.Any(index => index["key"].AsBsonDocument.Contains("CinemaId") && index["unique"].AsBoolean);
        if (!uniqueNameIndexExists)
        {
            var indexKeysDefinition = Builders<Cinema>.IndexKeys.Ascending(cinema => cinema.CinemaId);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<Cinema>(indexKeysDefinition, indexOptions);
            cinemaCollection.Indexes.CreateOne(indexModel);
        }
    }

    private async Task SeedMovies()
    {
        List<Movie> movies =
        [
            new() { Name = "The Shawshank Redemption", RuntimeMinutes = 142, PlotSummary = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", MovieId = Guid.NewGuid().ToString() },
            new() { Name = "The Godfather", RuntimeMinutes = 175, PlotSummary = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", MovieId = Guid.NewGuid().ToString() },
            new() { Name = "The Dark Knight", RuntimeMinutes = 152, PlotSummary = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.", MovieId = Guid.NewGuid().ToString() },
            new() { Name = "The Godfather: Part II", RuntimeMinutes = 202, PlotSummary = "The early life and career of Vito Corleone in 1920s New York City is portrayed, while his son, Michael, expands and tightens his grip on the family crime syndicate.", MovieId = Guid.NewGuid().ToString() },
        ];
        var movieCollection = _database.GetCollection<Movie>("movies");
        var existingMovies = movieCollection.Find(Builders<Movie>.Filter.Empty).ToList();
        foreach (var movie in movies)
        {
            if (!existingMovies.Any(x => x.Name == movie.Name))
            {
                await movieCollection.InsertOneAsync(movie);
            }
        }
    }

    private async Task SeedCinemas()
    {
        List<Cinema> cinemas =
        [
            new() { Name = "PVR", City = "Chennai", CinemaId = Guid.NewGuid().ToString(),
                Screens =
                [
                    new() { Name = "Screen 1", TotalSeats = 100, ScreenId = Guid.NewGuid().ToString() },
                    new() { Name = "Screen 2", TotalSeats = 100, ScreenId = Guid.NewGuid().ToString() },
                    new() { Name = "Screen 3", TotalSeats = 100, ScreenId = Guid.NewGuid().ToString() },
                ] },
            new() { Name = "PVR", City = "Mumbai", CinemaId = Guid.NewGuid().ToString(),
                Screens =
                [
                    new() { Name = "Screen 1", TotalSeats = 100, ScreenId = Guid.NewGuid().ToString() },
                    new() { Name = "Screen 2", TotalSeats = 100, ScreenId = Guid.NewGuid().ToString() },
                    new() { Name = "Screen 3", TotalSeats = 100, ScreenId = Guid.NewGuid().ToString() },
                ] },
            new() { Name = "PVR", City = "Bangalore", CinemaId = Guid.NewGuid().ToString(),
                Screens =
                [
                    new() { Name = "Screen 1", TotalSeats = 100, ScreenId = Guid.NewGuid().ToString() },
                    new() { Name = "Screen 2", TotalSeats = 100, ScreenId = Guid.NewGuid().ToString() },
                    new() { Name = "Screen 3", TotalSeats = 100, ScreenId = Guid.NewGuid().ToString() },
                ] }
        ];
        var cinemaCollection = _database.GetCollection<Cinema>("cinemas");
        var existingCinemas = cinemaCollection.Find(Builders<Cinema>.Filter.Empty).ToList();
        foreach (var cinema in cinemas)
        {
            if (!existingCinemas.Any(x => x.Name == cinema.Name && x.City == cinema.City))
            {
                await cinemaCollection.InsertOneAsync(cinema);
            }
        }
    }

    private async Task SeedCities()
    {
        List<City> cities =
        [
            new() { Name = "Chennai", CityId = Guid.NewGuid().ToString() },
            new() { Name = "Mumbai", CityId = Guid.NewGuid().ToString() },
            new () { Name = "Bangalore", CityId = Guid.NewGuid().ToString()}
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