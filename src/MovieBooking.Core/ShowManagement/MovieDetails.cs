using MongoDB.Bson.Serialization.Attributes;

namespace MovieBooking.Core;

public class MovieDetails
{
    [BsonElement]
    [BsonRequired]
    public required string MovieId { get; set; }

    [BsonElement]
    [BsonRequired]
    public required string Name { get; set; }

    [BsonElement]
    [BsonRequired]
    public required int RuntimeMinutes { get; set; }

    [BsonElement]
    [BsonRequired]
    public required string PlotSummary { get; set; }
}
