using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieBooking.Core;

public class Cinema
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("CinemaId")]
    [BsonRequired]
    public required string CinemaId { get; set; }

    [BsonElement("Name")]
    [BsonRequired]
    public required string Name { get; set; }

    [BsonElement("City")]
    [BsonRequired]
    public required string City { get; set; }
}
