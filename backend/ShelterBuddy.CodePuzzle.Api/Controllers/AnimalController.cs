using Microsoft.AspNetCore.Mvc;
using ShelterBuddy.CodePuzzle.Api.Models;
using ShelterBuddy.CodePuzzle.Core.DataAccess;
using ShelterBuddy.CodePuzzle.Core.Entities;

namespace ShelterBuddy.CodePuzzle.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalController : ControllerBase
{
    private readonly IRepository<Animal, Guid> repository;

    public AnimalController(IRepository<Animal, Guid> animalRepository)
    {
        repository = animalRepository;
    }

    [HttpGet]
    public AnimalModel[] Get() => repository.GetAll().Select(animal => new AnimalModel
    {
        Id = $"{animal.Id}",
        Name = animal.Name,
        Colour = animal.Colour,
        Species = animal.Species,
        DateFound = animal.DateFound,
        DateLost = animal.DateLost,
        MicrochipNumber = animal.MicrochipNumber,
        DateInShelter = animal.DateInShelter,
        DateOfBirth = animal.DateOfBirth,
        AgeText = animal.AgeText,
        AgeMonths = animal.AgeMonths,
        AgeWeeks = animal.AgeWeeks,
        AgeYears = animal.AgeYears
    }).ToArray();

    [HttpPost]
    public ActionResult Post([FromForm] AnimalModel newAnimal)
    {
        try
        {
            if (newAnimal.Name is null)
            {
                throw new Exception("Name cannot be empty");
            }

            if (newAnimal.Species is null)
            {
                throw new Exception("Species cannot be empty");
            }

            if (newAnimal.DateOfBirth is null & newAnimal.AgeMonths is null & newAnimal.AgeWeeks is null & newAnimal.AgeYears is null)
            {
                throw new Exception("Either Date of Birth or Age cannot be empty");
            }

            repository.Add(new Animal
            {   
                Id = Guid.NewGuid(),
                Name = newAnimal.Name,
                Colour = newAnimal.Colour,
                Species = newAnimal.Species,
                DateFound = newAnimal.DateFound,
                DateLost = newAnimal.DateLost,
                MicrochipNumber = newAnimal.MicrochipNumber,
                DateInShelter = newAnimal.DateInShelter,
                DateOfBirth = newAnimal.DateOfBirth,
                AgeMonths = newAnimal.AgeMonths,
                AgeWeeks = newAnimal.AgeWeeks,
                AgeYears = newAnimal.AgeYears
            });

            return Ok(repository.GetAll().Select(animal => new AnimalModel
            {
                Id = $"{animal.Id}",
                Name = animal.Name,
                Colour = animal.Colour,
                Species = animal.Species,
                DateFound = animal.DateFound,
                DateLost = animal.DateLost,
                MicrochipNumber = animal.MicrochipNumber,
                DateInShelter = animal.DateInShelter,
                DateOfBirth = animal.DateOfBirth,
                AgeText = animal.AgeText,
                AgeMonths = animal.AgeMonths,
                AgeWeeks = animal.AgeWeeks,
                AgeYears = animal.AgeYears
            }).ToArray());
        }
        catch (Exception e) 
        {
            return BadRequest(e.Message);
        }
        
    }
}