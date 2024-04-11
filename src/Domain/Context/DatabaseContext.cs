using Domain.Models;
using MongoDB.Driver;

namespace Domain.Context;

public class DatabaseContext
{
    private readonly IMongoDatabase _mongoClient;

    public DatabaseContext(string connectionString)
    {
        var client = new MongoClient(connectionString);
        _mongoClient = client.GetDatabase("PokemonDb");
    }

    public IMongoCollection<Trainer> Trainers => _mongoClient.GetCollection<Trainer>(nameof(Trainers).ToLower());
}