using MongoDB.Bson;
using MongoDB.Driver;

namespace MovieBooking.Core
{
    public class MovieShowRepository(IMongoDatabase database) : IMovieShowRepository
    {
        private readonly IMongoCollection<MovieShow> _movieShows = database.GetCollection<MovieShow>(DBCollections.MOVIE_SHOWS);

        public Task DeleteMovieShow(string showId)
        {
            return _movieShows.DeleteOneAsync(movieShow => movieShow.ShowId == showId);
        }

        public Task<MovieShow> GetMovieShowById(string showId)
        {
            return _movieShows.Find(movieShow => movieShow.ShowId == showId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MovieShowResponse>> GetMovieShowsByCinemaId(string cinemaId)
        {
            var movieShows = await _movieShows.Find(movieShow => movieShow.Cinema.CinemaId == cinemaId).ToListAsync();
            return movieShows.Select(movieShow => new MovieShowResponse
            {
                ShowId = movieShow.ShowId,
                Cinema = movieShow.Cinema.Name,
                Screen = movieShow.Cinema.Screen,
                Movie = new MovieDto
                {
                    Name = movieShow.Movie.Name,
                    PlotSummary = movieShow.Movie.PlotSummary,
                    RuntimeMinutes = movieShow.Movie.RuntimeMinutes
                },
                ShowTime = movieShow.ShowTime
            });
        }

        public async Task SaveMovieShow(MovieShow movieShow)
        {
            await _movieShows.InsertOneAsync(movieShow);
        }

        public async Task<IEnumerable<SearchMovieShowResponse>> SearchMovieShows(string searchValue)
        {
            var filterByShowTime = Builders<MovieShow>.Filter.Gte(m => m.ShowTime, DateTimeOffset.UtcNow);
            var filterBySearchValue = Builders<MovieShow>.Filter.Or(
                Builders<MovieShow>.Filter.Regex(m => m.Cinema.Name, new BsonRegularExpression(searchValue, "i")),
                Builders<MovieShow>.Filter.Regex(m => m.Movie.Name, new BsonRegularExpression(searchValue, "i")));
            var filter = Builders<MovieShow>.Filter.And(filterByShowTime, filterBySearchValue);
            var movieShows = await _movieShows.Find(filter).ToListAsync();
            return new List<SearchMovieShowResponse>(movieShows.Select(movieShow => new SearchMovieShowResponse
            {
                Cinema = movieShow.Cinema.Name,
                Screen = movieShow.Cinema.Screen,
                Movie = new MovieDto
                {
                    Name = movieShow.Movie.Name,
                    PlotSummary = movieShow.Movie.PlotSummary,
                    RuntimeMinutes = movieShow.Movie.RuntimeMinutes
                },
                ShowTime = movieShow.ShowTime
            }));
        }
    }
}
