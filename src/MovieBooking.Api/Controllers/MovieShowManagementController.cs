using Microsoft.AspNetCore.Mvc;
using MovieBooking.Core;

namespace MovieBooking.Api;

[ApiController]
[Route("api/v1")]
public class MovieShowManagementController(MovieShowService movieShowService)
{
    private readonly MovieShowService _movieShowService = movieShowService;

    [HttpGet("cinemas/{cinemaId}/movie-shows")]
    public async Task<IEnumerable<MovieShowDto>> GetMovieShowsByCinemaId(string cinemaId)
    {
        return await Task.FromResult(Enumerable.Empty<MovieShowDto>());
    }
}
