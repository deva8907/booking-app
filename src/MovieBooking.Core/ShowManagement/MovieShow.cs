using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MovieBooking.Core.ShowManagement;

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
    public required CinemaDetails Cinema { get; set; }

    [BsonElement]
    [BsonRequired]
    public required DateTimeOffset ShowTime { get; set; }

    [BsonElement]
    [BsonRequired]
    public required MovieDetails Movie { get; set; }

    public static async Task<MovieShow> CreateMovieShow(string cinemaId, CreateMovieShowRequest request, ICinemaRepository cinemaRepository,
        IMovieRepository movieRepository, IMovieShowRepository movieShowRepository)
    {
        var cinema = await cinemaRepository.GetCinemaById(cinemaId) ??
         throw new MovieShowException($"Cinema {cinemaId} does not exist");

        var screen = cinema.Screens.FirstOrDefault(screen => screen.Name == request.Screen || screen.ScreenId == request.Screen) ??
         throw new MovieShowException($"Screen with {request.Screen} does not exist in cinema {cinema.Name}");

        var movie = await movieRepository.GetMovieById(request.MovieId) ?? throw new MovieShowException($"Movie with id {request.MovieId} does not exist");

        if (request.ShowTime < DateTime.UtcNow)
            throw new MovieShowException($"Cannot create shows for past time {request.ShowTime}");

        await movieShowRepository.GetMovieShowsByCinemaId(cinema.CinemaId).ContinueWith(movieShows =>
        {
            if (movieShows.Result.Any(movieShow => movieShow.ShowTime == request.ShowTime))
                throw new MovieShowException($"Show already exists for {request.ShowTime}");
        });
        
        return new MovieShow
        {
            ShowId = Guid.NewGuid().ToString(),
            Cinema = new CinemaDetails
            {
                CinemaId = cinema.CinemaId,
                Name = cinema.Name,
                ScreenId = screen.ScreenId,
                Screen = screen.Name,
                City = cinema.City
            },
            ShowTime = request.ShowTime,
            Movie = new MovieDetails
            {
                MovieId = movie.MovieId,
                Name = movie.Name,
                PlotSummary = movie.PlotSummary,
                RuntimeMinutes = movie.RuntimeMinutes
            }
        };
    }
}