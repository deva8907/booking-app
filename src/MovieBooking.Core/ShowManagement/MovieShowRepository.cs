using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MovieBooking.Core
{
    public class MovieShowRepository
    {
        private readonly IMongoCollection<MovieShow> _movieShows;

        public MovieShowRepository(IMongoClient client, IOptions<MongoDbSettings> options)
        {
            var database = client.GetDatabase(options.Value.Database);
            _movieShows = database.GetCollection<MovieShow>(DBCollections.MOVIE_SHOWS);
        }

        public async Task<IEnumerable<MovieShowDto>> GetMovieShowsByCinemaId(string cinemaId)
        {
            var pipeline = new BsonDocument[]
            {
                new("$match", new BsonDocument("CinemaId", cinemaId)),
                new("$lookup", new BsonDocument
                {
                    { "from", DBCollections.CINEMAS },
                    { "localField", "CinemaId" },
                    { "foreignField", "CinemaId" },
                    { "as", "Cinema" }
                }),
                new("$unwind", "$Cinema"),
                new("$lookup", new BsonDocument
                {
                    { "from", DBCollections.MOVIES },
                    { "localField", "MovieId" },
                    { "foreignField", "MovieId" },
                    { "as", "Movie" }
                }),
                new("$unwind", "$Movie"),
                new("$project", new BsonDocument
                {
                    { "ShowId", 1 },
                    { "Cinema", "$Cinema" },
                    { "Movie", "$Movie" }
                })
            };

            var movieShows = await _movieShows.Aggregate<MovieShowDto>(pipeline).ToListAsync();
            return movieShows;
        }
    }
}
