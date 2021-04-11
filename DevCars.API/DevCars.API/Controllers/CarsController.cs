using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Http;
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
        
        /// <summary>
        /// Obter dados de um carro
        /// </summary>        
        /// <param name="id">Id de um carro</param>        
        /// <returns>Informações detalhadas de um carro</returns>
        /// <response code="200">Objeto encontrado.</response>
        /// <response code="404">Objeto não encontrado.</response>        
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
        
        /// <summary>
        /// Cadastrar um carro
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        ///     "brand": "Chevrolet",
        ///     "model":   "Tracker",
        ///     "vinCode": "abc123",
        ///     "year": 2020,
        ///     "color": "prata",
        ///     "price": 100000
        ///     "productionDate": "2020-12-07"
        /// }
        /// </remarks>
        /// <param name="model">Dados de um novo carro</param>
        /// <returns>Objeto recém-criado</returns>
        /// <response code="201">Objeto criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] AddCarInputModel model)
        {
            if(model.Model.Length > 50)
            {
                return BadRequest("Modelo não pode ter mais de 50 caracteres");
            }

            var car = new Car(model.VinCode, model.Brand, model.Model, model.Year, model.Price, model.Color, model.ProductionDate);

            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = car.Id }, model);
        }
        
        /// <summary>
        /// Atualizar dados de um carro
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        ///     "color": "Vermelho",
        ///     "price": 100000
        /// }
        /// </remarks>
        /// <param name="id">Identificador de um carro</param>
        /// <param name="model">Dados de alteração</param>
        /// <returns>Não tem retorno.</returns>
        /// <response code="204">Alterações realizadas com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="404">Carro não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] UpdateCarInputModel model)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);

            if(car == null)
            {
                return NotFound();
            }

            car.Update(model.Color, model.Price);

            _dbContext.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Excluir um cadastro de carro
        /// </summary>        
        /// <param name="id">Id de um carro</param>        
        /// <returns>Sem retorno.</returns>
        /// <response code="204">Exclusão realizada com sucesso.</response>
        /// <response code="404">Objeto não encontrado.</response>     
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(car => car.Id == id);

            if(car == null)
            {
                return NotFound();
            }

            car.SetAsSuspended();

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
