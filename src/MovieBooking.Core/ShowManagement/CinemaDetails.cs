using MongoDB.Bson.Serialization.Attributes;

namespace MovieBooking.Core.ShowManagement
{
    public class CinemaDetails
    {

        [BsonElement]
        [BsonRequired]
        public required string CinemaId { get; set; }

        [BsonElement]
        [BsonRequired]
        public required string Name { get; set; }
        
        [BsonElement]
        [BsonRequired]
        public required string City { get; set; }

        [BsonElement]
        [BsonRequired]
        public required string ScreenId { get; set; }
        
        [BsonElement]
        [BsonRequired]
        public required string Screen { get; set; }
    }
}