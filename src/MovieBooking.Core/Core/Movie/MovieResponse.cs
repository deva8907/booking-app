namespace MovieBooking.Core;

public record MovieResponse
{
    public required string MovieId { get; set; }
    public required string Name { get; set; }
    public required int RuntimeMinutes { get; set; }
    public required string PlotSummary { get; set; }
}
