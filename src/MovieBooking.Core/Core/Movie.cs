using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieBooking.Core;

public class Movie
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    [BsonRequired]
    public required string Name { get; set; }

    [BsonElement("RuntimeMinutes")]
    [BsonRequired]
    public required int RuntimeMinutes { get; set; }

    [BsonElement("PlotSummary")]
    [BsonRequired]
    public required string PlotSummary { get; set; }
}
