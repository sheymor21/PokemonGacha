
namespace Domain.Dto;

public class TrainerCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
}

public class TrainerGetRequest
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int Age { get; set; }
    public List<PokemonDto> Pokemons { get; set; } = new();
}