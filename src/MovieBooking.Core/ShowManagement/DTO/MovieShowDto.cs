namespace MovieBooking.Core;

public record MovieShowResponse
{
    public required string ShowId { get; set; }
    public required string Cinema { get; set; }

    public required string Screen { get; set; }

    public required MovieDto Movie { get; set; }
    public required DateTimeOffset ShowTime { get; set; }
}
