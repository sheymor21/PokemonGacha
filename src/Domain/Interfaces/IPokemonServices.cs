using Domain.Dto;

namespace Domain.Interfaces;

public interface IPokemonServices
{
    Task<PokemonGetRequest> GetPokemonAsync();
}