
using MongoDB.Driver;

namespace MovieBooking.Core;

public class CoreRepository(IMongoDatabase database) : ICoreRepository
{
    private readonly IMongoCollection<City> _cities = database.GetCollection<City>(DBCollections.CITIES);
    private readonly IMongoCollection<Cinema> _cinemas = database.GetCollection<Cinema>(DBCollections.CINEMAS);
    private readonly IMongoCollection<Movie> _movies = database.GetCollection<Movie>(DBCollections.MOVIES);

    public async Task<IEnumerable<Cinema>> GetAllCinemas()
    {
        return await _cinemas.Find(Builders<Cinema>.Filter.Empty).ToListAsync();
    }

    public async Task<IEnumerable<City>> GetAllCities()
    {
        return await _cities.Find(Builders<City>.Filter.Empty).ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetAllMovies()
    {
        return await _movies.Find(Builders<Movie>.Filter.Empty).ToListAsync();
    }
}
