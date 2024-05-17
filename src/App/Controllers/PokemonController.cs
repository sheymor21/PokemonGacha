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

    /// <summary>
    /// Retrieves a Pokemon for a given trainer.
    /// </summary>
    /// <param name="trainerId">The ID of the trainer.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains the Pokemon's details as a <see cref="PokemonGetRequest"/>.</returns>
    [HttpGet]
    public async Task<ActionResult> GetPokemon(Guid trainerId)
    {
        return Ok(await _pokemonServices.GetPokemonAsync(trainerId));
    }
}