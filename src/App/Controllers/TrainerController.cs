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

    [HttpPost]
    public async Task<ActionResult> AddTrainer(TrainerCreateRequest trainerCreateRequest)
    {
        await _trainerServices.AddTrainerAsync(trainerCreateRequest);
        return Ok();
    }

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

    [HttpDelete("")]
    public async Task<ActionResult> DeletePokemonFromTrainer(PokemonDeleteRequest pokemonDeleteRequest)
    {
        await _trainerServices.RemovePokemonFromTrainer(pokemonDeleteRequest);
        return Ok();
    }

    [HttpGet]
    public Task<ActionResult> GetTrainer()
    {
        return Task.FromResult<ActionResult>(Ok(_trainerServices.GetTrainersAsync()));
    }
}