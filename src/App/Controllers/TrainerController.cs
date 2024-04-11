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
        await _trainerServices.AddTrainer(trainerCreateRequest);
        return Ok();
    }
    
    [HttpGet]
    public async Task<ActionResult> GetTrainer()
    {
        return Ok(await _trainerServices.GetTrainers());
    }
}