using Microsoft.AspNetCore.Mvc;
using MovieBooking.Core;

namespace MovieBooking.Api;

public class MovieShowBookingController(MovieShowService movieShowService) : ApiBaseController
{
    private readonly MovieShowService _movieShowService = movieShowService;

    [HttpGet("movie-shows/search")]
    public async Task<IEnumerable<SearchMovieShowResponse>> GetMovieShowBookings([FromQuery] string searchValue)
    {
        return await _movieShowService.SearchMovieShows(searchValue);
    }
}
