using Domain.Context;
using Domain.Dto;
using Domain.Interfaces;
using Domain.Models;
using MongoDB.Driver;

namespace Infrastructure.Services;

public class TrainerServices : ITrainerServices
{
    private readonly DatabaseContext _databaseContext;

    public TrainerServices(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task AddTrainer(TrainerCreateRequest trainerCreateRequest)
    {
        Trainer trainer = new()
        {
            Name = trainerCreateRequest.Name,
            Age = trainerCreateRequest.Age
        };
        foreach (var item in trainerCreateRequest.Pokemons)
        {
            Pokemon pokemon = new()
            {
                Name = item.Name,
                Type = item.Type
            };
            trainer.Pokemons.Add(pokemon);
        }

        await _databaseContext.Trainers.InsertOneAsync(trainer);
    }

    public async Task<List<Trainer>> GetTrainers()
    {
        return await _databaseContext.Trainers.Find(doc=> true).ToListAsync();
    }
}