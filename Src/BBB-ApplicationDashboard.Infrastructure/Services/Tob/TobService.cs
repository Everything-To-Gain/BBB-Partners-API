using BBB_ApplicationDashboard.Application.Interfaces;
using BBB_ApplicationDashboard.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BBB_ApplicationDashboard.Infrastructure.Services.Tob;

public class TobService(IMongoDatabase database) : ITobService
{
    public async Task<List<TOB>> GetTOBs(string? searchTerm)
    {
        var col = database.GetCollection<BsonDocument>("tobs");
        var projection = new BsonDocument
        {
            { "CbbbId", new BsonDocument("$toString", "$properties.cbbbid") },
            { "Name", "$properties.tob" },
        };
        var pipeline = col.Aggregate();
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var regex = new BsonRegularExpression(searchTerm, "i");
            pipeline = pipeline.Match(new BsonDocument("properties.tob", regex));
        }

        return await pipeline.Project<TOB>(projection).Limit(10).ToListAsync();
    }

    public async Task<string> GetTOBName(string cbbbId)
    {
        var col = database.GetCollection<BsonDocument>("tobs");

        var filter = Builders<BsonDocument>.Filter.Eq("properties.cbbbid", cbbbId);

        var projection = Builders<BsonDocument>.Projection.Include("properties.tob");

        var doc = await col.Find(filter).Project<BsonDocument>(projection).FirstOrDefaultAsync();

        return doc?["properties"]?["tob"]?.AsString ?? string.Empty;
    }
}
