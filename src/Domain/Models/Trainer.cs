using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models;

public class Trainer
{
    [BsonId] public Guid TrainerId { get; set; }
    [BsonElement("name")] public string Name { get; set; } = string.Empty;
    [BsonElement("age")] public int Age { get; set; }
    [BsonElement("pokemons")] public List<Pokemon> Pokemons { get; set; } = new();
}