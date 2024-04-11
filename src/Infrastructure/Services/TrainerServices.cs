using Domain.Context;
using Domain.Dto;
using Domain.Interfaces;
using Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Services;

public class TrainerServices : ITrainerServices
{
    private readonly DatabaseContext _databaseContext;

    public TrainerServices(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task AddTrainerAsync(TrainerCreateRequest trainerCreateRequest)
    {
        Trainer trainer = new()
        {
            Name = trainerCreateRequest.Name,
            Age = trainerCreateRequest.Age
        };

        await _databaseContext.Trainers.InsertOneAsync(trainer);
    }

    public async IAsyncEnumerable<TrainerGetRequest> GetTrainersAsync()
    {
        var projections = Builders<Trainer>.Projection
            .Expression(p => new
            {
                Id = p.TrainerId,
                Name = p.Name,
                LastName = p.LastName,
                Age = p.Age,
                Pokemons = p.Pokemons.Select(x => new PokemonDto
                {
                    Name = x.Name,
                    Type = x.Type
                }).ToList(),
            });
        var trainers = await _databaseContext.Trainers
            .Find(doc => true)
            .Project(projections)
            .ToListAsync();

        foreach (var item in trainers)
        {
            TrainerGetRequest trainerGetRequest = new()
            {
                Id = item.Id,
                FullName = $"{item.Name} {item.LastName}",
                Age = item.Age,
                Pokemons = item.Pokemons
            };
            yield return trainerGetRequest;
        }
    }

    public async Task AddPokemonToTrainerAsync(PokemonCreateRequest pokemonCreateRequest)
    {
        var filter = Builders<Trainer>.Filter.Eq(w => w.TrainerId, pokemonCreateRequest.TrainerId);

        Pokemon pokemon = new()
        {
            Name = pokemonCreateRequest.PokemonName,
            Type = new() { "esto" }
        };


        var update = Builders<Trainer>.Update.Push(w => w.Pokemons, pokemon);
        await _databaseContext.Trainers.UpdateOneAsync(filter, update);
    }
}