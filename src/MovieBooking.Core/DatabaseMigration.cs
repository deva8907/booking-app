using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MovieBooking.Core;

class DatabaseMigration(IMongoDatabase database) : BackgroundService
{
    private readonly IMongoDatabase _database = database;

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
        var movieCollection = _database.GetCollection<Movie>(DBCollections.MOVIES);
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
        var cinemaCollection = _database.GetCollection<Cinema>(DBCollections.CINEMAS);
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
            new() { Name = "The Shawshank Redemption", RuntimeMinutes = 142, PlotSummary = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", MovieId = "SHAWSHANK-REDEMPTION" },
            new() { Name = "The Godfather", RuntimeMinutes = 175, PlotSummary = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", MovieId = "GODFATHER-I" },
            new() { Name = "The Dark Knight", RuntimeMinutes = 152, PlotSummary = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.", MovieId = "DARK-KNIGHT" },
            new() { Name = "The Godfather: Part II", RuntimeMinutes = 202, PlotSummary = "The early life and career of Vito Corleone in 1920s New York City is portrayed, while his son, Michael, expands and tightens his grip on the family crime syndicate.", MovieId = "GODFATHER-II" },
        ];
        var movieCollection = _database.GetCollection<Movie>(DBCollections.MOVIES);
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
            new() { Name = "PVR", City = "Chennai", CinemaId = "PVR-CHE",
                Screens =
                [
                    new() { Name = "Screen 1", TotalSeats = 100, ScreenId = "PVR-CHE-SCREEN-I" },
                    new() { Name = "Screen 2", TotalSeats = 100, ScreenId = "PVR-CHE-SCREEN-II" },
                    new() { Name = "Screen 3", TotalSeats = 100, ScreenId = "PVR-CHE-SCREEN-III" },
                ] },
            new() { Name = "PVR", City = "Mumbai", CinemaId = "PVR-MUM",
                Screens =
                [
                    new() { Name = "Screen 1", TotalSeats = 100, ScreenId = "PVR-MUM-SCREEN-I" },
                    new() { Name = "Screen 2", TotalSeats = 100, ScreenId = "PVR-MUM-SCREEN-II" },
                    new() { Name = "Screen 3", TotalSeats = 100, ScreenId = "PVR-MUM-SCREEN-III" },
                ] },
            new() { Name = "PVR", City = "Bangalore", CinemaId = "PVR-BLR",
                Screens =
                [
                    new() { Name = "Screen 1", TotalSeats = 100, ScreenId = "PVR-BLR-SCREEN-I" },
                    new() { Name = "Screen 2", TotalSeats = 100, ScreenId = "PVR-BLR-SCREEN-II" },
                    new() { Name = "Screen 3", TotalSeats = 100, ScreenId = "PVR-BLR-SCREEN-III" },
                ] }
        ];
        var cinemaCollection = _database.GetCollection<Cinema>(DBCollections.CINEMAS);
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
            new() { Name = "Chennai", CityId = "CHN" },
            new() { Name = "Mumbai", CityId = "MUM" },
            new () { Name = "Bangalore", CityId = "BLR"}
        ];
        var cityCollection = _database.GetCollection<City>(DBCollections.CITIES);
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