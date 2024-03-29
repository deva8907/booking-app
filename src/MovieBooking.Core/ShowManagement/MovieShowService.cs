﻿

namespace MovieBooking.Core;

public class MovieShowService(IMovieShowRepository movieShowRepository, ICinemaRepository cinemaRepository, IMovieRepository movieRepository)
{
    private readonly IMovieShowRepository _movieShowRepository = movieShowRepository;
    private readonly ICinemaRepository _cinemaRepository = cinemaRepository;
    private readonly IMovieRepository _movieRepository = movieRepository;

    public async Task<CreateMovieShowResponse> CreateMovieShow(string cinemaId, CreateMovieShowRequest request)
    {
        var movieShow = await MovieShow.CreateMovieShow(cinemaId, request, _cinemaRepository, _movieRepository,
            _movieShowRepository);
        await _movieShowRepository.SaveMovieShow(movieShow);
        return new CreateMovieShowResponse
        {
            ShowId = movieShow.ShowId
        };
    }

    public async Task DeleteMovieShow(string showId)
    {
        var _ = await _movieShowRepository.GetMovieShowById(showId) ?? throw new MovieShowException($"Movie show with id {showId} does not exist");
        await _movieShowRepository.DeleteMovieShow(showId);
    }

    public async Task<IEnumerable<MovieShowResponse>> GetMovieShows(string cinemaId)
    {
        return await _movieShowRepository.GetMovieShowsByCinemaId(cinemaId);    
    }

    public async Task<IEnumerable<SearchMovieShowResponse>> SearchMovieShows(string searchValue)
    {
        return await _movieShowRepository.SearchMovieShows(searchValue);
    }
}
