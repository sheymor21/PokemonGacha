using Domain.Dto;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

public class TrainerController : BaseController
{
    private readonly ITrainerServices _trainerServices;

    public TrainerController(ITrainerServices trainerServices)
    {
        _trainerServices = trainerServices;
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
        await _trainerServices.AddPokemonToTrainerAsync(pokemonCreateRequest);
        return Ok();
    }

    [HttpGet]
    public Task<ActionResult> GetTrainer()
    {
        return Task.FromResult<ActionResult>(Ok(_trainerServices.GetTrainersAsync()));
    }
}