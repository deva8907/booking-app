using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieBooking.Core;

public class Screen
{
    [BsonElement]
    [BsonRequired]
    public required string ScreenId { get; set; }

    [BsonElement]
    [BsonRequired]
    public required string Name { get; set; }

    [BsonElement]
    [BsonRequired]
    public required int TotalSeats { get; set; }
}
