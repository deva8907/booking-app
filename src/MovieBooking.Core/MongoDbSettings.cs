using MongoDB.Driver;

namespace MovieBooking.Core;

public class MongoDbSettings
{
    public required string ConnectionString { get; set; }
    public required string Database { get; set; }
}
