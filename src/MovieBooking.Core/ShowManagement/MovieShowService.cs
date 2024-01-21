namespace MovieBooking.Core;

public class MovieShowService(MovieShowRepository movieShowRepository)
{
    private readonly MovieShowRepository _movieShowRepository = movieShowRepository;
    public async Task<IEnumerable<MovieShowDto>> GetMovieShows(string cinemaId)
    {
        return await _movieShowRepository.GetMovieShowsByCinemaId(cinemaId);    
    }
}
