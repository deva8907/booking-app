namespace MovieBooking.Core;

public interface IMovieShowRepository
{
    Task SaveMovieShow(MovieShow movieShow);
    Task<IEnumerable<MovieShowResponse>> GetMovieShowsByCinemaId(string cinemaId);
    Task<IEnumerable<SearchMovieShowResponse>> SearchMovieShows(string searchValue);
}
