using DevCars.API.Persistence;
using DevCars.Application.Commands.AddCostumer;
using DevCars.Application.Commands.AddOrder;
using DevCars.Application.Queries.GetOrder;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;
        private readonly IMediator _mediator;

        public CustomersController(DevCarsDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }
        
        /// <summary>
        /// Cadastro de clientes
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        ///     "fullname": "Victor",
        ///     "document": "abc123",
        ///     "birthdate": 1993-12-15
        /// }
        /// </remarks>
        /// <param name="command">Dados de um novo cliente</param>
        /// <returns>Objeto recém-criado</returns>
        /// <response code="204">Objeto criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AddCostumerCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Cadastro de pedidos
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        ///     "idCar": 1,
        ///     "idCostumer": 2,
        ///     "extraItems": [
        ///         {
        ///         "description": "Teto solar",
        ///         "price": 2500
        ///         },
        ///         {
        ///           "description": "blindado"
        ///           "price": 10000
        ///         }
        ///       ]
        ///     
        /// }
        /// </remarks>
        /// <param name="id">Id do cliente</param>
        /// <param name="command">Dados de um novo pedido</param>
        /// <returns>Objeto de pedido recém-criado</returns>
        /// <response code="201">Objeto criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Objeto não encontrado</response>
        [HttpPost("{id}/orders")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostOrder(int id, [FromBody] AddOrderCommand command)
        {
            var newOrderId = await _mediator.Send(command);

            if(newOrderId == default(int))
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetOrder), new { id = command.IdCostumer, orderid = newOrderId }, command);
        }
        
        /// <summary>
        /// Mostrando detalhes do pedido
        /// </summary>
        /// <param name="id">Id do cliente</param>
        /// <param name="orderid">Id do pedido</param>
        /// <returns>Um objeto com dados do pedido</returns>
        /// <response code="200">Objeto encontrado</response>
        /// <response code="404">Objeto não encontrado</response>
        [HttpGet("{id}/orders/{orderid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrder(int id, int orderid)
        {
            var query = new GetOrderQuery(id, orderid);

            var orderViewModel = await _mediator.Send(query);

            if(orderViewModel == null)
            {
                return NotFound();
            }

            return Ok(orderViewModel);
        }
    }
}
