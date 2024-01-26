using Microsoft.AspNetCore.Mvc;
using MovieBooking.Core;

namespace MovieBooking.Api;
public class BookingCoreController(ICoreRepository coreRepository) : ApiBaseController
{
    //can be refactored later to use a service
    private readonly ICoreRepository _coreRepository = coreRepository;
    
    [HttpGet("cities")]
    public async Task<IEnumerable<City>> GetCities()
    {
        return await _coreRepository.GetAllCities();
    }

    [HttpGet("cinemas")]
    public async Task<IEnumerable<Cinema>> GetCinemas()
    {
        return await _coreRepository.GetAllCinemas();
    }

    [HttpGet("movies")]
    public async Task<IEnumerable<Movie>> GetMovies()
    {
        return await _coreRepository.GetAllMovies();
    }
}
