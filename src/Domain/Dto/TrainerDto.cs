using System.Text.Json.Serialization;

namespace Domain.Dto;

public class TrainerCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    
    public List<PokemonDto> Pokemons { get; set; } = new();
}