using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly DevCarsDbContext _dbContext;

        public CustomersController(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
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
        /// <param name="model">Dados de um novo cliente</param>
        /// <returns>Objeto recém-criado</returns>
        /// <response code="204">Objeto criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] AddCostumerInputModel model)
        {
            var costumer = new Customer(model.FullName, model.Document, model.BirthDate);

            _dbContext.Customers.Add(costumer);
            _dbContext.SaveChanges();

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
        /// <param name="model">Dados de um novo pedido</param>
        /// <returns>Objeto de pedido recém-criado</returns>
        /// <response code="201">Objeto criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost("{id}/orders")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostOrder(int id, [FromBody] AddOrderInputModel model)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == model.IdCar);
            var extraItems = model.ExtraItems.Select(e => new ExtraOrderItem(e.Description, e.Price)).ToList();
            var order = new Order(model.IdCar, model.IdCostumer, car.Price, extraItems);

            //var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == model.IdCostumer);
            //customer.Purchase(order);

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetOrder), new { id = order.IdCostumer, orderid = order.Id }, model);
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
        public IActionResult GetOrder(int id, int orderid)
        {            
            var order = _dbContext.Orders.Include(o => o.ExtraItems).Include(c => c.Customer).SingleOrDefault(o => o.Id == orderid);            

            if (order == null)
            {
                return NotFound();
            }

            var extraItems = order.ExtraItems.Select(e => e.Description).ToList();

            var orderViewModel = new OrderDetailsViewModel(order.IdCar, order.IdCostumer, order.TotalCost, extraItems);

            return Ok(orderViewModel);
        }
    }
}
