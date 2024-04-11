using Domain.Context;
using Domain.Dto;
using Domain.Interfaces;
using Domain.Models.JsonModels;
using RestSharp;

namespace Infrastructure.Services;

public class PokemonServices : IPokemonServices
{
    private readonly DatabaseContext _databaseContext;
    private readonly string Url = "https://pokeapi.co/api/v2/";

    public PokemonServices(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<PokemonGetRequest> GetPokemonAsync()
    {
        var client = new RestClient(Url);
        var pokemonList = await GetPokemonListAsync();
        Random random = new();
        try
        {
            var id = random.Next(1, pokemonList.count);
            var pokemonName = pokemonList.results[id].name;
            var request = new RestRequest($"pokemon/{pokemonName}");
            var response = await client.ExecuteAsync<PokemonRoot>(request);

            PokemonGetRequest pokemonGetRequest = new()
            {
                Name = response.Data!.name,
                Type = response.Data.types.Select(w => w.Type.name).ToList()
            };
            return pokemonGetRequest;
        }
        catch (NullReferenceException)
        {
            return await GetPokemonAsync();
        }
    }

    private async Task<PokemonCountListRequest> GetPokemonListAsync()
    {
        var client = new RestClient(Url);
        var request = new RestRequest("pokemon?limit=100000&offset=0");
        var response = await client.ExecuteAsync<PokemonCountListRequest>(request);
        return response.Data!;
    }
}