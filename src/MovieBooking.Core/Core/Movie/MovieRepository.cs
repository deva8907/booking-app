
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MovieBooking.Core;

public class MovieRepository : IMovieRepository
{
    private readonly IMongoCollection<Movie> _movies;

    public MovieRepository(IMongoClient client, IOptions<MongoDbSettings> options)
    {
        var database = client.GetDatabase(options.Value.Database);
        _movies = database.GetCollection<Movie>(DBCollections.MOVIES);
    }

    public async Task<Movie> GetMovieById(string movieId)
    {
        var filter = Builders<Movie>.Filter.Eq(movie => movie.MovieId, movieId);
        return await _movies.Find(filter).FirstOrDefaultAsync();
    }
}
