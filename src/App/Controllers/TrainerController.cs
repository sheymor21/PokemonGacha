using Domain.Dto;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace App.Controllers;

public class TrainerController : BaseController
{
    private readonly ITrainerServices _trainerServices;
    private readonly IMemoryCache _cache;

    public TrainerController(ITrainerServices trainerServices, IMemoryCache cache)
    {
        _trainerServices = trainerServices;
        _cache = cache;
    }

    /// <summary>
    /// Adds a new trainer to the system.
    /// </summary>
    /// <param name="trainerCreateRequest">The request object containing the details of the trainer.</param>
    /// <returns>An ActionResult indicating the result of the operation.</returns>
    [HttpPost]
    public async Task<ActionResult> AddTrainer(TrainerCreateRequest trainerCreateRequest)
    {
        await _trainerServices.AddTrainerAsync(trainerCreateRequest);
        return Ok();
    }

    /// <summary>
    /// Adds a new Pokemon to a trainer.
    /// </summary>
    /// <param name="pokemonCreateRequest">The request object containing the trainer ID and the name of the Pokemon to be added.</param>
    /// <returns>An ActionResult indicating the result of the operation.</returns>
    [HttpPost("Pokemon")]
    public async Task<ActionResult> AddPokemonToTrainer(PokemonCreateRequest pokemonCreateRequest)
    {
        var pokemonNames = await _trainerServices.GetTrainerPokemonNamesAsync(pokemonCreateRequest.TrainerId);
        if (pokemonNames.Count == 6)
        {
            return BadRequest("Max pokemon is 6");
        }

        _cache.TryGetValue($"PokemonName{pokemonCreateRequest.TrainerId}", out string? value);
        if (value == pokemonCreateRequest.PokemonName)
        {
            if (!pokemonNames.Exists(w => w == pokemonCreateRequest.PokemonName))
            {
                await _trainerServices.AddPokemonToTrainerAsync(pokemonCreateRequest);
                _cache.Remove(value);
                return Ok();
            }

            return BadRequest("You have this exact pokemon, for repeat the same pokemon you need to catch it again");
        }

        return BadRequest("You doesnt catch this pokemon yet");
    }

    /// <summary>
    /// Removes a Pokemon from a trainer.
    /// </summary>
    /// <param name="pokemonDeleteRequest">The request object containing the trainer ID and the name of the Pokemon to be removed.</param>
    /// <returns>An ActionResult indicating the result of the operation.</returns>
    [HttpDelete("")]
    public async Task<ActionResult> DeletePokemonFromTrainer(PokemonDeleteRequest pokemonDeleteRequest)
    {
        await _trainerServices.RemovePokemonFromTrainer(pokemonDeleteRequest);
        return Ok();
    }

    /// <summary>
    /// Retrieves the detailed information of a trainer.
    /// </summary>
    /// <returns>The detailed information of the trainer.</returns>
    [HttpGet]
    public Task<ActionResult> GetTrainer()
    {
        return Task.FromResult<ActionResult>(Ok(_trainerServices.GetTrainersAsync()));
    }
}