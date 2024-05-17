using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

public class PokemonController : BaseController
{
    private readonly IPokemonServices _pokemonServices;

    public PokemonController(IPokemonServices pokemonServices)
    {
        _pokemonServices = pokemonServices;
    }

    [HttpGet]
    public async Task<ActionResult> GetPokemon(Guid trainerId)
    {
        return Ok(await _pokemonServices.GetPokemonAsync(trainerId));
    }
}