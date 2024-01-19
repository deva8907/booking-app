using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieBooking.Core;

public class MovieShow
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement]
    [BsonRequired]
    public required string ShowId { get; set; }

    [BsonElement]
    [BsonRequired]
    public required string CinemaId { get; set; }

    [BsonElement]
    [BsonRequired]
    public required string Screen { get; set; }

    [BsonElement]
    [BsonRequired]
    public required DateTime ShowTime { get; set; }

    [BsonElement]
    [BsonRequired]
    public int TotalSeatsAvailable { get; set; }

    public static MovieShow CreateMovieShow(string cinemaId, string screen, DateTime showTime, int totalSeatsAvailable)
    {
        return new MovieShow
        {
            ShowId = "showId",
            CinemaId = cinemaId,
            Screen = screen,
            ShowTime = showTime,
            TotalSeatsAvailable = totalSeatsAvailable
        };
    }
}
