namespace MovieBooking.Core;

public record CreateMovieShowRequest
{
    public required string CinemaId { get; set; }

    public required string Screen { get; set; }

    public required string MovieId { get; set; }

    public required DateTimeOffset ShowTime { get; set; }
}
