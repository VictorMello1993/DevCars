using DevCars.API.Persistence;
using DevCars.Application.Commands.AddCar;
using DevCars.Application.Commands.DeleteCar;
using DevCars.Application.Commands.UpdateCar;
using DevCars.Application.InputModels;
using DevCars.Application.Queries.GetAllCars;
using DevCars.Application.Queries.GetCarById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly IMediator _mediator;

        public CarsController(DevCarsDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllCarsQuery();

            var cars = await _mediator.Send(query);

            return Ok(cars);
        }
        
        /// <summary>
        /// Obter dados de um carro
        /// </summary>        
        /// <param name="id">Id de um carro</param>        
        /// <returns>Informações detalhadas de um carro</returns>
        /// <response code="200">Objeto encontrado.</response>
        /// <response code="404">Objeto não encontrado.</response>        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetCarByIdQuery(id);

            var car = await _mediator.Send(query);

            if(car == null)
            {
                return NotFound();
            }

            return Ok(car);
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
        /// <param name="command">Dados de um novo carro</param>
        /// <returns>Objeto recém-criado</returns>
        /// <response code="201">Objeto criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AddCarCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
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
        /// <param name="inputModel">Dados de alteração</param>
        /// <returns>Não tem retorno.</returns>
        /// <response code="204">Alterações realizadas com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="404">Carro não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCarInputModel inputModel)
        {
            var command = new UpdateCarCommand(inputModel.Color, inputModel.Price, id);

            if(id < 0)
            {
                return BadRequest("O id do carro não pode ser negativo!");
            }

            await _mediator.Send(command);

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
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCarCommand(id);

            if(id < 0)
            {
                return BadRequest("O id do carro não pode ser negativo!");
            }

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
