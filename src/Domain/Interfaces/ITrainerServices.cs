using Domain.Dto;

namespace Domain.Interfaces;

public interface ITrainerServices
{
    Task AddTrainerAsync(TrainerCreateRequest trainerCreateRequest);
    IAsyncEnumerable<TrainerGetRequest> GetTrainersAsync();
    Task AddPokemonToTrainerAsync(PokemonCreateRequest pokemonCreateRequest);
}