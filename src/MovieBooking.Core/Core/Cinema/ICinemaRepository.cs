namespace MovieBooking.Core;

public interface ICinemaRepository
{
    Task<Cinema> GetCinemaById(string cinemaId);
}
