using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieBooking.Core;

public class MovieShow
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement]
    [BsonRequired]
    public required string ShowId { get; set; }

    [BsonElement]
    [BsonRequired]
    public required string CinemaId { get; set; }

    [BsonElement]
    [BsonRequired]
    public required string ScreenId { get; set; }

    [BsonElement]
    [BsonRequired]
    public required DateTimeOffset ShowTime { get; set; }

    [BsonElement]
    [BsonRequired]
    public required string MovieId { get; set; }

    public static async Task<MovieShow> CreateMovieShow(string cinemaId, string screenId, DateTimeOffset showTime, string movieId, ICinemaRepository cinemaRepository,
        IMovieRepository movieRepository)
    {
        var cinema = await cinemaRepository.GetCinemaById(cinemaId) ??
         throw new MovieShowException($"Cinema with id {cinemaId} does not exist");

        var screen = cinema.Screens.FirstOrDefault(screen => screen.ScreenId == screenId) ??
         throw new MovieShowException($"Screen with id {screenId} does not exist in cinema {cinema.Name}");

        var movie = await movieRepository.GetMovieById(movieId) ?? throw new MovieShowException($"Movie with id {movieId} does not exist");

        if (showTime < DateTime.UtcNow)
            throw new MovieShowException($"Show time {showTime} cannot be in the past");
        return new MovieShow
        {
            ShowId = Guid.NewGuid().ToString(),
            CinemaId = cinemaId,
            ScreenId = screen.ScreenId,
            ShowTime = showTime,
            MovieId = movie.MovieId
        };
    }
}