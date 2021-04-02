using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;

        public CarsController(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var cars = _dbContext.Cars;

            var carsViewModel = cars.Select(c => new CarItemViewModel(c.Id, c.Brand, c.Model, c.Price)).ToList();

            return Ok(carsViewModel);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);

            if(car == null)
            {
                return NotFound();
            }

            var carDetailsViewModel = new CarDetailsViewModel(car.Id, car.Brand, car.Model, car.Color, car.Year, car.Price, car.ProductionDate, car.VinCode);

            return Ok(carDetailsViewModel);
        }
                
        [HttpPost]
        public IActionResult Post([FromBody] AddCarInputModel model)
        {
            if(model.Model.Length > 50)
            {
                return BadRequest("Modelo não pode ter mais de 50 caracteres");
            }

            var car = new Car(4, model.VinCode, model.Brand, model.Model, model.Year, model.Price, model.Color, model.ProductionDate);

            _dbContext.Cars.Add(car);

            return CreatedAtAction(nameof(GetById), new { id = car.Id }, model);
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCarInputModel model)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);

            if(car == null)
            {
                return NotFound();
            }

            car.Update(model.Color, model.Price);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(car => car.Id == id);

            if(car == null)
            {
                return NotFound();
            }

            car.SetAsSuspended();

            return NoContent();
        }
    }
}
