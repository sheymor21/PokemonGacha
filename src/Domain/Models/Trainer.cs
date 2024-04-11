using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models;

public class Trainer
{
    [BsonId] public Guid TrainerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public List<Pokemon> Pokemons { get; set; } = new();
}