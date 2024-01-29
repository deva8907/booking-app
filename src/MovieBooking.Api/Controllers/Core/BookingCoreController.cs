using Microsoft.AspNetCore.Mvc;
using MovieBooking.Core;

namespace MovieBooking.Api;
public class BookingCoreController(ICoreRepository coreRepository) : ApiBaseController
{
    //can be refactored later to use a service
    private readonly ICoreRepository _coreRepository = coreRepository;
    
    [HttpGet("cities")]
    public async Task<IEnumerable<CityResponse>> GetCities()
    {
        var cities = await _coreRepository.GetAllCities();
        return cities.Select(city => new CityResponse
        {
            CityId = city.CityId,
            Name = city.Name
        });
    }

    [HttpGet("cinemas")]
    public async Task<IEnumerable<CinemaResponse>> GetCinemas()
    {
        var cinemas = await _coreRepository.GetAllCinemas();
        return cinemas.Select(cinema => new CinemaResponse
        {
            CinemaId = cinema.CinemaId,
            Name = cinema.Name,
            City = cinema.City,
            Screens = cinema.Screens
        });
    }

    [HttpGet("movies")]
    public async Task<IEnumerable<MovieResponse>> GetMovies()
    {
        var movies = await _coreRepository.GetAllMovies();
        return movies.Select(movie => new MovieResponse
        {
            MovieId = movie.MovieId,
            Name = movie.Name,
            RuntimeMinutes = movie.RuntimeMinutes,
            PlotSummary = movie.PlotSummary
        });
    }
}
