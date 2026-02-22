using LoggingService.Domain.Entities;
using MongoDB.Driver;

namespace LoggingService.Infrastructure.Services;

public class MongoLogService
{
    private readonly IMongoCollection<RequestLog> _logs;

    public MongoLogService()
    {
        var client = new MongoClient("mongodb://localhost:27017");

        var database = client.GetDatabase("WeatherLogs");

        _logs = database.GetCollection<RequestLog>("RequestLogs");
    }

    public async Task LogRequest(RequestLog log)
    {
        await _logs.InsertOneAsync(log);
    }
}