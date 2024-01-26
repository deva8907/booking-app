namespace MovieBooking.Core;

public interface ICoreRepository
{
    Task<IEnumerable<City>> GetAllCities();
    Task<IEnumerable<Cinema>> GetAllCinemas();
    Task<IEnumerable<Movie>> GetAllMovies();
}
