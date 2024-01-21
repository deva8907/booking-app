
namespace MovieBooking.Core;

public class MovieShowService(IMovieShowRepository movieShowRepository, ICinemaRepository cinemaRepository, IMovieRepository movieRepository)
{
    private readonly IMovieShowRepository _movieShowRepository = movieShowRepository;
    private readonly ICinemaRepository _cinemaRepository = cinemaRepository;
    private readonly IMovieRepository _movieRepository = movieRepository;

    public async Task<CreateMovieShowResponse> CreateMovieShow(CreateMovieShowRequest request)
    {
        var movieShow = await MovieShow.CreateMovieShow(request, _cinemaRepository, _movieRepository);
        await _movieShowRepository.SaveMovieShow(movieShow);
        return new CreateMovieShowResponse
        {
            ShowId = movieShow.ShowId,
            CinemaId = movieShow.CinemaId,
            Screen = movieShow.ScreenId,
            MovieId = movieShow.MovieId,
            ShowTime = movieShow.ShowTime
        };
    }

    public async Task<IEnumerable<MovieShowResponse>> GetMovieShows(string cinemaId)
    {
        return await _movieShowRepository.GetMovieShowsByCinemaId(cinemaId);    
    }
}
