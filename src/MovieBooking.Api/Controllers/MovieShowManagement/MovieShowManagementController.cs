using Microsoft.AspNetCore.Mvc;
using MovieBooking.Core;

namespace MovieBooking.Api;

public class MovieShowManagementController(MovieShowService movieShowService, ILogger<MovieShowManagementController> logger) : ApiBaseController
{
    private readonly MovieShowService _movieShowService = movieShowService;
    private readonly ILogger _logger = logger;

    [HttpGet("cinemas/{cinemaId}/movie-shows")]
    public async Task<IEnumerable<MovieShowResponse>> GetMovieShowsByCinemaId(string cinemaId)
    {
        return await _movieShowService.GetMovieShows(cinemaId);
    }

    [HttpPost("cinemas/{cinemaId}/movie-shows")]
    public async Task<IActionResult> CreateMovieShow(string cinemaId, CreateMovieShowRequest movieShowRequest)
    {
        try
        {
            movieShowRequest.CinemaId ??= cinemaId;
            var response = await _movieShowService.CreateMovieShow(movieShowRequest);
            return Ok(response);
        }
        catch (MovieShowException mex)
        {
            _logger.LogError("Bad request: {Message}", mex);
            return BadRequest(mex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Internal server error: {Error}", ex);
            return StatusCode(500, "Error while creating movie show, please try again later");
        }
    }
}
