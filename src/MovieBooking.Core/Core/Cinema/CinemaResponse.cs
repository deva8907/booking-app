namespace MovieBooking.Core;

public record CinemaResponse
{
    public required string CinemaId { get; set; }

    public required string Name { get; set; }

    public required string City { get; set; }

    public required List<Screen> Screens { get; set; }
}
