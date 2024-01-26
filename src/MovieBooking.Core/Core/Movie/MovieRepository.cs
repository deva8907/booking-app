
using MongoDB.Driver;

namespace MovieBooking.Core;

public class MovieRepository(IMongoDatabase database) : IMovieRepository
{
    private readonly IMongoCollection<Movie> _movies = database.GetCollection<Movie>(DBCollections.MOVIES);

    public async Task<Movie> GetMovieById(string movieId)
    {
        var filter = Builders<Movie>.Filter.Eq(movie => movie.MovieId, movieId);
        return await _movies.Find(filter).FirstOrDefaultAsync();
    }
}
