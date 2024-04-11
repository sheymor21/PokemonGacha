namespace Domain.Dto;

public class PokemonDto
{
    public string Name { get; set; } = string.Empty;
    public List<string> Type { get; set; } = new();
}

public class PokemonGetRequest
{
    public string Name { get; set; } = string.Empty;
    public List<string> Type { get; set; } = new();
}
public class PokemonCreateRequest
{
    public Guid TrainerId { get; set; }
    public string PokemonName { get; set; } = string.Empty;
}
