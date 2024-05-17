using Domain.Context;
using Domain.Dto;
using Domain.Interfaces;
using Domain.Models.JsonModels;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

namespace Infrastructure.Services;

public class PokemonServices : IPokemonServices
{
    private readonly DatabaseContext _databaseContext;
    private readonly IMemoryCache _cache;
    private const string Url = "https://pokeapi.co/api/v2/";

    public PokemonServices(DatabaseContext databaseContext, IMemoryCache cache)
    {
        _databaseContext = databaseContext;
        _cache = cache;
    }

    public async Task<PokemonGetRequest> GetPokemonAsync(Guid trainerId)
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
            string cacheEntry = pokemonGetRequest.Name;
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
            _cache.Set($"PokemonName{trainerId}", cacheEntry, cacheOptions);

            return pokemonGetRequest;
        }
        catch (NullReferenceException)
        {
            return await GetPokemonAsync(trainerId);
        }
    }

    public async Task<PokemonGetRequest> GetPokemonByName(string name)
    {
        var client = new RestClient(Url);
        var request = new RestRequest($"pokemon/{name}");
        var response = await client.ExecuteAsync<PokemonRoot>(request);
        PokemonGetRequest pokemonGetRequest = new()
        {
            Name = response.Data!.name,
            Type = response.Data.types.Select(w => w.Type.name).ToList()
        };
        return pokemonGetRequest;
    }

    private async Task<PokemonCountListRequest> GetPokemonListAsync()
    {
        var client = new RestClient(Url);
        var request = new RestRequest("pokemon?limit=100000&offset=0");
        var response = await client.ExecuteAsync<PokemonCountListRequest>(request);
        return response.Data!;
    }
}