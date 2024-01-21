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

    public static async Task<MovieShow> CreateMovieShow(CreateMovieShowRequest request, ICinemaRepository cinemaRepository,
        IMovieRepository movieRepository)
    {
        var cinema = await cinemaRepository.GetCinemaById(request.CinemaId) ??
         throw new MovieShowException($"Cinema {request.CinemaId} does not exist");

        var screen = cinema.Screens.FirstOrDefault(screen => screen.Name == request.Screen) ??
         throw new MovieShowException($"Screen with {request.Screen} does not exist in cinema {cinema.Name}");

        var movie = await movieRepository.GetMovieById(request.MovieId) ?? throw new MovieShowException($"Movie with id {request.MovieId} does not exist");

        if (request.ShowTime < DateTime.UtcNow)
            throw new MovieShowException($"Show time {request.ShowTime} cannot be in the past");
        return new MovieShow
        {
            ShowId = Guid.NewGuid().ToString(),
            CinemaId = request.CinemaId,
            ScreenId = screen.ScreenId,
            ShowTime = request.ShowTime,
            MovieId = movie.MovieId
        };
    }
}