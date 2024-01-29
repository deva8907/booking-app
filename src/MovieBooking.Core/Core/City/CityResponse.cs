namespace MovieBooking.Core;

public record CityResponse
{
    public required string CityId { get; set; }

    public required string Name { get; set; }
}
