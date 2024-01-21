namespace MovieBooking.Core;

public record CreateMovieShowResponse : CreateMovieShowRequest
{
    public required string ShowId { get; set; }
}
