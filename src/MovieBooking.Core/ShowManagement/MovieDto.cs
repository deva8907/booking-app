namespace MovieBooking.Core;

public record MovieDto
{
    public required string Name { get; set; }
    public required int RuntimeMinutes { get; set; }
    public required string PlotSummary { get; set; }
}
