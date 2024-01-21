using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MovieBooking.Core
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly IMongoCollection<Cinema> _cinemas;

        public CinemaRepository(IMongoClient client, IOptions<MongoDbSettings> options)
        {
            var database = client.GetDatabase(options.Value.Database);
            _cinemas = database.GetCollection<Cinema>(DBCollections.CINEMAS);
        }

        public async Task<Cinema> GetCinemaById(string cinemaId)
        {
            var filter = Builders<Cinema>.Filter.Eq(cinema => cinema.CinemaId, cinemaId);
            return await _cinemas.Find(filter).FirstOrDefaultAsync();
        }
    }
}
