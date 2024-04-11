using Domain.Dto;
using Domain.Models;
using MongoDB.Driver;

namespace Domain.Interfaces;

public interface ITrainerServices
{
    Task AddTrainer(TrainerCreateRequest trainerCreateRequest);
    Task<List<Trainer>> GetTrainers();
}