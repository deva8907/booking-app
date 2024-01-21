namespace MovieBooking.Core;

public interface IMovieRepository
{
    Task<Movie> GetMovieById(string movieId);
}
