using Domain.Dto;

namespace Domain.Interfaces;

public interface ITrainerServices
{
    Task AddTrainerAsync(TrainerCreateRequest trainerCreateRequest);
    IAsyncEnumerable<TrainerGetRequest> GetTrainersAsync();
    Task AddPokemonToTrainerAsync(PokemonCreateRequest pokemonCreateRequest);
    Task<List<string>> GetTrainerPokemonNamesAsync(Guid trainerId);
    Task RemovePokemonFromTrainer(PokemonDeleteRequest pokemonDeleteRequest);
}